using System.Collections.Generic;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    [XmlRoot("CardCollection")]
    public class XmlCardCollection
    {
        [XmlAttribute("NextId")]
        public int NextId { get; set; }

        [XmlArray("Cards")]
        [XmlArrayItem("Card")]
        public List<XmlCard> Cards { get; set; } = new List<XmlCard>();
    }
}
