using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCard
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LoreName { get; set; }

        public string PrimaryAttribute { get; set; }

        public string AttackType { get; set; }

        public string Role { get; set; }

        [JsonIgnore]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("ReleaseDate")]
        public string ReleaseDateText
        {
            get => ReleaseDate.ToShortDateString();
            set => ReleaseDate = DateTime.Parse(value);
        }

        [JsonIgnore]
        public DateTime? ReworkDate { get; set; }

        [JsonProperty("ReworkDate")]
        public string ReworkDateText
        {
            get => ReworkDate?.ToShortDateString() ?? "-";
            set => ReworkDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        public decimal WinRate { get; set; }
    }
}
