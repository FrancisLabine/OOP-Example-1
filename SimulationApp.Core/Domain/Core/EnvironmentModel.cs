// <copyright file="EnvironmentModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Core.Domain.Core
{
    using System.Collections.Generic;
    using SimulationApp.Core.Domain.Shared;

    public class EnvironmentModel
    {
        public List<BuildingBase> Buildings { get; set; } = new ();

        public List<Pathway> Paths { get; set; } = new ();

        public List<BuildingMetadata> Metadata { get; set; } = new ();
    }
}
