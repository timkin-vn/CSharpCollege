using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    internal class JsonCard
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string Genre { get; set; }
        [JsonIgnore]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("ReleaseDate")]
        public string ReleaseDateXml
        {
            get => ReleaseDate.ToShortDateString();
            set => ReleaseDate = DateTime.Parse(value);
        }

        public string Studio { get; set; }
        public string Director { get; set; }

        [JsonIgnore]
        public DateTime? EndDate { get; set; }

        [JsonProperty("EndDate")]
        public string EndDateXml
        {
            get => EndDate.HasValue ? EndDate.Value.ToShortDateString() : "-";
            set => EndDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        public decimal Rating { get; set; }
    }
}