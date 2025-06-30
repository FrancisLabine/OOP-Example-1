using System;
using System.Collections.Generic;
using SimulationApp.Core.Models.Domain.Components;
using SimulationApp.Core.Models.Domain.Shared;

namespace SimulationApp.Core.Models.Domain.Buildings {
    /// <summary>
    /// Abstract base class for all types of buildings in the simulation.
    /// Implements the Observer pattern.
    /// </summary>
    public abstract class BuildingBase : IObserver {
        public List<BuildingBase> Observers { get; private set; } = [];

        public List<Component> Inventory { get; private set; } = [];

        public List<Component> Transport { get; private set; } = [];

        public int PosX { get; private set; }

        public int PosY { get; private set; }

        public string Id { get; private set; }

        public BuildingBase LinkedBuilding { get; set; }

        public BuildingMetadata BuildingMetadata { get; private set; }

        public abstract string GetStatusIcon();

        public abstract void ExecuteRoutine();

        public abstract void NotifyStart();

        public abstract void NotifyStop();

        protected BuildingBase(string pId, int pPosX, int pPosY, BuildingMetadata pBuildingMetadata) {
            Id = pId ?? throw new ArgumentNullException(nameof(pId));
            PosX = pPosX;
            PosY = pPosY;
            BuildingMetadata = pBuildingMetadata;
        }
    }
}
