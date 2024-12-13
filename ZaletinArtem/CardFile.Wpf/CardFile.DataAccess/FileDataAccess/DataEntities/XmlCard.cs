using System;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.DataEntities
{
    [Serializable]
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlElement("Title")]
        public string Title { get; set; }

        [XmlElement("Author")]
        public string Author { get; set; }

        [XmlElement("PublicationDate")]
        public long PublicationDateTicks
        {
            get => PublicationDate.Ticks;
            set => PublicationDate = new DateTime(value);
        }

        [XmlIgnore]
        public DateTime PublicationDate { get; set; }

        [XmlElement("Genre")]
        public string Genre { get; set; }

        [XmlElement("PageCount")]
        public int PageCount { get; set; }

        [XmlElement("Price")]
        public decimal Price { get; set; }
    }
}
