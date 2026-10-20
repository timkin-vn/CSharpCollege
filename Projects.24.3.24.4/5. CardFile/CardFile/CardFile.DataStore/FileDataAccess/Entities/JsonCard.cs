using Newtonsoft.Json;
using System;
using System.Globalization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCard
    {
        private const string DateFormat = "yyyy-MM-dd";

        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }

        [JsonIgnore]
        public DateTime ManufactureDate { get; set; }

        [JsonProperty("ManufactureDate")]
        public string ManufactureDateText
        {
            get => ManufactureDate.ToString(DateFormat, CultureInfo.InvariantCulture);
            set => ManufactureDate = DateTime.ParseExact(value, DateFormat, CultureInfo.InvariantCulture);
        }

        public string Color { get; set; }
        public string Owner { get; set; }

        [JsonIgnore]
        public DateTime RegistrationDate { get; set; }

        [JsonProperty("RegistrationDate")]
        public string RegistrationDateText
        {
            get => RegistrationDate.ToString(DateFormat, CultureInfo.InvariantCulture);
            set => RegistrationDate = DateTime.ParseExact(value, DateFormat, CultureInfo.InvariantCulture);
        }

        [JsonIgnore]
        public DateTime? DeregistrationDate { get; set; }

        [JsonProperty("DeregistrationDate")]
        public string DeregistrationDateText
        {
            get => DeregistrationDate?.ToString(DateFormat, CultureInfo.InvariantCulture) ?? "-";
            set => DeregistrationDate = value == "-" ? (DateTime?)null : DateTime.ParseExact(value, DateFormat, CultureInfo.InvariantCulture);
        }

        public decimal Price { get; set; }
    }
}
