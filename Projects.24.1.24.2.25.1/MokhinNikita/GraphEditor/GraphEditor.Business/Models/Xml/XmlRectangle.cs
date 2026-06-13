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
        [XmlAttribute(nameof(Left))]
        public int Left { get; set; }
        [XmlAttribute(nameof(Top))]
        public int Top { get; set; }
        [XmlAttribute(nameof(Width))]
        public int Width { get; set; }
        [XmlAttribute(nameof(Height))]
        public int Height { get; set; }

        [XmlElement(nameof(FillColor))]
        public XmlColor FillColor { get; set; }

        [XmlElement(nameof(BorderColor))]
        public XmlColor BorderColor { get; set; }
        [XmlAttribute(nameof(BorderWidth))]
        public int BorderWidth { get; set; }

        [XmlIgnore]
        public FigureType Type { get; set; }

        [XmlAttribute(nameof(Type))]
        public string TypeName => Type.ToString();

        [XmlAttribute(nameof(Opacity))]
        public byte Opacity { get; set; }
    }
}
