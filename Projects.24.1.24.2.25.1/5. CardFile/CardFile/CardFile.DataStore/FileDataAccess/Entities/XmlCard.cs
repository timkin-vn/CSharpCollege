﻿using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("Hero")]
        public string Hero { get; set; }

        [XmlAttribute("Slot1")]
        public string Slot1 { get; set; }

        [XmlAttribute("Slot2")]
        public string Slot2 { get; set; }

        [XmlAttribute("Slot3")]
        public string Slot3 { get; set; }

        [XmlAttribute("Slot4")]
        public string Slot4 { get; set; }

        [XmlAttribute("Slot5")]
        public string Slot5 { get; set; }

        [XmlAttribute("Slot6")]
        public string Slot6 { get; set; }

        [XmlAttribute("Neutral")]
        public string Neutral { get; set; }
    }
}
