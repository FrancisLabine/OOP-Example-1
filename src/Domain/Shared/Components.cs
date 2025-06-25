// <copyright file="Components.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Domain.Shared
{
    using System;
    using SimulationApp.Domain.Factories;

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
            this.Type = pType;
            this.Destination = pDestination ?? throw new ArgumentNullException(nameof(pDestination));
            this.Source = pSource ?? throw new ArgumentNullException(nameof(pSource));

            this.X = pSource.PosX;
            this.Y = pSource.PosY;
        }

        /// <summary>
        /// Executes the component's routine: moves and checks for delivery.
        /// </summary>
        public void Ship()
        {
            this.Move();
            if (this.X == this.Destination.PosX && this.Y == this.Destination.PosY)
            {
                this.Destination.ReceiveComponent(this);
                this.Destination.RemoveInTransitComponent(this);
            }
        }

        private void Move()
        {
            float dx = this.Destination.PosX - this.X;
            float dy = this.Destination.PosY - this.Y;
            float ratio = Math.Abs(dx) > 0.01f ? Math.Abs(dy / dx) : 1f;

            this.X += dx > 0 ? this.Speed : dx < 0 ? -this.Speed : 0;
            this.Y += dy > 0 ? ratio * this.Speed : dy < 0 ? -ratio * this.Speed : 0;
        }

        // Keep if we want to change destination dynamically
        public void SetDestination(BuildingBase newDestination)
        {
            this.Destination = newDestination ?? throw new ArgumentNullException(nameof(newDestination));
        }

        public void SetSource(BuildingBase newSource)
        {
            this.Source = newSource ?? throw new ArgumentNullException(nameof(newSource));
        }

        public string GetIconPath()
        {
            return $"./src/ressources/{this.Type.ToString().ToLowerInvariant()}.png";
        }
    }
}