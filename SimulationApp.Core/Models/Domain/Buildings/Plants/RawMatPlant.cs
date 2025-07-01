using System;
using SimulationApp.Core.Models.Domain.Components;

namespace SimulationApp.Core.Models.Domain.Buildings.Plants {
    public class RawMatPlant : PlantBase {
        public RawMatPlant(string id, int posX, int posY, BuildingMetadata buildingMetadata)
            : base(id, posX, posY, buildingMetadata) {
            InitializeFactory();
        }

        public override void Build() {
            Component comp = new (ProductionType, LinkedBuilding, this);
            LinkedBuilding.Transport.Add(comp);
        }

        public override bool IsReadyToBuild() {
            return true;
        }

        protected override void InitializeFactory() {
            ProductionType = Enum.Parse<ProductionType>(BuildingMetadata.Output.ToUpper(System.Globalization.CultureInfo.CurrentCulture));
        }

        public override void ExecuteRoutine() {
            if (ProductionTime > -1) {
                ProductionTime++;
            }

            if (ProductionTime >= BuildingMetadata.Interval) {
                Build();
                ProductionTime = -1;
            }
        }

        public override void NotifyStart() {
            if (ProductionTime == -1) {
                ProductionTime = 0;
            }
        }

        public override void NotifyStop() {
            ProductionTime = -1;
        }
    }
}
