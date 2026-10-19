using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCard
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Бренд
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Дата поступления
        /// </summary>
        [JsonIgnore]
        public DateTime DateAdded { get; set; }

        [JsonProperty("DateAdded")]
        public string DateAddedText
        {
            get => DateAdded.ToShortDateString();
            set => DateAdded = DateTime.Parse(value);
        }

        /// <summary>
        /// Поставщик
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// Место хранения
        /// </summary>
        public string StorageLocation { get; set; }

        /// <summary>
        /// Дата последнего пополнения
        /// </summary>
        [JsonIgnore]
        public DateTime LastRestockDate { get; set; }

        [JsonProperty("LastRestockDate")]
        public string LastRestockDateText
        {
            get => LastRestockDate.ToShortDateString();
            set => LastRestockDate = DateTime.Parse(value);
        }

        /// <summary>
        /// Срок годности
        /// </summary>
        [JsonIgnore]
        public DateTime? ExpirationDate { get; set; }

        [JsonProperty("ExpirationDate")]
        public string ExpirationDateText
        {
            get => ExpirationDate?.ToShortDateString() ?? "-";
            set => ExpirationDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
    }
}
