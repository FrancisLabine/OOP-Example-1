// <copyright file="Components.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Core.Domain.Shared
{
    using System;
    using SimulationApp.Domain.Plants;

    /// <summary>
    /// A component represents an object produced and consumed by buildings.
    /// It carries its type and moves toward its destination.
    /// </summary>
    public class Component
    {
        public ProductionType Type { get; }

        public int Speed { get; } = 1;

        public BuildingBase Destination { get; private set; }

        public BuildingBase Source { get; private set; }

        public float X { get; private set; }

        public float Y { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Component"/> class.
        /// </summary>
        /// <param name="pType">The type of the component.</param>
        /// <param name="pDestination">The building receiving the component.</param>
        /// <param name="pSource">The building that produced the component.</param>
        public Component(ProductionType pType, BuildingBase pDestination, BuildingBase pSource)
        {
            Type = pType;
            Destination = pDestination ?? throw new ArgumentNullException(nameof(pDestination));
            Source = pSource ?? throw new ArgumentNullException(nameof(pSource));

            X = pSource.PosX;
            Y = pSource.PosY;
        }

        /// <summary>
        /// Executes the component's routine: moves and checks for delivery.
        /// </summary>
        public void ExecuteRoutine()
        {
            Move();
            if (X == Destination.PosX && Y == Destination.PosY)
            {
                Destination.ReceiveComponent(this);
                Destination.RemoveInTransitComponent(this);
            }
        }

        private void Move()
        {
            float dx = Destination.PosX - X;
            float dy = Destination.PosY - Y;
            float ratio = Math.Abs(dx) > 0.01f ? Math.Abs(dy / dx) : 1f;

            X += dx > 0 ? Speed : dx < 0 ? -Speed : 0;
            Y += dy > 0 ? ratio * Speed : dy < 0 ? -ratio * Speed : 0;
        }

        // Keep if we want to change destination dynamically
        public void SetDestination(BuildingBase newDestination)
        {
            Destination = newDestination ?? throw new ArgumentNullException(nameof(newDestination));
        }

        public void SetSource(BuildingBase newSource)
        {
            Source = newSource ?? throw new ArgumentNullException(nameof(newSource));
        }

        public string GetIconPath()
        {
            return $"./src/ressources/{Type.ToString().ToLowerInvariant()}.png";
        }
    }
}