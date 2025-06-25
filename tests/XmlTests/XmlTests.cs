// <copyright file="SimulationTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using System.IO;
    using NUnit.Framework;
    using SimulationApp.Domain.Shared;
    using SimulationApp.Domain.Warehouses;
    using SimulationApp.Infrastructure.Xml;

    [TestFixture]
    public class XmlTests
    {
        private string ConfigPath = Path.Combine(AppContext.BaseDirectory, "../../../tests/XmlTests/config1.xml");


        [Test]
        public void ShouldLoadWarehouseFromXmlCorrectly()
        {
            // Step 1: Create the XML reader
            var reader = new XmlReaderService(ConfigPath);

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
    }
}
