using Newtonsoft.Json;
using System;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    internal class JsonCard
    {
        public int Id { get; set; }

        /// <summary>
        /// Номер стола
        /// </summary>
        public int TableNumber { get; set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Тип заказа
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        /// Дата заказа
        /// </summary>
        [JsonIgnore]
        public DateTime OrderDate { get; set; }

        [JsonProperty("OrderDate")]
        public string OrderDateXml
        {
            get => OrderDate.ToShortDateString();
            set => OrderDate = DateTime.Parse(value);
        }

        /// <summary>
        /// Дата завершения
        /// </summary>
        [JsonIgnore]
        public DateTime? CompletionDate { get; set; }

        [JsonProperty("CompletionDate")]
        public string CompletionDateXml
        {
            get => CompletionDate.HasValue ? CompletionDate.Value.ToShortDateString() : "-";
            set
            {
                if (value == "-")
                {
                    CompletionDate = null;
                }
                else
                {
                    CompletionDate = DateTime.Parse(value);
                }
            }
        }

        /// <summary>
        /// Цена заказа
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Статус оплаты
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Имя официанта
        /// </summary>
        public string WaiterName { get; set; }
    }
}