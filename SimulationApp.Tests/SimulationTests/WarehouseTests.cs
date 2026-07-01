namespace SimulationApp {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using NUnit.Framework;
    using SimulationApp.Core.Models.Domain.Buildings;
    using SimulationApp.Core.Models.Domain.Buildings.Plants;
    using SimulationApp.Core.Models.Domain.Buildings.Warehouses;
    using SimulationApp.Core.Models.Domain.Components;
    using SimulationApp.Core.Models.Utils.Xml;

    [TestFixture]
    public class WarehouseTests {
        [Test]
        public void WarehouseStartsEmpty() {
            var buildingList = LoadBuildings("SimulationTests/config1.xml");

            var warehouse = buildingList.OfType<Warehouse>().First();

            Assert.That(warehouse.GetStatus(), Is.EqualTo(BuildingStatus.Empty));
        }

        [Test]
        public void WarehouseSignalsRawMaterialPlantWhenCapacityIsAvailable() {
            var buildingList = LoadBuildings("SimulationTests/config2.xml");
            var warehouse = buildingList.OfType<Warehouse>().First();
            var factory = buildingList.OfType<RawMatPlant>().First();

            warehouse.ExecuteRoutine();

            Assert.That(factory.ProductionTime, Is.EqualTo(0));
            Assert.That(factory.LinkedBuilding, Is.SameAs(warehouse));
            Assert.That(warehouse.Observers, Does.Contain(factory));
        }

        [Test]
        public void RawMaterialPlantProducesComponentAfterInterval() {
            var buildingList = LoadBuildings("SimulationTests/config2.xml");
            var warehouse = buildingList.OfType<Warehouse>().First();
            var factory = buildingList.OfType<RawMatPlant>().First();

            factory.NotifyStart();
            factory.ExecuteRoutine();

            Assert.That(warehouse.Transport, Has.Count.EqualTo(1));
            Assert.That(warehouse.Transport[0].Type, Is.EqualTo(ProductionType.METAL));
        }

        [Test]
        public void ComponentDeliveryMovesItemFromTransportToInventory() {
            var buildingList = LoadBuildings("SimulationTests/config2.xml");
            var warehouse = buildingList.OfType<Warehouse>().First();
            var factory = buildingList.OfType<RawMatPlant>().First();
            var component = new Component(ProductionType.METAL, warehouse, factory);
            warehouse.Transport.Add(component);

            for (var i = 0; i < 500 && warehouse.Transport.Count > 0; i++) {
                component.ExecuteRoutine();
            }

            Assert.That(warehouse.Transport, Is.Empty);
            Assert.That(warehouse.Inventory, Has.Count.EqualTo(1));
            Assert.That(warehouse.Inventory[0], Is.SameAs(component));
        }

        private static List<BuildingBase> LoadBuildings(string relativeConfigPath) {
            var configPath = Path.Combine(AppContext.BaseDirectory, relativeConfigPath);
            var reader = new XmlReaderService(configPath);
            var metadataList = MetadataXmlParser.Parse(reader.GetNodesByTag("metadonnees"));
            var buildingList = BuildingXmlParser.Parse(reader.GetNodesByTag("simulation"), metadataList);
            PathXmlParser.Parse(reader.GetNodesByTag("chemin"), buildingList);
            return buildingList;
        }
    }
}
