using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : INotifyPropertyChanged
    {
        private string _manufacturer;

        private string _model;

        private DateTime _datePurchase = new DateTime(2000, 6, 15);

        private decimal _price;

        private int _mileage;

        public int Id { get; set; }

        public string Car => $"{Manufacturer} {Model}";

        public string Manufacturer
        {
            get => _manufacturer;
            set
            {
                _manufacturer = value;
                OnPropertyChanged(nameof(Car));
                OnPropertyChanged(nameof(Manufacturer));
            }
        }

        public string Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Car));
                OnPropertyChanged(nameof(Model));
            }
        }

        public DateTime DatePurchase
        {
            get => _datePurchase;
            set
            {
                _datePurchase = value;
                OnPropertyChanged(nameof(DatePurchase));
            }
        }


        public string DatePurchaseText => DatePurchase.ToShortDateString();

        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public string PriceText => Price.ToString("#,##0.00 р\\.");

        public int Mileage
        {
            get => _mileage;
            set
            {
                _mileage = value;
                OnPropertyChanged(nameof(Mileage));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewModel model)
        {
            Id = model.Id;
            Manufacturer = model.Manufacturer;
            Model = model.Model;
            DatePurchase = model.DatePurchase;
            Price = model.Price;
            Mileage = model.Mileage;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
