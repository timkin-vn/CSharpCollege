using Newtonsoft.Json;
using System;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCard
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Category { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("CreatedAt")]
        public string CreatedAtText
        {
            get => CreatedAt.ToShortDateString();
            set => CreatedAt = DateTime.Parse(value);
        }

        public bool IsDone { get; set; }

        public bool IsPinned { get; set; }
    }
}