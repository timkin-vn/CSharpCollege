using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardFile.Business.Entities;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : INotifyPropertyChanged
    {
        private string _bank_name;

        private int _ATM_count;

        private string _street;

        private string _house;

        private string _city;

        private decimal _money_count;

        private int _money_limit;

        public string _card_number;
        public int Id { get; set; }

        public string Address => $"{Street}, {House}, {City}";


       

        public string Card_number
        {
            get => _card_number;
            set
            {
                
                _card_number = value;
                OnPropertyChanged(nameof(Card_number));
            }
        }

        public string Bank_name
        {
            get => _bank_name;
            set
            {

                _bank_name = value;
                OnPropertyChanged(nameof(Bank_name));
            }
        }

        public int ATM_count
        {
            get => _ATM_count;
            set
            {
                _ATM_count = value;
                OnPropertyChanged(nameof(ATM_count));
            }
        }

        public string ATM_Text => $"{ATM_count} шт.";

        public string Street
        {
            get => _street;
            set
            {
                _street = value;
                OnPropertyChanged(nameof(Address));
                OnPropertyChanged(nameof(Street));
            }
        }

        public string House
        {
            get => _house;
            set
            {
                _house = value;
                OnPropertyChanged(nameof(Address));
                OnPropertyChanged(nameof(House));
            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(Address));
                OnPropertyChanged(nameof(City));
            }
        }

        public decimal Money_count
        {
            get => _money_count;
            set
            {
                _money_count = value;
                OnPropertyChanged(nameof(Money_count));
            }
        }

        public string Money_countText => $"{Money_count:#,##0.00} р.";
        


        public int Money_limit
        {
            get => _money_limit;
            set
            {
                _money_limit = value;
                OnPropertyChanged(nameof(Money_limit));
            }
        }

        public string Money_limitText => $"{Money_limit:#,##0.00} р.";

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewModel model)
        {
            Id = model.Id;
            Bank_name = model.Bank_name;
            ATM_count = model.ATM_count;
            Street = model.Street;
            House = model.House;
            City = model.City;
            Money_count = model.Money_count;
            Money_limit = model.Money_limit;
            






        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
