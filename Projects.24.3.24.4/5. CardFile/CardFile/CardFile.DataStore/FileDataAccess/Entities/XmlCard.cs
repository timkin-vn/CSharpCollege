using System;
using System.Globalization;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class XmlCard
    {
        private const string DateFormat = "yyyy-MM-dd";

        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("Brand")]
        public string Brand { get; set; }

        [XmlAttribute("Model")]
        public string Model { get; set; }

        [XmlAttribute("LicensePlate")]
        public string LicensePlate { get; set; }

        [XmlIgnore]
        public DateTime ManufactureDate { get; set; }

        [XmlAttribute("ManufactureDate")]
        public string ManufactureDateText
        {
            get => ManufactureDate.ToString(DateFormat, CultureInfo.InvariantCulture);
            set => ManufactureDate = DateTime.ParseExact(value, DateFormat, CultureInfo.InvariantCulture);
        }

        [XmlAttribute("Color")]
        public string Color { get; set; }

        [XmlAttribute("Owner")]
        public string Owner { get; set; }

        [XmlIgnore]
        public DateTime RegistrationDate { get; set; }

        [XmlAttribute("RegistrationDate")]
        public string RegistrationDateText
        {
            get => RegistrationDate.ToString(DateFormat, CultureInfo.InvariantCulture);
            set => RegistrationDate = DateTime.ParseExact(value, DateFormat, CultureInfo.InvariantCulture);
        }

        [XmlIgnore]
        public DateTime? DeregistrationDate { get; set; }

        [XmlAttribute("DeregistrationDate")]
        public string DeregistrationDateText
        {
            get => DeregistrationDate?.ToString(DateFormat, CultureInfo.InvariantCulture) ?? "-";
            set => DeregistrationDate = value == "-" ? (DateTime?)null : DateTime.ParseExact(value, DateFormat, CultureInfo.InvariantCulture);
        }

        [XmlAttribute("Price")]
        public decimal Price { get; set; }
    }
}
