using System.Collections.Generic;
using SimulationApp.Core.Models.Domain.Components;
using SimulationApp.Core.Models.Domain.Interfaces;
using SimulationApp.Core.Models.Domain.Strategies;

namespace SimulationApp.Core.Models.Domain.Buildings.Warehouses {
    public class Warehouse : BuildingBase {
        public ISellingStrategy SellingStrategy { get; set; }

        public Warehouse(string id, int x, int y, BuildingMetadata metadata)
            : base(id, x, y, metadata) {
            SellingStrategy = new RandomStrategy();
        }

        public override void ExecuteRoutine() {
            var maxCapacity = BuildingMetadata.InputQuantity1 ?? 0;
            var currentLoad = Inventory.Count + Transport.Count;
            if (currentLoad < maxCapacity) {
                foreach (var observer in Observers) {
                    observer.NotifyStart();
                }
            } else {
                foreach (var observer in Observers) {
                    observer.NotifyStop();
                }
            }

            foreach (var component in new List<Component>(Transport)) {
                component.ExecuteRoutine();
            }

            SellingStrategy.Sell(this);
        }

        public override void NotifyStart() {
        }

        public override void NotifyStop() {
        }

        public override BuildingStatus GetStatus() {
            if (BuildingMetadata.InputQuantity1 == null) {
                return BuildingStatus.Empty;
            }

            int? capacity = BuildingMetadata.InputQuantity1;
            int count = Inventory.Count;

            if (count == 0) {
                return BuildingStatus.Empty;
            }

            if (count <= capacity / 3) {
                return BuildingStatus.Low;
            }

            if (count < capacity) {
                return BuildingStatus.Medium;
            }

            return BuildingStatus.Full;
        }
    }
}
