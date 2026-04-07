using System.Collections.Generic;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    [XmlRoot("LetterCollection")]
    public class XmlLetterCollection
    {
        [XmlAttribute("NextId")] public int NextId { get; set; }
        [XmlArray("Letters")][XmlArrayItem("Letter")] public List<XmlLetter> Letters { get; set; } = new List<XmlLetter>();
    }
}