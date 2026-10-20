using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        private int _id;
        private string _brand;
        private string _model;
        private string _licensePlate;
        private DateTime _manufactureDate;
        private string _color;
        private string _owner;
        private DateTime _registrationDate;
        private DateTime? _deregistrationDate;
        private decimal _price;
        private bool _isRegisteredNow;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Brand
        {
            get => _brand;
            set
            {
                _brand = value;
                OnPropertyChanged(nameof(Brand));
                OnPropertyChanged(nameof(CarInfo));
            }
        }

        public string Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
                OnPropertyChanged(nameof(CarInfo));
            }
        }

        public string LicensePlate
        {
            get => _licensePlate;
            set
            {
                _licensePlate = value;
                OnPropertyChanged(nameof(LicensePlate));
                OnPropertyChanged(nameof(CarInfo));
            }
        }

        public DateTime ManufactureDate
        {
            get => _manufactureDate;
            set
            {
                _manufactureDate = value;
                OnPropertyChanged(nameof(ManufactureDate));
                OnPropertyChanged(nameof(ManufactureDateText));
            }
        }

        public string Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        public string Owner
        {
            get => _owner;
            set
            {
                _owner = value;
                OnPropertyChanged(nameof(Owner));
            }
        }

        public DateTime RegistrationDate
        {
            get => _registrationDate;
            set
            {
                _registrationDate = value;
                OnPropertyChanged(nameof(RegistrationDate));
                OnPropertyChanged(nameof(RegistrationDateText));
            }
        }

        public DateTime? DeregistrationDate
        {
            get => _deregistrationDate;
            set
            {
                _deregistrationDate = value;
                OnPropertyChanged(nameof(DeregistrationDate));
                OnPropertyChanged(nameof(DeregistrationDateText));
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(PriceText));
            }
        }

        public string CarInfo => $"{Brand} {Model} ({LicensePlate})";
        public string ManufactureDateText => ManufactureDate.ToShortDateString();
        public string RegistrationDateText => RegistrationDate.ToShortDateString();
        public string DeregistrationDateText => DeregistrationDate?.ToShortDateString() ?? "На учёте";
        public string PriceText => Price.ToString("c");

        public bool IsRegisteredNow
        {
            get => _isRegisteredNow;
            set
            {
                _isRegisteredNow = value;
                OnPropertyChanged(nameof(IsRegisteredNow));
                OnPropertyChanged(nameof(IsDeregistrationDateEnabled));
            }
        }

        public bool IsDeregistrationDateEnabled => !IsRegisteredNow;

        public void IsRegisteredNowChecked()
        {
            IsRegisteredNow = true;
            DeregistrationDate = null;
        }

        public void IsRegisteredNowUnchecked()
        {
            IsRegisteredNow = false;
            DeregistrationDate = DateTime.Today;
        }

        public void LoadViewModel(CardViewModel card)
        {
            Mapping.Mapper.Map(card, this);
            IsRegisteredNow = !card.DeregistrationDate.HasValue;
            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Brand));
            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(LicensePlate));
            OnPropertyChanged(nameof(ManufactureDate));
            OnPropertyChanged(nameof(Color));
            OnPropertyChanged(nameof(Owner));
            OnPropertyChanged(nameof(RegistrationDate));
            OnPropertyChanged(nameof(DeregistrationDate));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(CarInfo));
            OnPropertyChanged(nameof(ManufactureDateText));
            OnPropertyChanged(nameof(RegistrationDateText));
            OnPropertyChanged(nameof(DeregistrationDateText));
            OnPropertyChanged(nameof(PriceText));
            OnPropertyChanged(nameof(IsRegisteredNow));
            OnPropertyChanged(nameof(IsDeregistrationDateEnabled));
        }
    }
}
