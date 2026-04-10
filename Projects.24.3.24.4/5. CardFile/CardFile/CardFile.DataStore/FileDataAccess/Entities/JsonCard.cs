using Newtonsoft.Json;
using System;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCard
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Studio { get; set; }
        public string Genre { get; set; }

        [JsonIgnore]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("ReleaseDate")]
        public string ReleaseDateText
        {
            get => ReleaseDate.ToShortDateString();
            set => ReleaseDate = DateTime.Parse(value);
        }

        public string Platform { get; set; }
        public string Publisher { get; set; }

        [JsonIgnore]
        public DateTime PurchaseDate { get; set; }

        [JsonProperty("PurchaseDate")]
        public string PurchaseDateText
        {
            get => PurchaseDate.ToShortDateString();
            set => PurchaseDate = DateTime.Parse(value);
        }

        [JsonIgnore]
        public DateTime? CompletionDate { get; set; }

        [JsonProperty("CompletionDate")]
        public string CompletionDateText
        {
            get => CompletionDate?.ToShortDateString() ?? "-";
            set => CompletionDate = value == "-" ? (DateTime?)null : DateTime.Parse(value);
        }

        public decimal Price { get; set; }
    }
}
