using SimulationApp.Core.Models.Domain.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationApp.Core.Models.Domain.Buildings.Plants
{
    internal class AssemblyPlant : PlantBase
    {
        public ProductionType Input1 { get; private set; }

        public ProductionType Input2 { get; private set; }

        public AssemblyPlant(string pId, int pPosX, int pPosY, BuildingMetadata pBuildingMetadata)
            : base(pId, pPosX, pPosY, pBuildingMetadata)
        {
            InitializeFactory();
        }

        protected override void InitializeFactory()
        {
            Input1 = Enum.Parse<ProductionType>(BuildingMetadata.Input1.ToUpper(System.Globalization.CultureInfo.CurrentCulture));
            Input2 = Enum.Parse<ProductionType>(BuildingMetadata.Input2.ToUpper(System.Globalization.CultureInfo.CurrentCulture));
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
                        Inventory.RemoveAt(i);
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

            foreach (var inTransitComponent in Transport)
            {
                inTransitComponent.ExecuteRoutine();
            }
        }

        //public override string GetStatusIcon()
        //{
        //    throw new NotImplementedException();
        //}

        public override bool IsReadyToBuild()
        {
            bool hasTwoOfEachType = LinkedBuilding.Inventory
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
