using System.Xml.Serialization;

namespace GraphEditor.Business.Models.Xml;

[Serializable]
public class XmlRectangle {
    [XmlAttribute("Id")]
    public Guid Id { get; set; }
    
    [XmlAttribute("Left")]
    public int Left { get; set; }

    [XmlAttribute("Top")]
    public int Top { get; set; }

    [XmlAttribute("Width")]
    public int Width { get; set; }

    [XmlAttribute("Height")]
    public int Height { get; set; }

    [XmlElement("FillColor")]
    public XmlColor? FillColor { get; set; }

    [XmlElement("BorderColor")]
    public XmlColor? BorderColor { get; set; }
        
    [XmlElement("BorderWidth")]
    public float  BorderWidth { get; set; }
    
    [XmlElement("Text")]
    public string? Text { get; set; }
    
    [XmlElement("TextColor")]
    public XmlColor? TextColor { get; set; }
    
    [XmlElement("FontFamily")]
    public string? FontFamily { get; set; }
    
    [XmlElement("FontSize")]
    public float  FontSize { get; set; }
    
    [XmlElement("TextAlign")]
    public string? TextAlign { get; set; }
}