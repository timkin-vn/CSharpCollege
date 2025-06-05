using System;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    [Serializable]
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Номер стола
        /// </summary>
        [XmlAttribute("TableNumber")]
        public int TableNumber { get; set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        [XmlAttribute("CustomerName")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Тип заказа
        /// </summary>
        [XmlAttribute("OrderType")]
        public string OrderType { get; set; }

        /// <summary>
        /// Дата заказа
        /// </summary>
        [XmlIgnore]
        public DateTime OrderDate { get; set; }

        [XmlAttribute("OrderDate")]
        public string OrderDateXml
        {
            get => OrderDate.ToShortDateString();
            set => OrderDate = DateTime.Parse(value);
        }

        /// <summary>
        /// Дата завершения
        /// </summary>
        [XmlIgnore]
        public DateTime? CompletionDate { get; set; }

        [XmlAttribute("CompletionDate")]
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
        [XmlAttribute("Price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Статус оплаты
        /// </summary>
        [XmlAttribute("IsPaid")]
        public bool IsPaid { get; set; }

        /// <summary>
        /// Имя официанта
        /// </summary>
        [XmlAttribute("WaiterName")]
        public string WaiterName { get; set; }
    }
}