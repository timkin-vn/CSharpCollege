using System;
using System.ComponentModel;

namespace CardFile.Business.Models
{
    public class Card : INotifyPropertyChanged
    {
        public int Id { get; set; }

        /// <summary>
        /// Номер ремонтного бокса
        /// </summary>
        public int BayNumber { get; set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Тип услуги (например, шиномонтаж, замена масла)
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// Дата приёма автомобиля
        /// </summary>
        public DateTime DropOffDate { get; set; }

        /// <summary>
        /// Дата завершения/выдачи
        /// </summary>
        public DateTime? PickupDate { get; set; }

        /// <summary>
        /// Стоимость услуги
        /// </summary>
        public decimal ServiceCost { get; set; }

        /// <summary>
        /// Статус оплаты
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Имя механика
        /// </summary>
        public string MechanicName { get; set; }

        public string ServiceInfo { get; private set; }

        public Card()
        {
            DropOffDate = DateTime.Now;
            ServiceCost = 100.00m;
            UpdateServiceInfo();
        }

        private void UpdateServiceInfo()
        {
            ServiceInfo = $"{BayNumber} {ClientName} ({(IsPaid ? "Оплачено" : "Не оплачено")})";
            OnPropertyChanged("ServiceInfo");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
