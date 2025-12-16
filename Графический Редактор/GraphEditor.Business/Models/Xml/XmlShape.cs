using GraphEditor.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphEditor.Business.Models.Xml
{
    [Serializable]
    public class XmlShape
    {
        [XmlAttribute("Left")]
        public int Left { get; set; }

        [XmlAttribute("Top")]
        public int Top { get; set; }

        [XmlAttribute("Width")]
        public int Width { get; set; }

        [XmlAttribute("Height")]
        public int Height { get; set; }

        [XmlAttribute("BorderThickness")]
        public int BorderThickness { get; set; } = 3;

        [XmlAttribute("ShapeType")]
        public ShapeType ShapeType { get; set; } = ShapeType.Rectangle;

        // Добавляем свойство направления треугольника
        [XmlAttribute("TrianglePointsUp")]
        public bool TrianglePointsUp { get; set; } = true;

        [XmlElement("FillColor")]
        public XmlColor FillColor { get; set; }

        [XmlElement("BorderColor")]
        public XmlColor BorderColor { get; set; }
    }
}