using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites;

[Serializable]
public class XmlCard {
    [XmlAttribute("Id")]
    public int Id { get; set; }
    
    [XmlAttribute("Title")]
    public string Title { get; set; }

    [XmlAttribute("OriginalTitle")]
    public string OriginalTitle { get; set; }

    [XmlAttribute("Genre")]
    public string Genre { get; set; }

    [XmlIgnore]
    public DateTime ReleaseDate { get; set; }

    [XmlAttribute("ReleaseDate")]
    public string ReleaseDateXml {
        get => ReleaseDate.ToShortDateString();
        set => ReleaseDate = DateTime.Parse(value);
    }

    [XmlAttribute("Studio")]
    public string Studio { get; set; }

    [XmlAttribute("Director")]
    public string Director { get; set; }

    [XmlIgnore]
    public DateTime? EndDate { get; set; }

    [XmlAttribute("EndDate")]
    public string EndDateXml {
        get => EndDate.HasValue ? EndDate.Value.ToShortDateString() : "-";
        set => EndDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
    }

    [XmlAttribute("Rating")]
    public decimal Rating { get; set; }
}