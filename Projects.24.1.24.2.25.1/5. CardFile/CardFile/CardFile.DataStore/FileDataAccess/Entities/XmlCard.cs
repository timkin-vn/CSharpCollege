using System;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("Title")]
        public string Title { get; set; }

        [XmlElement("Text")]
        public string Text { get; set; }

        [XmlAttribute("Category")]
        public string Category { get; set; }

        [XmlIgnore]
        public DateTime CreatedAt { get; set; }

        [XmlAttribute("CreatedAt")]
        public string CreatedAtText
        {
            get => CreatedAt.ToShortDateString();
            set => CreatedAt = DateTime.Parse(value);
        }

        [XmlAttribute("IsDone")]
        public bool IsDone { get; set; }

        [XmlAttribute("IsPinned")]
        public bool IsPinned { get; set; }
    }
}