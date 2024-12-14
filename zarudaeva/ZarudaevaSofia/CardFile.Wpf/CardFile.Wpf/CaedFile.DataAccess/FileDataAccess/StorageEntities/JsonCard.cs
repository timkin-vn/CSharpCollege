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

        public string MovieName { get; set; }

        public string MovieType { get; set; }

        
        [JsonPropertyName("DateReles")]
        public long DateRelesTicks
        {
            get => DateReles.Ticks;
            set => DateReles = new DateTime(value);
        }

        [JsonIgnore]
        public DateTime DateReles { get; set; } 
        [JsonPropertyName("TimeSpan")]
        public long TimeSpanTicks
        {
            get => TimeGoes.Ticks;
            set => TimeGoes = new TimeSpan(value);
        }

        [JsonIgnore]
        public TimeSpan TimeGoes { get; set; }

        public decimal PriseOneTickets { get; set; }

        public int CountTickets { get; set; }
        public short LinePlace { get; set; }
        public short Place { get; set; }

    }
}
