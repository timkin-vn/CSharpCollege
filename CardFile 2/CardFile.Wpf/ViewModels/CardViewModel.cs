using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : INotifyPropertyChanged
    {
        private string _firstName;

        private string _lastName;

        private string _middleName;

        private DateTime _birthDate = new DateTime(2000, 6, 15);

        private decimal _heightAmount;

        private int _weight;

        private string _street;

        private string _city;

        private int _house = 1;

        public int Id { get; set; }

        public string Fio => $"{LastName} {FirstName} {MiddleName}";

        public string Address => $"г. {City} ул. {Street} д. {House}";

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

        public int House
        {
            get => _house;
            set
            {
                _house = value;
                OnPropertyChanged(nameof(Address));
                OnPropertyChanged(nameof(House));
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(Fio));
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string MiddleName
        {
            get => _middleName;
            set
            {
                _middleName = value;
                OnPropertyChanged(nameof(Fio));
                OnPropertyChanged(nameof(MiddleName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(Fio));
                OnPropertyChanged(nameof(LastName));
            }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }


        public string BirthDateText => BirthDate.ToShortDateString();

        public decimal HeightAmount
        {
            get => _heightAmount;
            set
            {
                _heightAmount = value;
                OnPropertyChanged(nameof(HeightAmount));
            }
        }

        public string HeightAmountText => HeightAmount.ToString("###000 см\\.");


        public int Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewModel model)
        {
            Id = model.Id;
            FirstName = model.FirstName;
            MiddleName = model.MiddleName;
            LastName = model.LastName;
            BirthDate = model.BirthDate;
            HeightAmount = model.HeightAmount;
            Weight = model.Weight;
            City = model.City;
            Street = model.Street;
            House = model.House;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
