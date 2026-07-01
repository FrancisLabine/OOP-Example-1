using System;
using System.Collections.Generic;
using SimulationApp.Core.Models.Domain.Components;
using SimulationApp.Core.Models.Domain.Interfaces;

namespace SimulationApp.Core.Models.Domain.Buildings {
    /// <summary>
    /// Abstract base class for all types of buildings in the simulation.
    /// Implements the Observer pattern.
    /// </summary>
    public abstract class BuildingBase : IObserver {
        public List<BuildingBase> Observers { get; } = [];

        public List<Component> Inventory { get; } = [];

        public List<Component> Transport { get; } = [];

        public int PosX { get; private set; }

        public int PosY { get; private set; }

        public string Id { get; private set; }

        public BuildingBase? LinkedBuilding { get; set; }

        public BuildingMetadata BuildingMetadata { get; }

        public abstract BuildingStatus GetStatus();

        public abstract void ExecuteRoutine();

        public abstract void NotifyStart();

        public abstract void NotifyStop();

        protected BuildingBase(string id, int posX, int posY, BuildingMetadata pBuildingMetadata) {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            PosX = posX;
            PosY = posY;
            BuildingMetadata = pBuildingMetadata;
        }
    }
}
