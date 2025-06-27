// <copyright file="EnvironmentModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Core.Models.Domain {
    using System.Collections.Generic;
    using SimulationApp.Core.Models.Domain.Buildings;
    using SimulationApp.Core.Models.Domain.Buildings.Pathways;

    public class EnvironmentModel
    {
        public List<BuildingBase> Buildings { get; set; } = new ();

        public List<Pathway> Paths { get; set; } = new ();

        public List<BuildingMetadata> Metadata { get; set; } = new ();
    }
}
