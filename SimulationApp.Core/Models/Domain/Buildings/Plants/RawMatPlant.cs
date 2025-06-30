namespace SimulationApp.Core.Models.Domain.Buildings.Plants {
    using System;
    using System.Diagnostics;
    using SimulationApp.Core.Models.Domain.Buildings;
    using SimulationApp.Core.Models.Domain.Components;

    public class RawMatPlant : PlantBase
    {
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
