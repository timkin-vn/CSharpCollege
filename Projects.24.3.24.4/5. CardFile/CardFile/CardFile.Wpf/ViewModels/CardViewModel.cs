using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Studio { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Platform { get; set; }
        public string Publisher { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public decimal Price { get; set; }

        public string GameInfo => $"{Title} ({Platform})";
        public string ReleaseDateText => ReleaseDate.ToShortDateString();
        public string PurchaseDateText => PurchaseDate.ToShortDateString();
        public string CompletionDateText => CompletionDate?.ToShortDateString() ?? "Не пройдена";
        public string PriceText => Price.ToString("c");

        public bool IsNotCompleted { get; set; }
        public bool IsCompletionDateEnabled => !IsNotCompleted;

        public void IsNotCompletedChecked()
        {
            CompletionDate = null;
            OnPropertyChanged(nameof(CompletionDate));
            OnPropertyChanged(nameof(CompletionDateText));
            OnPropertyChanged(nameof(IsCompletionDateEnabled));
        }

        public void IsNotCompletedUnchecked()
        {
            CompletionDate = DateTime.Today;
            OnPropertyChanged(nameof(CompletionDate));
            OnPropertyChanged(nameof(CompletionDateText));
            OnPropertyChanged(nameof(IsCompletionDateEnabled));
        }

        public void LoadViewModel(CardViewModel card)
        {
            Mapping.Mapper.Map(card, this);
            IsNotCompleted = !card.CompletionDate.HasValue;
            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Studio));
            OnPropertyChanged(nameof(Genre));
            OnPropertyChanged(nameof(ReleaseDate));
            OnPropertyChanged(nameof(Platform));
            OnPropertyChanged(nameof(Publisher));
            OnPropertyChanged(nameof(PurchaseDate));
            OnPropertyChanged(nameof(CompletionDate));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(GameInfo));
            OnPropertyChanged(nameof(ReleaseDateText));
            OnPropertyChanged(nameof(PurchaseDateText));
            OnPropertyChanged(nameof(CompletionDateText));
            OnPropertyChanged(nameof(PriceText));
            OnPropertyChanged(nameof(IsNotCompleted));
            OnPropertyChanged(nameof(IsCompletionDateEnabled));
        }
    }
}
