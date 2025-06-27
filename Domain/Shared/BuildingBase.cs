// <copyright file="BuildingBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Core.Domain.Shared
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Abstract base class for all types of buildings in the simulation.
    /// Implements the Observer pattern.
    /// </summary>
    public abstract class BuildingBase : IObserver
    {
        public readonly List<BuildingBase> Observers = new ();

        public readonly List<Component> Inventory = new ();

        public readonly List<Component> Transport = new ();

        public int PosX { get; private set; }

        public int PosY { get; private set; }

        public string Id { get; private set; }

        public BuildingBase LinkedBuilding { get; set;}

        public BuildingMetadata BuildingMetadata { get; protected set; }

        /// <summary>
        /// Gets the icon representing the building's status.
        /// </summary>
        /// <returns></returns>
        public abstract string GetStatusIcon();

        /// <summary>
        /// Executes the building's main routine logic.
        /// </summary>
        public abstract void ExecuteRoutine();

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
            Id = pId ?? throw new ArgumentNullException(nameof(pId));
            PosX = pPosX;
            PosY = pPosY;
            BuildingMetadata = pBuildingMetadata ?? throw new ArgumentNullException(nameof(pBuildingMetadata));
        }

        public virtual void ReceiveComponent(Component component) => Inventory.Add(component);

        public virtual void RemoveInTransitComponent(Component component) => Transport.Remove(component);
    }
}
