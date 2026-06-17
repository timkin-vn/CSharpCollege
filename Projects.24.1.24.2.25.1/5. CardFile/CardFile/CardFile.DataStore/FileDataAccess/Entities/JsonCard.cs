using Newtonsoft.Json;
using System;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCard
    {
        /// <summary>
        /// Уникальный идентификатор фильма
        /// </summary>
        [JsonProperty("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Название фильма
        /// </summary>
        [JsonProperty("Title")]
        public string Title { get; set; }

        /// <summary>
        /// Режиссер
        /// </summary>
        [JsonProperty("Director")]
        public string Director { get; set; }

        /// <summary>
        /// Год выпуска киноленты
        /// </summary>
        [JsonProperty("Year")]
        public int Year { get; set; }

        /// <summary>
        /// Жанр фильма
        /// </summary>
        [JsonProperty("Genre")]
        public string Genre { get; set; }

        /// <summary>
        /// Длительность фильма в минутах
        /// </summary>
        [JsonProperty("Duration")]
        public int Duration { get; set; }

        /// <summary>
        /// Рейтинг фильма (например, 8.6)
        /// </summary>
        [JsonProperty("Rating")]
        public decimal Rating { get; set; }
    }
}
