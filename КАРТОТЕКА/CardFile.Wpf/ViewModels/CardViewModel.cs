using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
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

        public string EntryDateText => EntryDate.ToShortDateString();

        /// <summary>
        /// Дата выезда
        /// </summary>
        public DateTime? ExitDate { get; set; }

        public string ExitDateText => ExitDate?.ToShortDateString();

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

        public string ParkingInfo => $"{LicensePlate} {OwnerName} ({(IsPaid ? "Оплачено" : "Не оплачено")})";

        public bool IsStillParked { get; set; }

        public bool IsExitDateEnabled => !IsStillParked;

        public void LoadFromViewModel(CardViewModel viewModel)
        {
            Mapping.Mapper.Map(viewModel, this);
            IsStillParked = !viewModel.ExitDate.HasValue;
            UpdateAll();
        }

        public void IsStillParkedChecked()
        {
            ExitDate = null;
            OnPropertyChanged("ExitDate");
            OnPropertyChanged("ExitDateText");
            OnPropertyChanged("IsExitDateEnabled");
        }

        public void IsStillParkedUnchecked()
        {
            ExitDate = DateTime.Today;
            OnPropertyChanged("ExitDate");
            OnPropertyChanged("ExitDateText");
            OnPropertyChanged("IsExitDateEnabled");
        }

        private void UpdateAll()
        {
            OnPropertyChanged("Id");
            OnPropertyChanged("LicensePlate");
            OnPropertyChanged("OwnerName");
            OnPropertyChanged("VehicleType");
            OnPropertyChanged("EntryDate");
            OnPropertyChanged("EntryDateText");
            OnPropertyChanged("ExitDate");
            OnPropertyChanged("ExitDateText");
            OnPropertyChanged("HourlyRate");
            OnPropertyChanged("IsPaid");
            OnPropertyChanged("ParkingSpot");
            OnPropertyChanged("ParkingInfo");
            OnPropertyChanged("IsStillParked");
            OnPropertyChanged("IsExitDateEnabled");
        }
    }
}