using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Industry")]
        public string Industry { get; set; }

        [XmlAttribute("FoundationYear")]
        public int FoundationYear { get; set; }

        [XmlAttribute("Revenue")]
        public decimal Revenue { get; set; }

        [XmlAttribute("Employees")]
        public int Employees { get; set; }

        [XmlAttribute("Description")]
        public string Description { get; set; }
    }
}