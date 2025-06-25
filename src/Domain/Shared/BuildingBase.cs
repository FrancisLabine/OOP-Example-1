// <copyright file="BuildingBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Domain.Shared
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Abstract base class for all types of buildings in the simulation.
    /// Implements the Observer pattern.
    /// </summary>
    public abstract class BuildingBase : IObserver
    {
        protected readonly List<BuildingBase> Observers = new ();

        protected readonly List<Component> Inventory = new ();

        protected readonly List<Component> Transport = new ();

        public int PosX { get; private set; }

        public int PosY { get; private set; }

        public string Id { get; private set; }

        private BuildingBase? linkedBuilding;

        public BuildingMetadata? BuildingMetadata { get; protected set; }

        /// <summary>
        /// Gets the icon representing the building's status.
        /// </summary>
        /// <returns></returns>
        public abstract string GetStatusIcon();

        /// <summary>
        /// Executes the building's main routine logic.
        /// </summary>
        public abstract void ExecuteRoutine();

        /// <summary>
        /// Initializes the building’s internal state. Called during setup.
        /// </summary>
        protected abstract void InitializeBuilding();

        public abstract void NotifyStart();

        public abstract void NotifyStop();

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingBase"/> class.
        /// </summary>
        /// <param name="pId">The building's unique identifier.</param>
        /// <param name="pPosX">X position.</param>
        /// <param name="pPosY">Y position.</param>
        /// <param name="pBuildingMetadata">Associated metadata object.</param>
        protected BuildingBase(string pId, int pPosX, int pPosY, BuildingMetadata pBuildingMetadata)
        {
            this.Id = pId ?? throw new ArgumentNullException(nameof(pId));
            this.PosX = pPosX;
            this.PosY = pPosY;
            this.BuildingMetadata = pBuildingMetadata ?? throw new ArgumentNullException(nameof(pBuildingMetadata));
        }

        public BuildingBase? LinkedBuilding
        {
            get => this.linkedBuilding;
            set => this.linkedBuilding = value;
        }

        public IReadOnlyList<BuildingBase> GetObservers() => this.Observers.AsReadOnly();

        public IReadOnlyList<Component> GetTransport() => this.Transport.AsReadOnly();

        public virtual void ReceiveComponent(Component component) => this.Inventory.Add(component);

        public virtual void RemoveInTransitComponent(Component component) => this.Transport.Remove(component);
    }
}
