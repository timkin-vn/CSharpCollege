using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    [Serializable]
    [XmlRoot("Cards")]
    public class XmlCardCollection
    {
        [XmlAttribute("CurrentId")]
        public int CurrentId { get; set; }

        [XmlArray("Cards")]
        [XmlArrayItem("Card")]
        public List<XmlCard> Cards { get; set; } = new List<XmlCard>();
    }
}