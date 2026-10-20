using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCard
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string Genre { get; set; } = string.Empty;
        public decimal DurationMinutes { get; set; }
        public int Rating { get; set; }
        public bool IsAvailable { get; set; }
    }
}
