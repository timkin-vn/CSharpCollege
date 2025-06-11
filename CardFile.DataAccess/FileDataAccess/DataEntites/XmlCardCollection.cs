using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.DataEntites
{
    [Serializable]
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
