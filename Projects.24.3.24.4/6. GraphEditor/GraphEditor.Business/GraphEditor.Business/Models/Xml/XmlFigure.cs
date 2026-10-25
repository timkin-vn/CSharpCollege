using System;
using System.Xml.Serialization;

namespace GraphEditor.Business.Models.Xml
{
    [Serializable]
    [XmlInclude(typeof(XmlRectangle))]
    [XmlInclude(typeof(XmlCircle))]
    [XmlInclude(typeof(XmlTriangle))]
    public abstract class XmlFigure
    {
        [XmlAttribute("Left")] public int Left { get; set; }
        [XmlAttribute("Top")] public int Top { get; set; }
        [XmlAttribute("Width")] public int Width { get; set; }
        [XmlAttribute("Height")] public int Height { get; set; }

        [XmlElement("FillColor")] public XmlColor FillColor { get; set; }
        [XmlElement("BorderColor")] public XmlColor BorderColor { get; set; }
    }

    public class XmlRectangle : XmlFigure { }
    public class XmlCircle : XmlFigure { }
    public class XmlTriangle : XmlFigure { }
}