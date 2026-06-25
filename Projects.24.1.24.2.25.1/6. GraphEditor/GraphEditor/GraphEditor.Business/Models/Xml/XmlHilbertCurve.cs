using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphEditor.Business.Models.Xml
{
    [Serializable]
    public class XmlHilbertCurve
    {
        [XmlAttribute("X")]
        public int X { get; set; }

        [XmlAttribute("Y")]
        public int Y { get; set; }

        [XmlAttribute("Size")]
        public int Size { get; set; }

        [XmlAttribute("Order")]
        public int Order { get; set; }

        [XmlElement("FillColor")]
        public XmlColor FillColor { get; set; }

        [XmlElement("BorderColor")]
        public XmlColor BorderColor { get; set; }
    }
}