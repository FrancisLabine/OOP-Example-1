using SimulationApp.Core.Models;
using SimulationApp.Core.Models.Domain;
using SimulationApp.Core.Models.Infrastructure.Xml;
using SimulationApp.Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationApp.Core.Controllers
{
    public class SimulationController {
        private readonly SimulationLoop loop;
        private readonly EnvironmentModel model;

        public SimulationController(string configPath) {
            var reader = new XmlReaderService(configPath);
            var loader = new EnvironmentLoader(reader);
            model = loader.Load();
            loop = new SimulationLoop(model.Buildings);
        }

        //public void RunCycle() => loop.RunOnce();

        //public IEnumerable<string> GetStatus() =>
        //    model.Buildings.Select(b => $"{b.Name}: {b.Inventory.Count} items");
    }
}
