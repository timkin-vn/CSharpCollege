using System;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("Artist")]
        public string Artist { get; set; }

        [XmlAttribute("AlbumTitle")]
        public string AlbumTitle { get; set; }

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

        [XmlAttribute("Label")]
        public string Label { get; set; }

        [XmlAttribute("Format")]
        public string Format { get; set; }

        [XmlIgnore]
        public DateTime PurchaseDate { get; set; }

        [XmlAttribute("PurchaseDate")]
        public string PurchaseDateText
        {
            get => PurchaseDate.ToShortDateString();
            set => PurchaseDate = DateTime.Parse(value);
        }

        [XmlIgnore]
        public DateTime? LastListenDate { get; set; }

        [XmlAttribute("LastListenDate")]
        public string LastListenDateText
        {
            get => LastListenDate?.ToShortDateString() ?? "-";
            set => LastListenDate = value == "-" ? (DateTime?)null : DateTime.Parse(value);
        }

        [XmlAttribute("Price")]
        public decimal Price { get; set; }
    }
}
