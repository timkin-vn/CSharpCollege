using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphEditor.Business.Models.Xml
{
    [Serializable]
    public class XmlCircle
    {
        [XmlAttribute("Left")]
        public int Left { get; set; }

        [XmlAttribute("Top")]
        public int Top { get; set; }

        [XmlAttribute("Diameter")]
        public int Diameter { get; set; }

        [XmlElement("FillColor")]
        public XmlColor FillColor { get; set; }

        [XmlElement("BorderColor")]
        public XmlColor BorderColor { get; set; }
    }
}
