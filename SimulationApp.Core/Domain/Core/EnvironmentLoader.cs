// <copyright file="EnvironmentLoader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Core.Domain.Core
{
    using SimulationApp.Core.Infrastructure.Xml;

    public class EnvironmentLoader
    {
        private readonly IXmlReader xmlReader;

        public EnvironmentLoader(IXmlReader xmlReader)
        {
            this.xmlReader = xmlReader;
        }

        public EnvironmentModel Load()
        {
            var metadata = MetadataXmlParser.Parse(xmlReader.GetNodesByTag("metadonnees"));
            var buildings = BuildingXmlParser.Parse(xmlReader.GetNodesByTag("simulation"), metadata);
            var paths = PathXmlParser.Parse(xmlReader.GetNodesByTag("chemin"), buildings);

            return new EnvironmentModel
            {
                Buildings = buildings,
                Paths = paths,
                Metadata = metadata,
            };
        }
    }
}