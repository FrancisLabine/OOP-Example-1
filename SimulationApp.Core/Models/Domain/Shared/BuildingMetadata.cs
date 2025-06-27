// <copyright file="BuildingMetada.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Core.Models.Domain.Shared
{
    /// <summary>
    /// Holds configuration and resource info for a building.
    /// Initialized from XML configuration files.
    /// </summary>
    public class BuildingMetadata
    {
        public int? Interval { get; set; }

        public string Input1 { get; set; }

        public string Input2 { get; set; }

        public string Output { get; set; }

        public int? InputQuantity1 { get; set; }

        public int? InputQuantity2 { get; set; }

        public string Type { get; set; }

        public string IconEmpty { get; set; }

        public string IconLow { get; set; }

        public string IconMedium { get; set; }

        public string IconFull { get; set; }
    }
}
