using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace CardFile.Business.Models
{
    public class Card
    {
        public int Id { get; set; }

        [JsonPropertyName("title")]
        [XmlElement("Title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("director")]
        [XmlElement("Director")]
        public string Director { get; set; } = string.Empty;

        [JsonPropertyName("release_year")]
        [XmlElement("ReleaseYear")]
        public int ReleaseYear { get; set; }

        [JsonPropertyName("genre")]
        [XmlElement("Genre")]
        public string Genre { get; set; } = string.Empty;

        [JsonPropertyName("duration_minutes")]
        [XmlElement("DurationMinutes")]
        public decimal DurationMinutes { get; set; }

        [JsonPropertyName("rating")]
        [XmlElement("Rating")]
        public int Rating { get; set; }

        [JsonPropertyName("is_available")]
        [XmlElement("IsAvailable")]
        public bool IsAvailable { get; set; } = true;

        public Card() { }

        public Card(int id, string title, string director, int releaseYear)
        {
            Id = id;
            Title = title;
            Director = director;
            ReleaseYear = releaseYear;
        }
    }
}
