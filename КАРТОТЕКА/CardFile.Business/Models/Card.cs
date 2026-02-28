using System;
using System.ComponentModel;

namespace CardFile.Business.Models
{
    public class Card : INotifyPropertyChanged
    {
        public int Id { get; set; }

        /// <summary>
        /// Номер машины
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// Владелец
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// Тип транспорта
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// Дата въезда
        /// </summary>
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// Дата выезда
        /// </summary>
        public DateTime? ExitDate { get; set; }

        /// <summary>
        /// Стоимость в час
        /// </summary>
        public decimal HourlyRate { get; set; }

        /// <summary>
        /// Статус оплаты
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Парковочное место
        /// </summary>
        public int ParkingSpot { get; set; }

        public string ParkingInfo { get; private set; }

        public Card()
        {
            EntryDate = DateTime.Now;
            HourlyRate = 5.00m;
            UpdateParkingInfo();
        }

        private void UpdateParkingInfo()
        {
            ParkingInfo = $"{LicensePlate} {OwnerName} ({(IsPaid ? "Оплачено" : "Не оплачено")})";
            OnPropertyChanged("ParkingInfo");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}