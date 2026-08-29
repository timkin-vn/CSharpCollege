using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphEditor.Business.Models.File
{
    [Serializable]
    public class XmlRectangleModel
    {
        [XmlAttribute("X")]
        public int X { get; set; }

        [XmlAttribute("Y")]
        public int Y { get; set; }

        [XmlAttribute("Width")]
        public int Width { get; set; }

        [XmlAttribute("Height")]
        public int Height { get; set; }

        [XmlElement("FillColor")]
        public XmlColorModel FillColor { get; set; }

        [XmlElement("DrawColor")]
        public XmlColorModel DrawColor { get; set; }
    }
}
