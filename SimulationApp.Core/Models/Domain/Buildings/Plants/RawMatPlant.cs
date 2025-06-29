namespace SimulationApp.Core.Models.Domain.Buildings.Plants {
    using System;
    using System.Diagnostics;
    using SimulationApp.Core.Models.Domain.Buildings;
    using SimulationApp.Core.Models.Domain.Components;

    public class RawMatPlant : PlantBase
    {
        // Production time of -1 is inactive
        public new int ProductionTime = -1;

        public RawMatPlant(string pId, int pPosX, int pPosY, BuildingMetadata pBuildingMetadata)
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
            return true;
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
        }

        //public override string GetStatusIcon()
        //{
        //    if (ProductionTime == -1)
        //    {
        //        return BuildingMetadata.IconEmpty;
        //    }
        //    else if (ProductionTime <= BuildingMetadata.Interval / 3)
        //    {
        //        return BuildingMetadata.IconLow;
        //    }
        //    else if (BuildingMetadata.Interval / 3 < ProductionTime && ProductionTime <= (BuildingMetadata.Interval / 3) * 2)
        //    {
        //        return BuildingMetadata.IconMedium;
        //    }
        //    else
        //    {
        //        return BuildingMetadata.IconFull;
        //    }
        //}

        public override void NotifyStart()
        {
            if (ProductionTime == -1)
            {
                ProductionTime = 0;
            }
        }

        public override void NotifyStop()
        {
            ProductionTime = -1;
        }

    }
}
