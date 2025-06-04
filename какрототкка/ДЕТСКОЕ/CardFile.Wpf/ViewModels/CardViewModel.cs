using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
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

        public string OrderDateText => OrderDate.ToShortDateString();

        /// <summary>
        /// Дата завершения
        /// </summary>
        public DateTime? CompletionDate { get; set; }

        public string CompletionDateText => CompletionDate?.ToShortDateString();

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

        public string OrderInfo => $"{TableNumber} {CustomerName} ({(IsPaid ? "Оплачено" : "Не оплачено")})";

        public bool IsOrderActive { get; set; }

        public bool IsCompletionDateEnabled => !IsOrderActive;

        public void LoadFromViewModel(CardViewModel viewModel)
        {
            Mapping.Mapper.Map(viewModel, this);
            IsOrderActive = !viewModel.CompletionDate.HasValue;
            UpdateAll();
        }

        public void IsOrderActiveChecked()
        {
            CompletionDate = null;
            OnPropertyChanged("CompletionDate");
            OnPropertyChanged("CompletionDateText");
            OnPropertyChanged("IsCompletionDateEnabled");
        }

        public void IsOrderActiveUnchecked()
        {
            CompletionDate = DateTime.Today;
            OnPropertyChanged("CompletionDate");
            OnPropertyChanged("CompletionDateText");
            OnPropertyChanged("IsCompletionDateEnabled");
        }

        private void UpdateAll()
        {
            OnPropertyChanged("Id");
            OnPropertyChanged("TableNumber");
            OnPropertyChanged("CustomerName");
            OnPropertyChanged("OrderType");
            OnPropertyChanged("OrderDate");
            OnPropertyChanged("OrderDateText");
            OnPropertyChanged("CompletionDate");
            OnPropertyChanged("CompletionDateText");
            OnPropertyChanged("Price");
            OnPropertyChanged("IsPaid");
            OnPropertyChanged("WaiterName");
            OnPropertyChanged("OrderInfo");
            OnPropertyChanged("IsOrderActive");
            OnPropertyChanged("IsCompletionDateEnabled");
        }
    }
}