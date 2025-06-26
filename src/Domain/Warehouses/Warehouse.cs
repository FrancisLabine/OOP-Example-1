// <copyright file="Warehouse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Domain.Warehouses
{
    using System;
    using System.Collections.Generic;
    using SimulationApp.Domain.Shared;

    /// <summary>
    /// A warehouse is the final node in the production chain.
    /// It does not produce components but manages inventory and selling logic.
    /// </summary>
    public class Warehouse : BuildingBase
    {
        private static readonly Random Random = new ();

        public Warehouse(string id, int x, int y, BuildingMetadata metadata)
            : base(id, x, y, metadata)
        {
        }

        /// <summary>
        /// Executes the warehouse routine:
        /// - Notify upstream factories to stop or continue
        /// - Process components in transit
        /// - Attempt to sell items based on strategy.
        /// </summary>
        public override void ExecuteRoutine()
        {
            var maxCapacity = this.BuildingMetadata?.InputQuantity1 ?? 0;
            var currentLoad = this.Inventory.Count + this.Transport.Count;

            if (currentLoad < maxCapacity)
            {
                foreach (var observer in this.Observers)
                {
                    observer.NotifyStart();
                }
            }
            else
            {
                foreach (var observer in this.Observers)
                {
                    observer.NotifyStop();
                }
            }

            foreach (var component in new List<Component>(this.Transport))
            {
                component.ExecuteRoutine();
            }

            this.TrySell(0);
        }

        /// <summary>
        /// Warehouses don’t respond to upstream notifications.
        /// </summary>
        public override void NotifyStart()
        {
        }

        public override void NotifyStop()
        {
        }

        private void TrySell(int strategy)
        {
            switch (strategy)
            {
                case 0:
                    if (this.Inventory.Count > 0 && Random.Next(400) == 26)
                    {
                        Console.WriteLine("vente");
                        this.Inventory.RemoveAt(0);
                    }

                    break;

                case 1:
                    if (this.Inventory.Count > 3)
                    {
                        Console.WriteLine("vente");
                        this.Inventory.RemoveAt(0);
                        Console.WriteLine("vente");
                        this.Inventory.RemoveAt(0);
                    }

                    break;
            }
        }

        public override string GetStatusIcon()
        {
            if (this.BuildingMetadata == null)
            {
                return string.Empty;
            }

            int? capacity = this.BuildingMetadata.InputQuantity1;
            int count = this.Inventory.Count;

            if (count == 0)
            {
                return this.BuildingMetadata.IconEmpty;
            }

            if (count <= capacity / 3)
            {
                return this.BuildingMetadata.IconLow;
            }

            if (count < capacity)
            {
                return this.BuildingMetadata.IconMedium;
            }

            return this.BuildingMetadata.IconFull;
        }
    }
}