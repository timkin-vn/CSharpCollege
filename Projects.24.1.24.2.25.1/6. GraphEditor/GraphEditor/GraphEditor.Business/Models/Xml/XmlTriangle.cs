using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphEditor.Business.Models.Xml
{
    [Serializable]
    public class XmlTriangle
    {
        [XmlAttribute("X1")]
        public int X1 { get; set; }

        [XmlAttribute("Y1")]
        public int Y1 { get; set; }

        [XmlAttribute("X2")]
        public int X2 { get; set; }

        [XmlAttribute("Y2")]
        public int Y2 { get; set; }

        [XmlAttribute("X3")]
        public int X3 { get; set; }

        [XmlAttribute("Y3")]
        public int Y3 { get; set; }

        [XmlElement("FillColor")]
        public XmlColor FillColor { get; set; }

        [XmlElement("BorderColor")]
        public XmlColor BorderColor { get; set; }
    }
}