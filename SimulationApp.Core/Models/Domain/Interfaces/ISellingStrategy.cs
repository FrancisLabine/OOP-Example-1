using SimulationApp.Core.Models.Domain.Buildings.Warehouses;

namespace SimulationApp.Core.Models.Domain.Interfaces {
    public interface ISellingStrategy {
        public void Sell(Warehouse warehouse);
    }
}
