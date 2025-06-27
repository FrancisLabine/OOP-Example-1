// <copyright file="Pathway.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Core.Models.Domain.Pathways
{
    /// <summary>
    /// Represents a path between two coordinate points.
    /// This is typically used to draw a line (e.g., in the UI or for logical routing).
    /// </summary>
    public class Pathway
    {
        /// <summary>
        /// Gets or sets the X coordinate of the starting point.
        /// </summary>
        public int X1 { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate of the ending point.
        /// </summary>
        public int X2 { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate of the starting point.
        /// </summary>
        public int Y1 { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate of the ending point.
        /// </summary>
        public int Y2 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pathway"/> class.
        /// Optional: Initializes a new instance of the <see cref="Pathway"/> class.
        /// </summary>
        public Pathway()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pathway"/> class.
        /// Optional: Initializes a new instance of the <see cref="Pathway"/> class with coordinates.
        /// </summary>
        public Pathway(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }
    }
}
