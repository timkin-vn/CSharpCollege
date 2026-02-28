using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    [Serializable]
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("BookName")]
        public string BookName { get; set; }

        [XmlAttribute("AuthorFirstName")]
        public string AuthorFirstName { get; set; }

        [XmlAttribute("AuthorLastName")]
        public string AuthorLastName { get; set; }

        [XmlAttribute("Genre")]
        public string Genre { get; set; }

        [XmlIgnore]
        public DateTime DateOfPublishing { get; set; }

        [XmlAttribute("DateOfPublishing")]
        public string BirthDateXml
        {
            get => DateOfPublishing.ToShortDateString();
            set => DateOfPublishing = DateTime.Parse(value);
        }

        [XmlAttribute("Edition")]
        public string Edition { get; set; }

        [XmlAttribute("Price")]
        public int Price { get; set; }

        [XmlAttribute("AmountOfPages")]
        public int AmountOfPages { get; set; }

        [XmlIgnore]
        public DateTime? DateOfDelisting { get; set; }

        [XmlAttribute("DateOfDelisting")]
        public string DateOfDelistingXml
        {
            get => DateOfDelisting.HasValue ? DateOfDelisting.Value.ToShortDateString() : "-";
            set => DateOfDelisting = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        [XmlAttribute("Rating")]
        public decimal Rating { get; set; }
    }
}
