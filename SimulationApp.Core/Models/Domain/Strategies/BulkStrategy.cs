using System.Diagnostics;
using SimulationApp.Core.Models.Domain.Buildings.Warehouses;
using SimulationApp.Core.Models.Domain.Interfaces;

namespace SimulationApp.Core.Models.Domain.Strategies {
    internal class BulkStrategy : ISellingStrategy {
        public void Sell(Warehouse warehouse) {
            if (warehouse.Inventory.Count > 3) {
                Debug.WriteLine("Sold 2 Planes");
                var item = warehouse.Inventory[0];
                warehouse.Inventory.RemoveAt(0);
                item = null;

                item = warehouse.Inventory[0];
                warehouse.Inventory.RemoveAt(0);
                item = null;
            }
        }
    }
}
