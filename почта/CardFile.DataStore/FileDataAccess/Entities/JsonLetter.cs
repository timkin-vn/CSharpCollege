using Newtonsoft.Json;
using System;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonLetter
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        [JsonIgnore] public DateTime Date { get; set; }
        [JsonProperty("Date")] public string DateText { get => Date.ToShortDateString(); set => Date = DateTime.Parse(value); }

        public bool IsRead { get; set; }
    }
}