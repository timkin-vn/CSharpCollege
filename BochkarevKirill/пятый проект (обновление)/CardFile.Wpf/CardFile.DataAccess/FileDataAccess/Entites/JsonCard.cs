using Newtonsoft.Json;
using System;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    internal class JsonCard
    {
        public int Id { get; set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество клиента
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Дата привоза автомобиля
        /// </summary>
        [JsonIgnore]
        public DateTime ArrivalDate { get; set; }

        [JsonProperty("ArrivalDate")]
        public string ArrivalDateXml
        {
            get => ArrivalDate.ToShortDateString();
            set => ArrivalDate = DateTime.Parse(value);
        }

        /// <summary>
        /// Причина ремонта
        /// </summary>
        public string RepairReason { get; set; }

        /// <summary>
        /// Дата завершения ремонта
        /// </summary>
        [JsonIgnore]
        public DateTime? CompletionDate { get; set; }

        [JsonProperty("CompletionDate")]
        public string CompletionDateXml
        {
            get => CompletionDate.HasValue ? CompletionDate.Value.ToShortDateString() : "-";
            set => CompletionDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        /// <summary>
        /// Стоимость ремонта
        /// </summary>
        public decimal RepairCost { get; set; }
    }
}