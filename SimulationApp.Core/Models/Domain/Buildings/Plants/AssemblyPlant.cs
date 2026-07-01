using System.Linq;
using SimulationApp.Core.Models.Domain.Components;

namespace SimulationApp.Core.Models.Domain.Buildings.Plants {
    internal class AssemblyPlant : PlantBase {
        private readonly ProductionType inputType1;
        private readonly ProductionType inputType2;

        public AssemblyPlant(string id, int posX, int posY, BuildingMetadata buildingMetadata)
            : base(id, posX, posY, buildingMetadata) {
            inputType1 = ParseProductionType(buildingMetadata.Input1, nameof(buildingMetadata.Input1));
            inputType2 = ParseProductionType(buildingMetadata.Input2, nameof(buildingMetadata.Input2));
        }

        public override void Build() {
            var destination = GetLinkedBuilding();
            destination.Transport.Add(new Component(ProductionType, destination, this));
            RemoveInputs(inputType1, BuildingMetadata.InputQuantity1 ?? 0);
            RemoveInputs(inputType2, BuildingMetadata.InputQuantity2 ?? 0);
        }

        public override void ExecuteRoutine() {
            TickProduction();
            MoveInboundComponents();
        }

        public override bool IsReadyToBuild() {
            return Inventory.Count(component => component.Type == inputType1) >= (BuildingMetadata.InputQuantity1 ?? 0)
                && Inventory.Count(component => component.Type == inputType2) >= (BuildingMetadata.InputQuantity2 ?? 0);
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
