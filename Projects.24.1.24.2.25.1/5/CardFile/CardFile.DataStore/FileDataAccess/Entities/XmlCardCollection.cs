using System.Collections.Generic;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    [XmlRoot("CardCollection")]
    public class XmlCardCollection
    {
        [XmlAttribute("NextId")]
        public int NextId { get; set; }

        [XmlArray("Heroes")]
        [XmlArrayItem("Hero")]
        public List<string> Heroes { get; set; } = new List<string>();

        [XmlArray("Items")]
        [XmlArrayItem("Item")]
        public List<string> Items { get; set; } = new List<string>();

        [XmlArray("Neutrals")]
        [XmlArrayItem("Neutral")]
        public List<string> Neutrals { get; set; } = new List<string>();

        [XmlArray("Cards")]
        [XmlArrayItem("Card")]
        public List<XmlCard> Cards { get; set; } = new List<XmlCard>();
    }
}
