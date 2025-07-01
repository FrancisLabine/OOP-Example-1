using System;
using SimulationApp.Core.Models.Domain.Components;

namespace SimulationApp.Core.Models.Domain.Buildings.Plants
{
    internal class ProductionPlant : PlantBase
    {
        public ProductionPlant(string id, int posX, int posY, BuildingMetadata buildingMetadata)
            : base(id, posX, posY, buildingMetadata)
        {
            InitializeFactory();
        }

        public override void Build()
        {
            Component comp = new (ProductionType, LinkedBuilding, this);
            LinkedBuilding.Transport.Add(comp);

            for (int i = 0; i < BuildingMetadata.InputQuantity1; i++) {
                var item = Inventory[0];
                Inventory.RemoveAt(0);
                item = null;
            }
        }

        public override bool IsReadyToBuild()
        {
            if (Inventory.Count >= BuildingMetadata.InputQuantity1)
            {
                return true;
            }

            return false;
        }

        protected override void InitializeFactory()
        {
            ProductionType = Enum.Parse<ProductionType>(BuildingMetadata.Output.ToUpper(System.Globalization.CultureInfo.CurrentCulture));
        }

        public override void ExecuteRoutine()
        {
            if (ProductionTime > -1)
            {
                ProductionTime++;
            }

            if (ProductionTime >= BuildingMetadata.Interval)
            {
                Build();
                ProductionTime = -1;
            }

            for (int i = 0; i < Transport.Count; i++) {
                Transport[i].ExecuteRoutine();
            }
        }

        public override string GetStatusIcon()
        {
            if (ProductionTime == -1)
            {
                return BuildingMetadata.IconEmpty;
            }
            else if (ProductionTime <= BuildingMetadata.Interval / 3)
            {
                return BuildingMetadata.IconLow;
            }
            else if (BuildingMetadata.Interval / 3 < ProductionTime && ProductionTime <= BuildingMetadata.Interval / 3 * 2)
            {
                return BuildingMetadata.IconMedium;
            }
            else
            {
                return BuildingMetadata.IconFull;
            }
        }

        public override void NotifyStart()
        {
            if (ProductionTime == -1)
            {
                if (IsReadyToBuild())
                {
                    ProductionTime = 0;
                }
                else
                {
                    foreach (var obs in Observers)
                    {
                        obs.NotifyStart();
                    }
                }
            }
        }

        public override void NotifyStop()
        {
            ProductionTime = -1;
            foreach (var obs in Observers)
            {
                obs.NotifyStop();
            }
        }
    }
}
