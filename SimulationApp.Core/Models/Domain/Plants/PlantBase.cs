namespace SimulationApp.Core.Models.Domain.Plants
{
    using SimulationApp.Core.Models.Domain.Shared;

    public abstract class PlantBase : BuildingBase
    {
        public ProductionType ProductionType { get; protected set; }

        public int ProductionTime { get; set; } = -1;

        protected PlantBase(string pId, int pPosX, int pPosY, BuildingMetadata pBuildingMetadata)
            : base(pId, pPosX, pPosY, pBuildingMetadata)
        {
        }

        public abstract void Build();

        public abstract bool IsReadyToBuild();

        protected abstract void InitializeFactory();

        public override abstract void ExecuteRoutine();

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

        public override abstract void NotifyStart();

        public override abstract void NotifyStop();

    }
}
