
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCard
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
        public decimal Price { get; set; }
        public string FuelType { get; set; }
        public decimal EngineVolume { get; set; }
        public string TransmissionType { get; set; }
        public bool HasAirConditioning { get; set; }

        [JsonIgnore]
        public DateTime AddedDate { get; set; }

        [JsonProperty("AddedDate")]
        public string AddedDateText
        {
            get => AddedDate.ToString("O", CultureInfo.InvariantCulture);
            set => AddedDate = DateTime.Parse(
                value, CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind);
        }

        [JsonIgnore]
        public DateTime? SoldDate { get; set; }

        [JsonProperty("SoldDate")]
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

        public string Vin { get; set; }
    }
}
