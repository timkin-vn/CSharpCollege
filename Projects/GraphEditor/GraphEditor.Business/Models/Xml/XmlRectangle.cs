using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GraphEditor.Business.Models.Xml;

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

    [XmlElement("FillColor")]
    public XmlColor FillColor { get; set; }

    [XmlElement("BorderColor")]
    public XmlColor BorderColor { get; set; }
}
