using System;
using System.Globalization;

namespace SimulationApp.Core.Models.Domain.Buildings.Plants {
    public abstract class PlantBase : BuildingBase {
        protected PlantBase(string id, int posX, int posY, BuildingMetadata buildingMetadata)
            : base(id, posX, posY, buildingMetadata) {
            ProductionType = ParseProductionType(buildingMetadata.Output, nameof(buildingMetadata.Output));
        }

        public ProductionType ProductionType { get; }

        public int ProductionTime { get; protected set; } = -1;

        public abstract void Build();

        public abstract bool IsReadyToBuild();

        public override BuildingStatus GetStatus() {
            if (ProductionTime == -1) {
                return BuildingStatus.Empty;
            }

            var interval = BuildingMetadata.Interval ?? 1;
            if (ProductionTime <= interval / 3) {
                return BuildingStatus.Low;
            }

            if (ProductionTime <= interval / 3 * 2) {
                return BuildingStatus.Medium;
            }

            return BuildingStatus.Full;
        }

        protected void TickProduction() {
            if (ProductionTime > -1) {
                ProductionTime++;
            }

            if (ProductionTime >= (BuildingMetadata.Interval ?? 0)) {
                Build();
                ProductionTime = -1;
            }
        }

        protected void MoveInboundComponents() {
            foreach (var component in InventorySnapshot(Transport)) {
                component.ExecuteRoutine();
            }
        }

        protected void StartProductionIfIdle() {
            if (ProductionTime == -1) {
                ProductionTime = 0;
            }
        }

        protected BuildingBase GetLinkedBuilding() {
            return LinkedBuilding ?? throw new InvalidOperationException($"Building '{Id}' is not linked to an output building.");
        }

        protected static ProductionType ParseProductionType(string? value, string fieldName) {
            if (string.IsNullOrWhiteSpace(value)) {
                throw new InvalidOperationException($"Missing required production type in '{fieldName}'.");
            }

            return Enum.Parse<ProductionType>(value.ToUpper(CultureInfo.InvariantCulture));
        }

        private static List<T> InventorySnapshot<T>(IEnumerable<T> items) {
            return new List<T>(items);
        }
    }
}
