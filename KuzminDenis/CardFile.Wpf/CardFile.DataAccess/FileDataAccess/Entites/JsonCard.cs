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

        /// <summary>
        /// Категория
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Производитель
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Серия
        /// </summary>
        public string Series { get; set; }

        /// <summary>
        /// Дата выпуска
        /// </summary>
        [JsonIgnore]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("ReleaseDate")]
        public string ReleaseDateXml
        {
            get => ReleaseDate.ToShortDateString();
            set => ReleaseDate = DateTime.Parse(value);
        }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Кол-во на складе
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Месяцев гарантии
        /// </summary>
        public int WarrantyMonths { get; set; }

        /// <summary>
        /// Дата прекращения производства
        /// </summary>
        [JsonIgnore]
        public DateTime? DiscontinuedDate { get; set; }

        [JsonProperty("DiscontinuedDate")]
        public string DiscontinuedDateXml
        {
            get => DiscontinuedDate.HasValue ? DiscontinuedDate.Value.ToShortDateString() : "-";
            set => DiscontinuedDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        /// <summary>
        /// Страна-производитель
        /// </summary>
        public string ProducingCountry { get; set; }
    }
}
