using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string AlbumTitle { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Label { get; set; }
        public string Format { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? LastListenDate { get; set; }
        public decimal Price { get; set; }

        public string AlbumInfo => $"{Artist} — {AlbumTitle}";
        public string ReleaseDateText => ReleaseDate.ToShortDateString();
        public string PurchaseDateText => PurchaseDate.ToShortDateString();
        public string LastListenDateText => LastListenDate?.ToShortDateString() ?? "Не слушал";
        public string PriceText => Price.ToString("c");

        public bool IsNotListenedYet { get; set; }
        public bool IsLastListenDateEnabled => !IsNotListenedYet;

        public void IsNotListenedYetChecked()
        {
            LastListenDate = null;
            OnPropertyChanged(nameof(LastListenDate));
            OnPropertyChanged(nameof(LastListenDateText));
            OnPropertyChanged(nameof(IsLastListenDateEnabled));
        }

        public void IsNotListenedYetUnchecked()
        {
            LastListenDate = DateTime.Today;
            OnPropertyChanged(nameof(LastListenDate));
            OnPropertyChanged(nameof(LastListenDateText));
            OnPropertyChanged(nameof(IsLastListenDateEnabled));
        }

        public void LoadViewModel(CardViewModel card)
        {
            Mapping.Mapper.Map(card, this);
            IsNotListenedYet = !card.LastListenDate.HasValue;
            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Artist));
            OnPropertyChanged(nameof(AlbumTitle));
            OnPropertyChanged(nameof(Genre));
            OnPropertyChanged(nameof(ReleaseDate));
            OnPropertyChanged(nameof(Label));
            OnPropertyChanged(nameof(Format));
            OnPropertyChanged(nameof(PurchaseDate));
            OnPropertyChanged(nameof(LastListenDate));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(AlbumInfo));
            OnPropertyChanged(nameof(ReleaseDateText));
            OnPropertyChanged(nameof(PurchaseDateText));
            OnPropertyChanged(nameof(LastListenDateText));
            OnPropertyChanged(nameof(PriceText));
            OnPropertyChanged(nameof(IsNotListenedYet));
            OnPropertyChanged(nameof(IsLastListenDateEnabled));
        }
    }
}
