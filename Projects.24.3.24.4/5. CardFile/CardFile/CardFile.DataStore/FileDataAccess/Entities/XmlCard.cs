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

        [XmlAttribute("Studio")]
        public string Studio { get; set; }

        [XmlAttribute("Genre")]
        public string Genre { get; set; }

        [XmlIgnore]
        public DateTime ReleaseDate { get; set; }

        [XmlAttribute("ReleaseDate")]
        public string ReleaseDateText
        {
            get => ReleaseDate.ToShortDateString();
            set => ReleaseDate = DateTime.Parse(value);
        }

        [XmlAttribute("Platform")]
        public string Platform { get; set; }

        [XmlAttribute("Publisher")]
        public string Publisher { get; set; }

        [XmlIgnore]
        public DateTime PurchaseDate { get; set; }

        [XmlAttribute("PurchaseDate")]
        public string PurchaseDateText
        {
            get => PurchaseDate.ToShortDateString();
            set => PurchaseDate = DateTime.Parse(value);
        }

        [XmlIgnore]
        public DateTime? CompletionDate { get; set; }

        [XmlAttribute("CompletionDate")]
        public string CompletionDateText
        {
            get => CompletionDate?.ToShortDateString() ?? "-";
            set => CompletionDate = value == "-" ? (DateTime?)null : DateTime.Parse(value);
        }

        [XmlAttribute("Price")]
        public decimal Price { get; set; }
    }
}
