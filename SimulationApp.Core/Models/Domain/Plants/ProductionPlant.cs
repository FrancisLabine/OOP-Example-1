using System;
using SimulationApp.Core.Models.Domain.Shared;

namespace SimulationApp.Core.Models.Domain.Plants
{
    internal class ProductionPlant : PlantBase
    {
        public ProductionPlant(string pId, int pPosX, int pPosY, BuildingMetadata pBuildingMetadata)
            : base(pId, pPosX, pPosY, pBuildingMetadata)
        {

            InitializeFactory();
        }

        public override void Build()
        {
            Component comp = new (ProductionType, LinkedBuilding, this);
            LinkedBuilding.Transport.Add(comp);
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

            foreach (var inTransitComponent in Transport)
            {
                inTransitComponent.ExecuteRoutine();
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
