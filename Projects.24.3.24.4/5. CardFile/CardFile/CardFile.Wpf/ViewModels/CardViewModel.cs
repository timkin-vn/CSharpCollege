using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {

        public int Id { get; set; }
        public string Brand { get; set; }
        public string ModelName { get; set; }
        public string FullTitle => $"{Brand} {ModelName}";
        public int Year { get; set; }
        public string VinCode { get; set; }
        public decimal Price { get; set; }
        public string EngineType { get; set; }
        public double EngineVolume { get; set; }
        public int Mileage { get; set; }
        public string Color { get; set; }
        public DateTime? LastServiceDate { get; set; } 

        public bool IsWorkingTillNow { get; set; }
        public bool IsDismissalDateEnabled => !IsWorkingTillNow;

        public void IsWorkingTillNowChecked()
        {
            IsWorkingTillNow = true;
            LastServiceDate = null;
            UpdateAll();
        }

        public void IsWorkingTillNowUnchecked()
        {
            IsWorkingTillNow = false;
            LastServiceDate = DateTime.Now;
            UpdateAll();
        }

        public void LoadViewModel(CardViewModel card)
        {
            Mapping.Mapper.Map(card, this);
            IsWorkingTillNow = !card.LastServiceDate.HasValue;
            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Brand));
            OnPropertyChanged(nameof(ModelName));
            OnPropertyChanged(nameof(FullTitle));
            OnPropertyChanged(nameof(Year));
            OnPropertyChanged(nameof(VinCode));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(EngineType));
            OnPropertyChanged(nameof(EngineVolume));
            OnPropertyChanged(nameof(Mileage));
            OnPropertyChanged(nameof(Color));
            OnPropertyChanged(nameof(LastServiceDate));
            OnPropertyChanged(nameof(IsWorkingTillNow));
            OnPropertyChanged(nameof(IsDismissalDateEnabled));
        }
    }
}