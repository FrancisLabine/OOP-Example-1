using System;
using System.Diagnostics;
using SimulationApp.Core.Models.Domain.Buildings.Warehouses;
using SimulationApp.Core.Models.Domain.Interfaces;

namespace SimulationApp.Core.Models.Domain.Strategies {
    internal class RandomStrategy : ISellingStrategy {
        private static readonly Random Random = new ();

        public void Sell(Warehouse warehouse) {
            if (warehouse.Inventory.Count > 0 && Random.Next(400) == 26) {
                Debug.WriteLine("Sold 1 plane");
                var item = warehouse.Inventory[0];
                warehouse.Inventory.RemoveAt(0);
                item = null;
            }
        }
    }
}
