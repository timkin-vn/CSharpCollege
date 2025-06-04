using System;
using System.ComponentModel;

namespace CardFile.Business.Models
{
    public class Card : INotifyPropertyChanged
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

        public string OrderInfo { get; private set; }

        public Card()
        {
            OrderDate = DateTime.Now;
            Price = 50.00m;
            UpdateOrderInfo();
        }

        private void UpdateOrderInfo()
        {
            OrderInfo = $"{TableNumber} {CustomerName} ({(IsPaid ? "Оплачено" : "Не оплачено")})";
            OnPropertyChanged("OrderInfo");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}