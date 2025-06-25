// <copyright file="XmlReaderService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp.Infrastructure.Xml
{
    using System;
    using System.IO;
    using System.Xml;

    /// <summary>
    /// Reads and parses the selected XML configuration file.
    /// </summary>
    public class XmlReaderService : IXmlReader
    {
        private readonly string configPath;
        private XmlDocument? document;

        public XmlReaderService(string configPath)
        {
            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException($"Configuration file not found: {configPath}");
            }

            this.configPath = configPath;
        }

        public XmlNodeList GetNodesByTag(string tagName)
        {
            this.EnsureDocumentLoaded();
            this.document!.DocumentElement!.Normalize();
            return this.document.GetElementsByTagName(tagName);
        }

        private void EnsureDocumentLoaded()
        {
            if (this.document != null)
            {
                return;
            }

            try
            {
                var doc = new XmlDocument();
                doc.Load(this.configPath);
                this.document = doc;
            }
            catch (XmlException ex)
            {
                Console.Error.WriteLine($"XML parsing error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }
    }
}
