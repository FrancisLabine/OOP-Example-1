namespace SimulationApp.Core.Models.Domain.Buildings.Warehouses {
    using System;
    using System.Collections.Generic;
    using SimulationApp.Core.Models.Domain.Buildings;
    using SimulationApp.Core.Models.Domain.Components;

    /// <summary>
    /// A warehouse is the final node in the production chain.
    /// It does not produce components but manages inventory and selling logic.
    /// </summary>
    public class Warehouse : BuildingBase {
        private static readonly Random Random = new();

        public Warehouse(string id, int x, int y, BuildingMetadata metadata)
            : base(id, x, y, metadata) {
        }

        /// <summary>
        /// Executes the warehouse routine:
        /// - Notify upstream factories to stop or continue
        /// - Process components in transit
        /// - Attempt to sell items based on strategy.
        /// </summary>
        public override void ExecuteRoutine() {
            var maxCapacity = BuildingMetadata?.InputQuantity1 ?? 0;
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

            TrySell(0);
        }

        /// <summary>
        /// Warehouses don’t respond to upstream notifications.
        /// </summary>
        public override void NotifyStart() {
        }

        public override void NotifyStop() {
        }

        private void TrySell(int strategy) {
            switch (strategy) {
                case 0:
                    if (Inventory.Count > 0 && Random.Next(400) == 26) {
                        Console.WriteLine("vente");
                        Inventory.RemoveAt(0);
                    }

                    break;

                case 1:
                    if (Inventory.Count > 3) {
                        Console.WriteLine("vente");
                        Inventory.RemoveAt(0);
                        Console.WriteLine("vente");
                        Inventory.RemoveAt(0);
                    }

                    break;
            }
        }

        public override string GetStatusIcon() {
            if (BuildingMetadata == null) {
                return string.Empty;
            }

            int? capacity = BuildingMetadata.InputQuantity1;
            int count = Inventory.Count;

            if (count == 0) {
                return BuildingMetadata.IconEmpty;
            }

            if (count <= capacity / 3) {
                return BuildingMetadata.IconLow;
            }

            if (count < capacity) {
                return BuildingMetadata.IconMedium;
            }

            return BuildingMetadata.IconFull;
        }
    }
}