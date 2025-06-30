using System;
using System.Collections.Generic;
using System.Xml;
using SimulationApp.Core.Models.Domain.Buildings;

namespace SimulationApp.Core.Models.Utils.Xml {
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
                        Console.WriteLine($"No metadata found for building type: {type}");
                        continue;
                    }

                    var building = BuildingFactory.CreateBuilding(type, id, x, y, metadata);
                    if (building is not null) {
                        buildings.Add(building);
                    }
                }
            }

            return buildings;
        }
    }
}
