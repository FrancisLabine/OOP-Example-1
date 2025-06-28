namespace SimulationApp.Core.Models.Domain {
    using System.Collections.Generic;
    using SimulationApp.Core.Models.Domain.Buildings;
    using SimulationApp.Core.Models.Domain.Buildings.Pathways;

    public class EnvironmentModel {
        public List<BuildingBase> Buildings { get; set; } = [];

        public List<Pathway> Paths { get; set; } = [];

        public List<BuildingMetadata> Metadata { get; set; } = [];
    }
}
