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

        public string NameProducts { get; set; }

        public string TypeProducts { get; set; }

 

        [JsonPropertyName("DateManufacture")]
        public long DateManufactureTicks
        {
            get => DateManufacture.Ticks;
            set => DateManufacture = new DateTime(value);
        }

        [JsonIgnore]
        public DateTime DateManufacture { get; set; }  
        [JsonPropertyName("DateExpiration")]
        public long DateExpirationTicks
        {
            get => DateExpiration.Ticks;
            set => DateExpiration = new DateTime(value);
        }

        [JsonIgnore]
        public DateTime DateExpiration { get; set; }

        public int CountProducts { get; set; }

        public decimal PriceOneProducts { get; set; }

        public string SectionProducts { get; set; }

        public string ShirtProducts { get; set; }
    }
}
