using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ProductName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public string DeliveryMethod { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public decimal TotalAmount { get; set; }

        public string OrderInfo => $"{ClientLastName} {ClientFirstName} — {ProductName}";
        public string OrderDateText => OrderDate.ToShortDateString();
        public string ShippingDateText => ShippingDate.ToShortDateString();
        public string ReceivedDateText => ReceivedDate?.ToShortDateString() ?? "В пути";
        public string TotalAmountText => TotalAmount.ToString("c");

        public bool IsInTransit { get; set; }
        public bool IsReceivedDateEnabled => !IsInTransit;

        public void IsInTransitChecked()
        {
            ReceivedDate = null;
            OnPropertyChanged(nameof(ReceivedDate));
            OnPropertyChanged(nameof(ReceivedDateText));
            OnPropertyChanged(nameof(IsReceivedDateEnabled));
        }

        public void IsInTransitUnchecked()
        {
            ReceivedDate = DateTime.Today;
            OnPropertyChanged(nameof(ReceivedDate));
            OnPropertyChanged(nameof(ReceivedDateText));
            OnPropertyChanged(nameof(IsReceivedDateEnabled));
        }

        public void LoadViewModel(CardViewModel card)
        {
            Mapping.Mapper.Map(card, this);
            IsInTransit = !card.ReceivedDate.HasValue;
            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(ClientFirstName));
            OnPropertyChanged(nameof(ClientLastName));
            OnPropertyChanged(nameof(ProductName));
            OnPropertyChanged(nameof(OrderDate));
            OnPropertyChanged(nameof(Address));
            OnPropertyChanged(nameof(DeliveryMethod));
            OnPropertyChanged(nameof(ShippingDate));
            OnPropertyChanged(nameof(ReceivedDate));
            OnPropertyChanged(nameof(TotalAmount));
            OnPropertyChanged(nameof(OrderInfo));
            OnPropertyChanged(nameof(OrderDateText));
            OnPropertyChanged(nameof(ShippingDateText));
            OnPropertyChanged(nameof(ReceivedDateText));
            OnPropertyChanged(nameof(TotalAmountText));
            OnPropertyChanged(nameof(IsInTransit));
            OnPropertyChanged(nameof(IsReceivedDateEnabled));
        }
    }
}
