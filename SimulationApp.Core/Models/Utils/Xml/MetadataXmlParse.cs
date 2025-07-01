using System.Collections.Generic;
using System.Xml;
using SimulationApp.Core.Models.Domain.Buildings;

namespace SimulationApp.Core.Models.Utils.Xml {
    public static class MetadataXmlParser
    {
        public static List<BuildingMetadata> Parse(XmlNodeList metadataNodes)
        {
            var result = new List<BuildingMetadata>();

            foreach (XmlNode node in metadataNodes) {
                foreach (XmlNode child in node.ChildNodes) {
                    if (child.Name != "usine") {
                        continue;
                    }

                    var element = (XmlElement)child;
                    var type = element.GetAttribute("type");

                    if (string.IsNullOrWhiteSpace(type) || result.Exists(md => md.Type == type)) {
                        continue;
                    }

                    string input1 = null, input2 = null;
                    int? qty1 = null, qty2 = null;
                    string output = null;
                    int? interval = null;
                    string iconEmpty = null, iconLow = null, iconMedium = null, iconFull = null;

                    foreach (XmlNode config in child.ChildNodes) {
                        switch (config.Name) {
                            case "icones":
                                foreach (XmlNode iconNode in config.ChildNodes) {
                                    var iconElement = (XmlElement)iconNode;
                                    string iconPath = iconElement.GetAttribute("path");
                                    switch (iconElement.GetAttribute("type")) {
                                        case "vide": iconEmpty = iconPath; break;
                                        case "un-tiers": iconLow = iconPath; break;
                                        case "deux-tiers": iconMedium = iconPath; break;
                                        case "plein": iconFull = iconPath; break;
                                    }
                                }
                                break;

                            case "entree":
                                var eElement = (XmlElement)config;
                                string entType = eElement.GetAttribute("type");
                                int qty = int.TryParse(eElement.GetAttribute("quantite"), out var q) ? q : 0;

                                if (input1 == null) {
                                    input1 = entType;
                                    qty1 = qty;
                                } else {
                                    input2 = entType;
                                    qty2 = qty;
                                }
                                break;

                            case "sortie":
                                output = ((XmlElement)config).GetAttribute("type");
                                break;

                            case "interval-production":
                                interval = int.TryParse(config.InnerText, out var parsed) ? parsed : null;
                                break;
                        }
                    }

                    var metadata = new BuildingMetadata {
                        Type = type,
                        Input1 = input1,
                        InputQuantity1 = qty1,
                        Input2 = input2,
                        InputQuantity2 = qty2,
                        Output = output,
                        Interval = interval,
                        IconEmpty = iconEmpty,
                        IconLow = iconLow,
                        IconMedium = iconMedium,
                        IconFull = iconFull,
                    };

                    result.Add(metadata);
                }
            }

            return result;
        }
    }
}
