using System.Xml.Serialization;

namespace GraphEditor.Business.Models.Xml;

[Serializable]
[XmlRoot("Picture")]
public class XmlPicture {
    [XmlArray("Figures")]
    [XmlArrayItem("Figure")]
    public required List<XmlRectangle> Rectangles { get; set; }
        
    [XmlArray("Groups")]
    [XmlArrayItem("Group")]
    public List<XmlGroup> Groups { get; set; } = new();
}