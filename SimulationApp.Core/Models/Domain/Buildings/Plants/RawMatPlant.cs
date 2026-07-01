using SimulationApp.Core.Models.Domain.Components;

namespace SimulationApp.Core.Models.Domain.Buildings.Plants {
    public class RawMatPlant : PlantBase {
        public RawMatPlant(string id, int posX, int posY, BuildingMetadata buildingMetadata)
            : base(id, posX, posY, buildingMetadata) {
        }

        public override void Build() {
            var destination = GetLinkedBuilding();
            destination.Transport.Add(new Component(ProductionType, destination, this));
        }

        public override bool IsReadyToBuild() {
            return true;
        }

        public override void ExecuteRoutine() {
            TickProduction();
        }

        public override void NotifyStart() {
            StartProductionIfIdle();
        }

        public override void NotifyStop() {
            ProductionTime = -1;
        }
    }
}
