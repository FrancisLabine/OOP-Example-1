// <copyright file="IXmlReader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Core.Models.Utils.Xml
{
    using System.Xml;

    public interface IXmlReader
    {
        XmlNodeList GetNodesByTag(string tagName);
    }
}
