using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
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

        public string Fio => $"{LastName} {FirstName} {MiddleName}";

        /// <summary>
        /// Дата привоза автомобиля
        /// </summary>
        public DateTime ArrivalDate { get; set; }

        public string ArrivalDateText => ArrivalDate.ToShortDateString();

        /// <summary>
        /// Причина ремонта
        /// </summary>
        public string RepairReason { get; set; }

        /// <summary>
        /// Дата завершения ремонта
        /// </summary>
        public DateTime? CompletionDate { get; set; }

        public string CompletionDateText => CompletionDate?.ToShortDateString();

        /// <summary>
        /// Стоимость ремонта
        /// </summary>
        public decimal RepairCost { get; set; }

        public bool IsRepairCompleted => CompletionDate.HasValue;

        public bool IsCompletionDateEnabled => !IsRepairCompleted;

        public bool GetIsRepairCompleted()
        {
            return IsRepairCompleted;
        }

        public void LoadFromViewModel(CardViewModel viewModel, bool isRepairCompleted)
        {
            Mapping.Mapper.Map(viewModel, this);
            isRepairCompleted = viewModel.CompletionDate.HasValue;
            UpdateAll();
        }

        public void RepairCompletedChecked()
        {
            CompletionDate = DateTime.Today;
            OnPropertyChanged(nameof(CompletionDate));
            OnPropertyChanged(nameof(IsCompletionDateEnabled));
        }

        public void RepairCompletedUnchecked()
        {
            CompletionDate = null;
            OnPropertyChanged(nameof(CompletionDate));
            OnPropertyChanged(nameof(IsCompletionDateEnabled));
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(MiddleName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Fio));
            OnPropertyChanged(nameof(ArrivalDate));
            OnPropertyChanged(nameof(ArrivalDateText));
            OnPropertyChanged(nameof(RepairReason));
            OnPropertyChanged(nameof(CompletionDate));
            OnPropertyChanged(nameof(CompletionDateText));
            OnPropertyChanged(nameof(RepairCost));
            OnPropertyChanged(nameof(IsRepairCompleted));
            OnPropertyChanged(nameof(IsCompletionDateEnabled));
        }
    }
}