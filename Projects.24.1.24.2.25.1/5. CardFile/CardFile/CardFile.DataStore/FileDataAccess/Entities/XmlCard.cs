using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("Title")]
        public string Title { get; set; }

        [XmlAttribute("Director")]
        public string Director { get; set; }

        [XmlAttribute("Year")]
        public int Year { get; set; }

        [XmlAttribute("Genre")]
        public string Genre { get; set; }

        [XmlAttribute("Duration")]
        public int Duration { get; set; }

        [XmlAttribute("Rating")]
        public decimal Rating { get; set; }
    }
}
