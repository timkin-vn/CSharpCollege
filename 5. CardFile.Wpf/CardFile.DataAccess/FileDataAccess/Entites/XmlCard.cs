using System;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    [Serializable]
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("Brand")]
        public string Brand { get; set; }

        [XmlAttribute("Model")]
        public string Model { get; set; }

        [XmlAttribute("Year")]
        public int Year { get; set; }

        [XmlAttribute("Vin")]
        public string Vin { get; set; }

        [XmlAttribute("Color")]
        public string Color { get; set; }

        [XmlAttribute("Price")]
        public decimal Price { get; set; }

        [XmlIgnore]
        public DateTime ArrivalDate { get; set; }

        [XmlAttribute("ArrivalDate")]
        public string ArrivalDateXml
        {
            get => ArrivalDate.ToShortDateString();
            set => ArrivalDate = DateTime.Parse(value);
        }

        [XmlIgnore]
        public DateTime? SaleDate { get; set; }

        [XmlAttribute("SaleDate")]
        public string SaleDateXml
        {
            get => SaleDate.HasValue ? SaleDate.Value.ToShortDateString() : "-";
            set => SaleDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }
    }
}
