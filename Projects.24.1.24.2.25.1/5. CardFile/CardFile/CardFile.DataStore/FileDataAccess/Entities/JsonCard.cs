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
        public int Year { get; set; }
        public int Copies { get; set; }

        [JsonIgnore]
        public DateTime AddedDate { get; set; }

        [JsonProperty("AddedDate")]
        public string AddedDateText
        {
            get => AddedDate.ToShortDateString();
            set => AddedDate = DateTime.Parse(value);
        }

        [JsonIgnore]
        public DateTime? DeletedDate { get; set; }

        [JsonProperty("DeletedDate")]
        public string DeletedDateText
        {
            get => DeletedDate?.ToShortDateString() ?? "-";
            set => DeletedDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }
    }
}