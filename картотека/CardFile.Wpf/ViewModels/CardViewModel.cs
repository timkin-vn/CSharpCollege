using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Id { get; set; }

        /// <summary>
        /// Номер бокса
        /// </summary>
        public int BayNumber { get; set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Вид услуги
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// Дата приёма машины
        /// </summary>
        public DateTime DropOffDate { get; set; }

        public string DropOffDateText => DropOffDate.ToShortDateString();

        /// <summary>
        /// Дата завершения ремонта
        /// </summary>
        public DateTime? PickupDate { get; set; }

        public string PickupDateText => PickupDate?.ToShortDateString();

        /// <summary>
        /// Стоимость услуги
        /// </summary>
        public decimal ServiceCost { get; set; }

        /// <summary>
        /// Оплачено ли
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Механик
        /// </summary>
        public string MechanicName { get; set; }

        public string ServiceInfo => $"{BayNumber} {ClientName} ({(IsPaid ? "Оплачено" : "Не оплачено")})";

        public bool IsServiceInProgress { get; set; }

        public bool IsPickupDateEnabled => !IsServiceInProgress;

        public void LoadFromViewModel(CardViewModel viewModel)
        {
            Mapping.Mapper.Map(viewModel, this);
            IsServiceInProgress = !viewModel.PickupDate.HasValue;
            UpdateAll();
        }
        public bool IsServiceActive
        {
            get => PickupDate == null;
            set
            {
                if (value)
                {
                    PickupDate = null;
                }
                else
                {
                    PickupDate = DateTime.Today;
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(PickupDate));
                OnPropertyChanged(nameof(PickupDateText));
                OnPropertyChanged(nameof(IsPickupDateEnabled));
            }
        }
        public void StartService()
        {
            PickupDate = null;
            OnPropertyChanged("PickupDate");
            OnPropertyChanged("PickupDateText");
            OnPropertyChanged("IsPickupDateEnabled");
        }

        public void FinishService()
        {
            PickupDate = DateTime.Today;
            OnPropertyChanged("PickupDate");
            OnPropertyChanged("PickupDateText");
            OnPropertyChanged("IsPickupDateEnabled");
        }

        private void UpdateAll()
        {
            OnPropertyChanged("Id");
            OnPropertyChanged("BayNumber");
            OnPropertyChanged("ClientName");
            OnPropertyChanged("ServiceType");
            OnPropertyChanged("DropOffDate");
            OnPropertyChanged("DropOffDateText");
            OnPropertyChanged("PickupDate");
            OnPropertyChanged("PickupDateText");
            OnPropertyChanged("ServiceCost");
            OnPropertyChanged("IsPaid");
            OnPropertyChanged("MechanicName");
            OnPropertyChanged("ServiceInfo");
            OnPropertyChanged("IsServiceInProgress");
            OnPropertyChanged("IsPickupDateEnabled");
        }
    }
}
