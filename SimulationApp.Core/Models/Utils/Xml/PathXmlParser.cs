// <copyright file="PathXmlParser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Core.Models.Utils.Xml {
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using SimulationApp.Core.Models.Domain.Buildings;
    using SimulationApp.Core.Models.Domain.Buildings.Pathways;

    public static class PathXmlParser
    {
        public static List<Pathway> Parse(XmlNodeList pathNodes, List<BuildingBase> buildings)
        {
            var paths = new List<Pathway>();

            foreach (XmlNode node in pathNodes)
            {
                if (node.Attributes == null)
                {
                    continue;
                }

                var fromId = node.Attributes["de"]?.Value;
                var toId = node.Attributes["vers"]?.Value;

                if (string.IsNullOrWhiteSpace(fromId) || string.IsNullOrWhiteSpace(toId))
                {
                    continue;
                }

                var fromBuilding = buildings.FirstOrDefault(b => b.Id == fromId);
                var toBuilding = buildings.FirstOrDefault(b => b.Id == toId);

                if (fromBuilding == null || toBuilding == null)
                {
                    continue;
                }

                // Link them
                fromBuilding.LinkedBuilding = toBuilding;
                toBuilding.Observers.Add(fromBuilding);

                // Build visual Pathway (coordinates are usually offset to center visuals)
                var path = new Pathway
                {
                    X1 = fromBuilding.PosX + 15,
                    Y1 = fromBuilding.PosY + 15,
                    X2 = toBuilding.PosX + 15,
                    Y2 = toBuilding.PosY + 15,
                };

                paths.Add(path);
            }

            return paths;
        }
    }
}
