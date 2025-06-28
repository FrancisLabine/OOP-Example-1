namespace SimulationApp {
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using NUnit.Framework;
    using SimulationApp.Core.Models.Domain.Buildings;
    using SimulationApp.Core.Models.Domain.Buildings.Plants;
    using SimulationApp.Core.Models.Domain.Buildings.Warehouses;
    using SimulationApp.Core.Models.Domain.Components;
    using SimulationApp.Core.Models.Utils.Xml;

    [TestFixture]
    public class WarehouseTests
    {
        private string configPath;

        [Test]
        public void ShouldReturnWarehouseIcons()
        {
            configPath = Path.Combine(AppContext.BaseDirectory, "../../../SimulationApp.Tests/SimulationTests/config1.xml");

            var reader = new XmlReaderService(configPath);

            XmlNodeList metadataNodes = reader.GetNodesByTag("metadonnees");
            var metadataList = MetadataXmlParser.Parse(metadataNodes);

            XmlNodeList simulationNodes = reader.GetNodesByTag("simulation");
            var buildingList = BuildingXmlParser.Parse(simulationNodes, metadataList);

            var warehouse1 = buildingList.OfType<Warehouse>().ToList()[0];
            var warehouse2 = buildingList.OfType<Warehouse>().ToList()[1];

            var iconEmpty = warehouse1.GetStatusIcon();
            Assert.That(iconEmpty, Is.EqualTo("./src/Ressources/UT0_.png"), "Expecting ./src/Ressources/UT0_.png (Warehouse Empty)");

            warehouse1.ReceiveComponent(new Component(ProductionType.AVION, warehouse1, warehouse2));
            iconEmpty = warehouse1.GetStatusIcon();

            Assert.That(iconEmpty, Is.EqualTo("./src/Ressources/UT33_.png"), "Expecting ./src/Ressources/UT1_.png (Warehouse Low)");

            warehouse1.ReceiveComponent(new Component(ProductionType.AVION, warehouse1, warehouse2));
            iconEmpty = warehouse1.GetStatusIcon();
            Assert.That(iconEmpty, Is.EqualTo("./src/Ressources/UT66_.png"), "Expecting ./src/Ressources/UT2_.png (Warehouse Medium)");

            warehouse1.ReceiveComponent(new Component(ProductionType.AVION, warehouse1, warehouse2));
            iconEmpty = warehouse1.GetStatusIcon();
            Assert.That(iconEmpty, Is.EqualTo("./src/Ressources/UT100_.png"), "Expecting ./src/Ressources/UT3_.png (Warehouse Full)");
        }

        [Test]
        public void ShouldLoadRawMatFactoryFromXmlCorrectly()
        {
            configPath = Path.Combine(AppContext.BaseDirectory, "../../../SimulationApp.Tests/SimulationTests/config2.xml");

            // Step 1: Create the XML reader
            var reader = new XmlReaderService(configPath);

            // Step 2: Load metadata
            XmlNodeList metadataNodes = reader.GetNodesByTag("metadonnees");
            var metadataList = MetadataXmlParser.Parse(metadataNodes);
            Assert.That(metadataList.Count, Is.EqualTo(2), "Expected exactly 2 metadata entry.");

            // Step 3: Load buildings
            XmlNodeList simulationNodes = reader.GetNodesByTag("simulation");
            var buildingList = BuildingXmlParser.Parse(simulationNodes, metadataList);
            Assert.That(buildingList.Count, Is.EqualTo(2), "Expected exactly 2 building.");

            // Load paths
            var pathNodes = reader.GetNodesByTag("chemin");
            var paths = PathXmlParser.Parse(pathNodes, buildingList);

            Assert.That(paths.ToList().Count, Is.EqualTo(1), "Expected exactly 1 path.");

            var warehouse = buildingList.OfType<Warehouse>().FirstOrDefault();
            var factory = buildingList.OfType<RawMatPlant>().FirstOrDefault();

            Assert.That(factory.LinkedBuilding, Is.SameAs(warehouse), "Expected Factory to be linked to Warehouse.");
            Assert.That(warehouse.Observers.ToList()[0], Is.SameAs(factory), "Expected Warehouse to observe Factory.");

            Assert.That(warehouse.Transport.Count, Is.EqualTo(0), "Expected exactly 0 RawMat in transit.");
            Assert.That(factory.ProductionTime, Is.EqualTo(-1), "Expected exactly -1 ProductionTime.");
            var input = warehouse.BuildingMetadata.Input1;
            var maxCapacity = warehouse.BuildingMetadata?.InputQuantity1 ?? 0;
            var currentLoad = warehouse.Inventory.Count + warehouse.Transport.Count;

            warehouse.ExecuteRoutine();

            Assert.That(factory.ProductionTime, Is.EqualTo(0), "Expected exactly 0 ProductionTime.");
            factory.ExecuteRoutine();
            Assert.That(warehouse.Transport.Count, Is.EqualTo(1), "Expected exactly 1 RawMat in transit.");
        }
    }
}
