using System;

namespace CardFile.DataAccess.Dtos
{
    public class CardDto
    {
        // DTO = Data Transfer Object
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
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Дата завершения
        /// </summary>
        public DateTime? CompletionDate { get; set; }

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

        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                TableNumber = TableNumber,
                CustomerName = CustomerName,
                OrderType = OrderType,
                OrderDate = OrderDate,
                CompletionDate = CompletionDate,
                Price = Price,
                IsPaid = IsPaid,
                WaiterName = WaiterName
            };
        }
    }
}