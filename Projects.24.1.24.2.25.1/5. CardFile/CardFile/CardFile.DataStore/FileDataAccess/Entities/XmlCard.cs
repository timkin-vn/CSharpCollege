using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("Title")]
        public string Title { get; set; } = string.Empty;

        [XmlAttribute("Director")]
        public string Director { get; set; } = string.Empty;

        [XmlAttribute("ReleaseYear")]
        public int ReleaseYear { get; set; }

        [XmlAttribute("Genre")]
        public string Genre { get; set; } = string.Empty;

        [XmlAttribute("DurationMinutes")]
        public decimal DurationMinutes { get; set; }

        [XmlAttribute("Rating")]
        public int Rating { get; set; }

        [XmlAttribute("IsAvailable")]
        public bool IsAvailable { get; set; }
    }
}
