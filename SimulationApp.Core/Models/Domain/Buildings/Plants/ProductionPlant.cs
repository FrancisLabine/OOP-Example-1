using System.Linq;
using SimulationApp.Core.Models.Domain.Components;

namespace SimulationApp.Core.Models.Domain.Buildings.Plants {
    internal class ProductionPlant : PlantBase {
        private readonly ProductionType inputType;

        public ProductionPlant(string id, int posX, int posY, BuildingMetadata buildingMetadata)
            : base(id, posX, posY, buildingMetadata) {
            inputType = ParseProductionType(buildingMetadata.Input1, nameof(buildingMetadata.Input1));
        }

        public override void Build() {
            var destination = GetLinkedBuilding();
            destination.Transport.Add(new Component(ProductionType, destination, this));
            RemoveInputs(inputType, BuildingMetadata.InputQuantity1 ?? 0);
        }

        public override bool IsReadyToBuild() {
            return Inventory.Count(component => component.Type == inputType) >= (BuildingMetadata.InputQuantity1 ?? 0);
        }

        public override void ExecuteRoutine() {
            TickProduction();
            MoveInboundComponents();
        }

        public override void NotifyStart() {
            if (ProductionTime != -1) {
                return;
            }

            if (IsReadyToBuild()) {
                StartProductionIfIdle();
                return;
            }

            foreach (var observer in Observers) {
                observer.NotifyStart();
            }
        }

        public override void NotifyStop() {
            ProductionTime = -1;
            foreach (var observer in Observers) {
                observer.NotifyStop();
            }
        }

        private void RemoveInputs(ProductionType type, int quantity) {
            for (var removed = 0; removed < quantity; removed++) {
                var index = Inventory.FindIndex(component => component.Type == type);
                if (index < 0) {
                    throw new InvalidOperationException($"Building '{Id}' cannot consume missing input '{type}'.");
                }

                Inventory.RemoveAt(index);
            }
        }
    }
}
