using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Series { get; set; }

        public string Platform { get; set; }

        public string Genre { get; set; }

        public string Developer { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string ReleaseDateText => ReleaseDate.ToShortDateString();

        public DateTime PurchaseDate { get; set; }

        public string PurchaseDateText => PurchaseDate.ToShortDateString();

        public DateTime? CompletionDate { get; set; }

        public string CompletionDateText => CompletionDate?.ToShortDateString() ?? "-";

        public decimal Price { get; set; }

        public string PriceText => Price.ToString("c");

        public int PersonalRating { get; set; }

        public bool IsDigital { get; set; }

        public string MediaTypeText => IsDigital ? "Цифровая" : "Физическая";

        public string GameInfo => string.IsNullOrWhiteSpace(Series)
            ? $"{Title} ({Platform})"
            : $"{Series}: {Title} ({Platform})";

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
            OnPropertyChanged(nameof(Series));
            OnPropertyChanged(nameof(Platform));
            OnPropertyChanged(nameof(Genre));
            OnPropertyChanged(nameof(Developer));
            OnPropertyChanged(nameof(ReleaseDate));
            OnPropertyChanged(nameof(ReleaseDateText));
            OnPropertyChanged(nameof(PurchaseDate));
            OnPropertyChanged(nameof(PurchaseDateText));
            OnPropertyChanged(nameof(CompletionDate));
            OnPropertyChanged(nameof(CompletionDateText));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(PriceText));
            OnPropertyChanged(nameof(PersonalRating));
            OnPropertyChanged(nameof(IsDigital));
            OnPropertyChanged(nameof(MediaTypeText));
            OnPropertyChanged(nameof(GameInfo));
            OnPropertyChanged(nameof(IsNotCompleted));
            OnPropertyChanged(nameof(IsCompletionDateEnabled));
        }
    }
}
