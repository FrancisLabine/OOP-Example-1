namespace SimulationApp.Core.Models.Domain.Buildings.Plants {
    public abstract class PlantBase : BuildingBase
    {
        public ProductionType ProductionType { get; protected set; }

        public int ProductionTime { get; set; } = -1;

        protected PlantBase(string id, int posX, int posY, BuildingMetadata buildingMetadata)
            : base(id, posX, posY, buildingMetadata)
        {
        }

        public abstract void Build();

        public abstract bool IsReadyToBuild();

        protected abstract void InitializeFactory();

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
    }
}
