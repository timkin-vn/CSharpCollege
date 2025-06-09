using System.Collections.Generic;
using System.ComponentModel;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : INotifyPropertyChanged
    {
        private string _fullName;
        private string _cardNumber;
        private int _bonusPoints;
        private string _cardType;

        public int Id { get; set; }

        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string CardNumber
        {
            get => _cardNumber;
            set
            {
                _cardNumber = value;
                OnPropertyChanged(nameof(CardNumber));
            }
        }

        public int BonusPoints
        {
            get => _bonusPoints;
            set
            {
                _bonusPoints = value;
                OnPropertyChanged(nameof(BonusPoints));
            }
        }

        public string CardType
        {
            get => _cardType;
            set
            {
                _cardType = value;
                OnPropertyChanged(nameof(CardType));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewModel model)
        {
            Id = model.Id;
            FullName = model.FullName;
            CardNumber = model.CardNumber;
            BonusPoints = model.BonusPoints;
            CardType = model.CardType;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}