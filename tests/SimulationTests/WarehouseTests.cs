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
    public class WarehouseTests
    {
        private string ConfigPath = Path.Combine(AppContext.BaseDirectory, "../../../tests/SimulationTests/config1.xml");


        [Test]
        public void ShouldReturnWarehouseIcons()
        {
            var reader = new XmlReaderService(ConfigPath);

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
    }
}
