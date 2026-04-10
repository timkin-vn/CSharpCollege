using Newtonsoft.Json;
using System;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCard
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string AlbumTitle { get; set; }
        public string Genre { get; set; }

        [JsonIgnore]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("ReleaseDate")]
        public string ReleaseDateText
        {
            get => ReleaseDate.ToShortDateString();
            set => ReleaseDate = DateTime.Parse(value);
        }

        public string Label { get; set; }
        public string Format { get; set; }

        [JsonIgnore]
        public DateTime PurchaseDate { get; set; }

        [JsonProperty("PurchaseDate")]
        public string PurchaseDateText
        {
            get => PurchaseDate.ToShortDateString();
            set => PurchaseDate = DateTime.Parse(value);
        }

        [JsonIgnore]
        public DateTime? LastListenDate { get; set; }

        [JsonProperty("LastListenDate")]
        public string LastListenDateText
        {
            get => LastListenDate?.ToShortDateString() ?? "-";
            set => LastListenDate = value == "-" ? (DateTime?)null : DateTime.Parse(value);
        }

        public decimal Price { get; set; }
    }
}
