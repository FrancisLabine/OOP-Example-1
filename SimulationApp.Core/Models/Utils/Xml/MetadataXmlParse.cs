// <copyright file="MetadataXmlParse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Core.Models.Utils.Xml {
    using System.Collections.Generic;
    using System.Xml;
    using SimulationApp.Core.Models.Domain.Buildings;

    public static class MetadataXmlParser
    {
        public static List<BuildingMetadata> Parse(XmlNodeList metadataNodes)
        {
            var result = new List<BuildingMetadata>();

            foreach (XmlNode node in metadataNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.Name != "usine")
                    {
                        continue;
                    }

                    var element = (XmlElement)child;
                    var type = element.GetAttribute("type");

                    if (string.IsNullOrWhiteSpace(type) || result.Exists(md => md.Type == type))
                    {
                        continue;
                    }

                    var metadata = new BuildingMetadata { Type = type };

                    foreach (XmlNode config in child.ChildNodes)
                    {
                        switch (config.Name)
                        {
                            case "icones":
                                foreach (XmlNode iconNode in config.ChildNodes)
                                {
                                    var iconElement = (XmlElement)iconNode;
                                    switch (iconElement.GetAttribute("type"))
                                    {
                                        case "vide": metadata.IconEmpty = iconElement.GetAttribute("path"); break;
                                        case "un-tiers": metadata.IconLow = iconElement.GetAttribute("path"); break;
                                        case "deux-tiers": metadata.IconMedium = iconElement.GetAttribute("path"); break;
                                        case "plein": metadata.IconFull = iconElement.GetAttribute("path"); break;
                                    }
                                }

                                break;

                            case "entree":
                                var eElement = (XmlElement)config;
                                if (metadata.Input1 == null)
                                {
                                    metadata.Input1 = eElement.GetAttribute("type");
                                    metadata.InputQuantity1 = int.TryParse(eElement.GetAttribute("quantite"), out int q1) ? q1 : 0;
                                }
                                else
                                {
                                    metadata.Input2 = eElement.GetAttribute("type");
                                    metadata.InputQuantity2 = int.TryParse(eElement.GetAttribute("quantite"), out int q2) ? q2 : 0;
                                }

                                break;

                            case "sortie":
                                metadata.Output = ((XmlElement)config).GetAttribute("type");
                                break;

                            case "interval-production":
                                int.TryParse(config.InnerText, out int interval);
                                metadata.Interval = interval;
                                break;
                        }
                    }

                    result.Add(metadata);
                }
            }

            return result;
        }
    }
}
