using GraphEditor.Business.Models;
using System.Xml.Serialization;

namespace GraphEditor.Business.Models.Xml
{
    public class XmlRectangle
    {
        [XmlAttribute]
        public int Left { get; set; }

        [XmlAttribute]
        public int Top { get; set; }

        [XmlAttribute]
        public int Width { get; set; }

        [XmlAttribute]
        public int Height { get; set; }

        [XmlAttribute]
        public ShapeType ShapeType { get; set; }

        [XmlElement]
        public XmlColor FillColor { get; set; }

        [XmlElement]
        public XmlColor BorderColor { get; set; }
    }
}
