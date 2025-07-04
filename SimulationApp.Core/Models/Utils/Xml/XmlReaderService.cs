using System;
using System.IO;
using System.Xml;

namespace SimulationApp.Core.Models.Utils.Xml {

    /// <summary>
    /// Reads and parses the selected XML configuration file.
    /// </summary>
    public class XmlReaderService : IXmlReader {
        private readonly string configPath;
        private XmlDocument document;

        public XmlReaderService(string configPath) {
            if (!File.Exists(configPath)) {
                throw new FileNotFoundException($"Configuration file not found: {configPath}");
            }

            this.configPath = configPath;
        }

        public XmlNodeList GetNodesByTag(string tagName) {
            EnsureDocumentLoaded();
            document!.DocumentElement!.Normalize();
            return document.GetElementsByTagName(tagName);
        }

        private void EnsureDocumentLoaded() {
            if (document != null) {
                return;
            }

            try {
                var doc = new XmlDocument();
                doc.Load(configPath);
                document = doc;
            } catch (XmlException ex) {
                Console.Error.WriteLine($"XML parsing error: {ex.Message}");
                throw;
            } catch (Exception ex) {
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }
    }
}
