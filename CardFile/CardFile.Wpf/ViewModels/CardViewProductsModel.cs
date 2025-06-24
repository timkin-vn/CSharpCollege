using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewProductsModel : INotifyPropertyChanged
    {
        private string _nameProducts;

        private string _typeProducts;

        private string _sectionProducts;

        private string _shirtProducts;

        private DateTime _dateManufacture = new DateTime(2024, 11, 4);

        private DateTime _dateExpiration = new DateTime(2024, 11, 15);

        private decimal _priceOneProducts;

        private int _countProducts;


        public int Id { get; set; }
        public string Storage_Location => $" {SectionProducts} - {ShirtProducts} полка ";
        public decimal Prise => PriceOneProducts * CountProducts;
        public string Date_Shelf_Life => $"{DateManufacture.ToShortDateString()} -{DateExpiration.ToShortDateString()}  ";

        public string NameProducts
        {
            get => _nameProducts;
            set
            {
                _nameProducts = value;

                OnPropertyChanged(nameof(NameProducts));
            }
        }

        public string TypeProducts
        {
            get => _typeProducts;
            set
            {
                _typeProducts = value;

                OnPropertyChanged(nameof(TypeProducts));
            }
        }

        public string SectionProducts
        {
            get => _sectionProducts;
            set
            {
                _sectionProducts = value;

                OnPropertyChanged(nameof(SectionProducts));
                OnPropertyChanged(nameof(Storage_Location));
            }
        }
        public string ShirtProducts
        {
            get => _shirtProducts;
            set
            {
                _shirtProducts = value;

                OnPropertyChanged(nameof(ShirtProducts));
                OnPropertyChanged(nameof(Storage_Location));
            }
        }

        public DateTime DateManufacture
        {
            get => _dateManufacture;
            set
            {
                _dateManufacture = value;
                OnPropertyChanged(nameof(DateManufacture));
                OnPropertyChanged(nameof(Date_Shelf_Life));
            }
        }
        public DateTime DateExpiration
        {
            get => _dateExpiration;
            set
            {
                _dateExpiration = value;
                OnPropertyChanged(nameof(DateExpiration));
                OnPropertyChanged(nameof(Date_Shelf_Life));
            }
        }



        public decimal PriceOneProducts
        {
            get => _priceOneProducts;
            set
            {
                _priceOneProducts = value;
                OnPropertyChanged(nameof(Prise));
                OnPropertyChanged(nameof(PriceOneProducts));
            }
        }
        public int CountProducts
        {
            get => _countProducts;
            set
            {
                _countProducts = value;
                OnPropertyChanged(nameof(Prise));
                OnPropertyChanged(nameof(PriceOneProducts));
            }
        }

        public string PriseText => Prise.ToString("#,##0.00 руб.");


        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewProductsModel model)
        {

            Id = model.Id;

            CountProducts = model.CountProducts;

            NameProducts = model.NameProducts;

            TypeProducts = model.TypeProducts;

            ShirtProducts = model.ShirtProducts;

            SectionProducts = model.SectionProducts;

            DateExpiration = model.DateExpiration;

            DateManufacture = model.DateManufacture;

            PriceOneProducts = model.PriceOneProducts;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
