// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp
{
    using System;
    using System.Xml;
    using SimulationApp.Domain.Shared;
    using SimulationApp.Domain.Warehouses;
    using SimulationApp.Infrastructure.Xml;

    public static class Program
    {
        public static int Strategy { get; set; } = 0;

        public static string ConfigPath { get; set; } = "./src/Ressources/configurations/config1.xml";

        public static void Main(string[] args)
        {
            Console.WriteLine("Starting simulation test...");

            // Step 1: Create the XML reader
            var reader = new XmlReaderService(ConfigPath);

            // Step 2: Load metadata
            XmlNodeList metadataNodes = reader.GetNodesByTag("metadonnees");
            var metadataList = MetadataXmlParser.Parse(metadataNodes);
            Console.WriteLine($"Metadata loaded: {metadataList.Count}");

            // Step 3: Load buildings
            XmlNodeList simulationNodes = reader.GetNodesByTag("simulation");
            var buildingList = BuildingXmlParser.Parse(simulationNodes, metadataList);
            Console.WriteLine($"Buildings loaded: {buildingList.Count}");

            // Step 4: Display a simple Warehouse for test
            foreach (var building in buildingList)
            {
                if (building is Warehouse warehouse)
                {
                    Console.WriteLine("Warehouse created:");
                    Console.WriteLine($"   ID: {warehouse.Id}");
                    Console.WriteLine($"   Position: ({warehouse.PosX}, {warehouse.PosY})");
                    Console.WriteLine($"   Metadata: Type = {warehouse.BuildingMetadata?.Type}");
                    break;
                }
            }

            Console.WriteLine("Test complete.");
        }
    }
}
