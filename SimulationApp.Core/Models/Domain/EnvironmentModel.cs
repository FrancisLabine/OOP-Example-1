using System.Collections.Generic;
using System.Linq;
using SimulationApp.Core.Models.Domain.Buildings;
using SimulationApp.Core.Models.Domain.Buildings.Pathways;
using SimulationApp.Core.Models.Domain.Buildings.Warehouses;
using SimulationApp.Core.Models.Domain.Strategies;

namespace SimulationApp.Core.Models.Domain {
    public class EnvironmentModel {
        public List<BuildingBase> Buildings { get; set; } = [];

        public List<Pathway> Paths { get; set; } = [];

        public void SetStrategy(int pIdx) {
            foreach (var warehouse in Buildings.OfType<Warehouse>()) {
                warehouse.SellingStrategy = pIdx switch {
                    0 => new RandomStrategy(),
                    1 => new BulkStrategy(),
                    _ => new RandomStrategy(),
                };
            }
        }
    }
}
