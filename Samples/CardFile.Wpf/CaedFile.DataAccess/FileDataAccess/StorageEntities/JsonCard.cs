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

        public string Title { get; set; }
        


        [JsonPropertyName("EXP")]
        public long EXPTicks
        {
            get => EXP.Ticks;
            set => EXP = new DateTime(value);
        }

        [JsonIgnore]
        public DateTime EXP { get; set; }

        public string Fabricator { get; set; }
        public string Section { get; set; }

        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
