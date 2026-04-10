using Newtonsoft.Json;
using System;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCard
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }

        [JsonIgnore]
        public DateTime PublicationDate { get; set; }

        [JsonProperty("PublicationDate")]
        public string PublicationDateText
        {
            get => PublicationDate.ToShortDateString();
            set => PublicationDate = DateTime.Parse(value);
        }

        public string Publisher { get; set; }
        public string Language { get; set; }

        [JsonIgnore]
        public DateTime ArrivalDate { get; set; }

        [JsonProperty("ArrivalDate")]
        public string ArrivalDateText
        {
            get => ArrivalDate.ToShortDateString();
            set => ArrivalDate = DateTime.Parse(value);
        }

        [JsonIgnore]
        public DateTime? BorrowedUntil { get; set; }

        [JsonProperty("BorrowedUntil")]
        public string BorrowedUntilText
        {
            get => BorrowedUntil?.ToShortDateString() ?? "-";
            set => BorrowedUntil = value == "-" ? (DateTime?)null : DateTime.Parse(value);
        }

        public decimal Price { get; set; }
    }
}
