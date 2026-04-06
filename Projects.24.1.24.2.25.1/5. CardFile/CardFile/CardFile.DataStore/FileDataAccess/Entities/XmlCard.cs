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

        [XmlAttribute("Genre")]
        public string Genre { get; set; }

        [XmlAttribute("GlobalRating")]
        public double GlobalRating { get; set; }

        [XmlAttribute("MyScore")]
        public int MyScore { get; set; }

        [XmlIgnore] 
        public DateTime ReleaseDate { get; set; }

        [XmlAttribute("ReleaseDate")]
        public string ReleaseDateText
        {
            get => ReleaseDate.ToShortDateString();
            set => ReleaseDate = DateTime.Parse(value);
        }

        [XmlAttribute("Description")]
        public string Description { get; set; }
    }
}
