using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LimerList.DataStore.FileAccess.Entities
{
    [XmlRoot("LimerCollection")]
    public class XmlLimerCollection
    {
        [XmlAttribute("NextId")]
        public int NextId { get; set; }

        [XmlArray("LimerCollection.Items"), XmlArrayItem("Item")]
        public List<XmlLimer> Limers { get; set; } = new List<XmlLimer>();
    }
}
