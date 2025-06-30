using SimulationApp.Core.Models.Domain.Buildings;
using SimulationApp.Core.Models.Domain.Buildings.Plants;
using SimulationApp.Core.Models.Domain.Buildings.Warehouses;

namespace SimulationApp.Core.Models.Domain.Buildings {
    public static class BuildingFactory {
        public static BuildingBase? CreateBuilding(string type, string id, int x, int y, BuildingMetadata metadata) {
            return type switch {
                "usine-matiere" => new RawMatPlant(id, x, y, metadata),
                "usine-aile" => new ProductionPlant(id, x, y, metadata),
                "usine-assemblage" => new AssemblyPlant(id, x, y, metadata),
                "usine-moteur" => new ProductionPlant(id, x, y, metadata),
                "entrepot" => new Warehouse(id, x, y, metadata),
                _ => null
            };
        }
    }
}
