// <copyright file="SimulationTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>


namespace SimulationApp.XmlTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using NUnit.Framework;
    using SimulationApp.Domain.Factories;
    using SimulationApp.Domain.Shared;
    using SimulationApp.Domain.Warehouses;
    using SimulationApp.Infrastructure.Xml;

    [TestFixture]
    public class XmlTests
    {
        private string configPath;

        [Test]
        public void ShouldLoadWarehouseFromXmlCorrectly()
        {
            configPath = Path.Combine(AppContext.BaseDirectory, "../../../tests/XmlTests/config_WH.xml");
            // Step 1: Create the XML reader
            var reader = new XmlReaderService(configPath);

            // Step 2: Load metadata
            XmlNodeList metadataNodes = reader.GetNodesByTag("metadonnees");
            var metadataList = MetadataXmlParser.Parse(metadataNodes);
            Assert.That(metadataList.Count, Is.EqualTo(1), "Expected exactly 1 metadata entry.");

            // Step 3: Load buildings
            XmlNodeList simulationNodes = reader.GetNodesByTag("simulation");
            var buildingList = BuildingXmlParser.Parse(simulationNodes, metadataList);
            Assert.That(buildingList.Count, Is.EqualTo(1), "Expected exactly 1 building.");

            // Step 4: Verify the warehouse
            var warehouse = buildingList.OfType<Warehouse>().FirstOrDefault();
            Assert.That(warehouse, Is.Not.Null, "Expected a warehouse to be created.");

            Assert.That(warehouse.Id, Is.EqualTo("warehouse_1"));
            Assert.That(warehouse.PosX, Is.EqualTo(100));
            Assert.That(warehouse.PosY, Is.EqualTo(100));
            Assert.That(warehouse.BuildingMetadata?.Type, Is.EqualTo("entrepot"));
        }


        [Test]
        public void ShouldLoadRawMatFactoryFromXmlCorrectly()
        {
            configPath = Path.Combine(AppContext.BaseDirectory, "../../../tests/XmlTests/config_RawMatFac.xml");

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

            var warehouse = buildingList.OfType<Warehouse>().FirstOrDefault();
            var factory = buildingList.OfType<RawMatFactory>().FirstOrDefault();
            Assert.That(warehouse, Is.Not.Null, "Expected a warehouse to be created.");
            Assert.That(factory, Is.Not.Null, "Expected a factory to be created.");

            Assert.That(warehouse.Id, Is.EqualTo("21"));
            Assert.That(warehouse.BuildingMetadata?.Type, Is.EqualTo("entrepot"));
            Assert.That(factory.Id, Is.EqualTo("11"));
            Assert.That(factory.BuildingMetadata?.Type, Is.EqualTo("usine-matiere"));
        }
    }
}
