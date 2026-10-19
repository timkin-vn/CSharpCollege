
using System;
using System.Globalization;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
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

        [XmlAttribute("Color")]
        public string Color { get; set; }

        [XmlAttribute("Mileage")]
        public int Mileage { get; set; }

        [XmlAttribute("Price")]
        public decimal Price { get; set; }

        [XmlAttribute("FuelType")]
        public string FuelType { get; set; }

        [XmlAttribute("EngineVolume")]
        public decimal EngineVolume { get; set; }

        [XmlAttribute("TransmissionType")]
        public string TransmissionType { get; set; }

        [XmlAttribute("HasAirConditioning")]
        public bool HasAirConditioning { get; set; }

        [XmlIgnore]
        public DateTime AddedDate { get; set; }

        [XmlAttribute("AddedDate")]
        public string AddedDateText
        {
            get => AddedDate.ToString("O", CultureInfo.InvariantCulture);
            set => AddedDate = DateTime.Parse(
                value, CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind);
        }

        [XmlIgnore]
        public DateTime? SoldDate { get; set; }

        [XmlAttribute("SoldDate")]
        public string SoldDateText
        {
            get => SoldDate.HasValue
                ? SoldDate.Value.ToString("O", CultureInfo.InvariantCulture)
                : "-";

            set => SoldDate = string.IsNullOrWhiteSpace(value) || value == "-"
                ? (DateTime?)null
                : DateTime.Parse(
                    value, CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind);
        }

        [XmlAttribute("Vin")]
        public string Vin { get; set; }
    }
}
