using System;
using SimulationApp.Core.Models.Domain.Buildings;

namespace SimulationApp.Core.Models.Domain.Components {

    /// <summary>
    /// A component represents an object produced and consumed by buildings.
    /// It carries its type and moves toward its destination.
    /// </summary>
    public class Component {
        public ProductionType Type { get; private set; }

        public int Speed { get; private set; } = 1;

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
        public Component(ProductionType pType, BuildingBase pDestination, BuildingBase pSource) {
            Type = pType;
            Destination = pDestination ?? throw new ArgumentNullException(nameof(pDestination));
            Source = pSource ?? throw new ArgumentNullException(nameof(pSource));

            X = pSource.PosX;
            Y = pSource.PosY;
        }

        /// <summary>
        /// Executes the component's routine: moves and checks for delivery.
        /// </summary>
        public void ExecuteRoutine() {
            Move();
            if (HasArrived()) {
                Destination.Inventory.Add(this);
                Destination.Transport.Remove(this);
            }
        }

        private void Move() {
            float dx = Destination.PosX - X;
            float dy = Destination.PosY - Y;
            float distance = MathF.Sqrt((dx * dx) + (dy * dy));

            if (distance <= Speed) {
                X = Destination.PosX;
                Y = Destination.PosY;
                return;
            }

            X += dx / distance * Speed;
            Y += dy / distance * Speed;
        }

        private bool HasArrived() {
            return Math.Abs(X - Destination.PosX) < 0.001f
                && Math.Abs(Y - Destination.PosY) < 0.001f;
        }
    }
}
