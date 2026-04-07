using System;
using CardFile.Common.Infrastructure;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string RealName { get; set; }
        public string Country { get; set; }
        public DateTime BirthDate { get; set; }
        public string Team { get; set; }
        public string Role { get; set; }
        public decimal TotalEarnings { get; set; }
        public string Achievements { get; set; }

        public string DisplayName => $"{Nickname} ({RealName})";
        public string BirthDateText => BirthDate.ToShortDateString();
        public string EarningsText => TotalEarnings.ToString("N0") + " $";

        public void LoadViewModel(CardViewModel model)
        {
            Mapping.Mapper.Map(model, this);
            RaiseAllPropertiesChanged();
        }

        private void RaiseAllPropertiesChanged()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Nickname));
            OnPropertyChanged(nameof(RealName));
            OnPropertyChanged(nameof(Country));
            OnPropertyChanged(nameof(BirthDate));
            OnPropertyChanged(nameof(Team));
            OnPropertyChanged(nameof(Role));
            OnPropertyChanged(nameof(TotalEarnings));
            OnPropertyChanged(nameof(Achievements));
            OnPropertyChanged(nameof(DisplayName));
            OnPropertyChanged(nameof(BirthDateText));
            OnPropertyChanged(nameof(EarningsText));
        }
    }
}