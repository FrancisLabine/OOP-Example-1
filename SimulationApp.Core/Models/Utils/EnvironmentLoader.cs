namespace SimulationApp.Core.Models.Utils {
    using SimulationApp.Core.Models.Domain;
    using SimulationApp.Core.Models.Utils.Xml;

    public class EnvironmentLoader {
        private readonly IXmlReader xmlReader;

        public EnvironmentLoader(IXmlReader xmlReader) {
            this.xmlReader = xmlReader;
        }

        public EnvironmentModel Load() {
            var metadata = MetadataXmlParser.Parse(xmlReader.GetNodesByTag("metadonnees"));
            var buildings = BuildingXmlParser.Parse(xmlReader.GetNodesByTag("simulation"), metadata);
            var paths = PathXmlParser.Parse(xmlReader.GetNodesByTag("chemin"), buildings);

            return new EnvironmentModel {
                Buildings = buildings,
                Paths = paths,
                Metadata = metadata,
            };
        }
    }
}