using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace GraphEditor.Business.Models.Xml
{
    [Serializable]
    public class XmlRectangle
    {
        [XmlAttribute("Left")]
        public int Left { get; set; }

        [XmlAttribute("Top")]
        public int Top { get; set; }

        [XmlAttribute("Width")]
        public int Width { get; set; }

        [XmlAttribute("Height")]
        public int Height { get; set; }

        [XmlAttribute("Opacity")]
        public int Opacity { get; set; } = 255;

        [XmlAttribute("CornerRadius")]
        public int CornerRadius { get; set; } = 0;

        [XmlElement("FillColor")]
        public XmlColor FillColor { get; set; }

        [XmlElement("BorderColor")]
        public XmlColor BorderColor { get; set; }
    }
}