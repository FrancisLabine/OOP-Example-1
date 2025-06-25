// <copyright file="EnvironmentLoader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Domain.Core
{
    using SimulationApp.Infrastructure.Xml;

    public class EnvironmentLoader
    {
        private readonly IXmlReader xmlReader;

        public EnvironmentLoader(IXmlReader xmlReader)
        {
            this.xmlReader = xmlReader;
        }

        public EnvironmentModel Load()
        {
            var metadata = MetadataXmlParser.Parse(this.xmlReader.GetNodesByTag("metadonnees"));
            var buildings = BuildingXmlParser.Parse(this.xmlReader.GetNodesByTag("simulation"), metadata);
            var paths = PathXmlParser.Parse(this.xmlReader.GetNodesByTag("chemin"), buildings);

            return new EnvironmentModel
            {
                Buildings = buildings,
                Paths = paths,
                Metadata = metadata,
            };
        }
    }
}