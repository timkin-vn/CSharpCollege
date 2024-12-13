using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CardFile.DataAccess.FileDataAccess.StorageEntities
{
    [Serializable]
    public class JsonCard
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        [JsonPropertyName("DatePurchase")]
        public long DatePurchaseTicks
        {
            get => DatePurchase.Ticks;
            set => DatePurchase = new DateTime(value);
        }

        [JsonIgnore]
        public DateTime DatePurchase { get; set; }

        public decimal Price { get; set; }

        public int Mileage { get; set; }
    }
}
