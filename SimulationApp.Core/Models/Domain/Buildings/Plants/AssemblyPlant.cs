using System;
using System.Linq;
using SimulationApp.Core.Models.Domain.Components;

namespace SimulationApp.Core.Models.Domain.Buildings.Plants
{
    internal class AssemblyPlant : PlantBase
    {
        // private ProductionType Input1 { get; set; }
        // private ProductionType Input2 { get; set; }
        public AssemblyPlant(string pId, int pPosX, int pPosY, BuildingMetadata pBuildingMetadata)
            : base(pId, pPosX, pPosY, pBuildingMetadata)
        {
            InitializeFactory();
        }

        protected override void InitializeFactory()
        {
            // Input1 = Enum.Parse<ProductionType>(BuildingMetadata.Input1.ToUpper(System.Globalization.CultureInfo.CurrentCulture));
            // Input2 = Enum.Parse<ProductionType>(BuildingMetadata.Input2.ToUpper(System.Globalization.CultureInfo.CurrentCulture));
            ProductionType = Enum.Parse<ProductionType>(BuildingMetadata.Output.ToUpper(System.Globalization.CultureInfo.CurrentCulture));
        }

        public override void Build()
        {
            Component comp = new (ProductionType, LinkedBuilding, this);
            LinkedBuilding.Transport.Add(comp);

            var grouped = Inventory.GroupBy(c => c.GetType());

            foreach (var group in grouped)
            {
                int removed = 0;
                for (int i = Inventory.Count - 1; i >= 0 && removed < 2; i--)
                {
                    if (Inventory[i].GetType() == group.Key)
                    {
                        var item = Inventory[i];
                        Inventory.RemoveAt(i);
                        item = null;
                        removed++;
                    }
                }
            }
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

        public override bool IsReadyToBuild()
        {
            bool hasTwoOfEachType = Inventory.Count != 0 && Inventory
            .GroupBy(c => c.GetType())
            .All(g => g.Count() >= 2);

            return hasTwoOfEachType;
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
