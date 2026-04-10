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

        [XmlAttribute("Author")]
        public string Author { get; set; }

        [XmlAttribute("Genre")]
        public string Genre { get; set; }

        [XmlIgnore]
        public DateTime PublicationDate { get; set; }

        [XmlAttribute("PublicationDate")]
        public string PublicationDateText
        {
            get => PublicationDate.ToShortDateString();
            set => PublicationDate = DateTime.Parse(value);
        }

        [XmlAttribute("Publisher")]
        public string Publisher { get; set; }

        [XmlAttribute("Language")]
        public string Language { get; set; }

        [XmlIgnore]
        public DateTime ArrivalDate { get; set; }

        [XmlAttribute("ArrivalDate")]
        public string ArrivalDateText
        {
            get => ArrivalDate.ToShortDateString();
            set => ArrivalDate = DateTime.Parse(value);
        }

        [XmlIgnore]
        public DateTime? BorrowedUntil { get; set; }

        [XmlAttribute("BorrowedUntil")]
        public string BorrowedUntilText
        {
            get => BorrowedUntil?.ToShortDateString() ?? "-";
            set => BorrowedUntil = value == "-" ? (DateTime?)null : DateTime.Parse(value);
        }

        [XmlAttribute("Price")]
        public decimal Price { get; set; }
    }
}
