using System.Xml.Serialization;

namespace GraphEditor.Business.Models.Xml;

[Serializable]
public class XmlGroup {
    [XmlAttribute("Id")]
    public Guid Id { get; set; }

    [XmlAttribute("Name")]
    public string? Name { get; set; }

    [XmlArray("Members")]
    [XmlArrayItem("RectangleId")]
    public List<Guid> RectangleIds { get; set; } = new();
}