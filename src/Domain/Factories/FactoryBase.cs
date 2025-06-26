using SimulationApp.Domain.Shared;

namespace SimulationApp.Domain.Factories
{
    public abstract class FactoryBase : BuildingBase
    {
        public ProductionType ProductionType;
        public int ProductionTime;

        protected FactoryBase(string pId, int pPosX, int pPosY, BuildingMetadata pBuildingMetadata) : base(pId, pPosX, pPosY, pBuildingMetadata)
        {
            InitializeFactory();
        }

        public abstract void Build();

        public abstract bool IsReadyToBuild();

        protected abstract void InitializeFactory();

        public override abstract void ExecuteRoutine();

        public override abstract string GetStatusIcon();

        public override abstract void NotifyStart();

        public override abstract void NotifyStop();

    }
}
