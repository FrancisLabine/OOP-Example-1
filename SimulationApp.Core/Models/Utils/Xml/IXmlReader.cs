using System.Xml;
namespace SimulationApp.Core.Models.Utils.Xml {
    public interface IXmlReader {
        XmlNodeList GetNodesByTag(string tagName);
    }
}
