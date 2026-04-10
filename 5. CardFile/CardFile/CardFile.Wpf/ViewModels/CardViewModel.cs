using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime? BorrowedUntil { get; set; }
        public decimal Price { get; set; }

        public string BookInfo => $"{Author} — {Title}";
        public string PublicationDateText => PublicationDate.ToShortDateString();
        public string ArrivalDateText => ArrivalDate.ToShortDateString();
        public string BorrowedUntilText => BorrowedUntil?.ToShortDateString() ?? "В наличии";
        public string PriceText => Price.ToString("c");

        public bool IsAvailable { get; set; }
        public bool IsBorrowedUntilEnabled => !IsAvailable;

        public void IsAvailableChecked()
        {
            BorrowedUntil = null;
            OnPropertyChanged(nameof(BorrowedUntil));
            OnPropertyChanged(nameof(BorrowedUntilText));
            OnPropertyChanged(nameof(IsBorrowedUntilEnabled));
        }

        public void IsAvailableUnchecked()
        {
            BorrowedUntil = DateTime.Today;
            OnPropertyChanged(nameof(BorrowedUntil));
            OnPropertyChanged(nameof(BorrowedUntilText));
            OnPropertyChanged(nameof(IsBorrowedUntilEnabled));
        }

        public void LoadViewModel(CardViewModel card)
        {
            Mapping.Mapper.Map(card, this);
            IsAvailable = !card.BorrowedUntil.HasValue;
            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Author));
            OnPropertyChanged(nameof(Genre));
            OnPropertyChanged(nameof(PublicationDate));
            OnPropertyChanged(nameof(Publisher));
            OnPropertyChanged(nameof(Language));
            OnPropertyChanged(nameof(ArrivalDate));
            OnPropertyChanged(nameof(BorrowedUntil));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(BookInfo));
            OnPropertyChanged(nameof(PublicationDateText));
            OnPropertyChanged(nameof(ArrivalDateText));
            OnPropertyChanged(nameof(BorrowedUntilText));
            OnPropertyChanged(nameof(PriceText));
            OnPropertyChanged(nameof(IsAvailable));
            OnPropertyChanged(nameof(IsBorrowedUntilEnabled));
        }
    }
}
