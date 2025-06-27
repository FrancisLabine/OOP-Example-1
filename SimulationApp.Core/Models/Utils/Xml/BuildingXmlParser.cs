// <copyright file="BuildingXmlParser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Core.Models.Infrastructure.Xml {
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using SimulationApp.Core.Domain.Plants;
    using SimulationApp.Core.Domain.Warehouses;
    using SimulationApp.Core.Models.Domain.Buildings;

    public static class BuildingXmlParser
    {
        public static List<BuildingBase> Parse(XmlNodeList simulationNodes, List<BuildingMetadata> metadataList)
        {
            var buildings = new List<BuildingBase>();

            foreach (XmlNode node in simulationNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.Name != "usine")
                    {
                        continue;
                    }

                    var element = (XmlElement)child;

                    string id = element.GetAttribute("id");
                    string type = element.GetAttribute("type");
                    int x = int.Parse(element.GetAttribute("x"));
                    int y = int.Parse(element.GetAttribute("y"));

                    var metadata = metadataList.Find(m => m.Type == type);

                    if (metadata == null)
                    {
                        Console.WriteLine($"⚠️  No metadata found for building type: {type}");
                        continue;
                    }

                    BuildingBase building = type switch
                    {
                        "usine-matiere" => new RawMatPlant(id, x, y, metadata),
                        "usine-aile" => new ProductionPlant(id, x, y, metadata),
                        "usine-assemblage" => new AssemblyPlant(id, x, y, metadata),
                        "usine-moteur" => new ProductionPlant(id, x, y, metadata),
                        "entrepot" => new Warehouse(id, x, y, metadata),
                        _ => null
                    };

                    if (building != null)
                    {
                        buildings.Add(building);
                    }
                }
            }

            return buildings;
        }
    }
}
