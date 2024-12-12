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
       

        private DateTime _dateManufacture = new DateTime(2024, 11, 4);

        private DateTime _dateExpiration = new DateTime(2024, 11, 15);

        private decimal _priceOneMedications;

        private int _countMedication; 
        private string _nameMedication;

        
        private string _typeMedication;

        public int Id { get; set; }


        public decimal Prise => PriceOneMedication * CountMedication;
        public string PriseText => Prise.ToString("#,##0.00 руб.");
        public string Date_Shelf_Life => $"{DateManufacture.ToShortDateString()} -{DateExpiration.ToShortDateString()}  ";

        public string NameMedication
        {
            get => _nameMedication;
            set
            {
                _nameMedication = value;

                OnPropertyChanged(nameof(NameMedication));
            }
        }

        public string TypeMedication
        {
            get => _typeMedication;
            set
            {
                _typeMedication = value;

                OnPropertyChanged(nameof(TypeMedication));
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



        public decimal PriceOneMedication
        {
            get => _priceOneMedications;
            set
            {
                _priceOneMedications = value;
                OnPropertyChanged(nameof(Prise));
                OnPropertyChanged(nameof(PriceOneMedication));
            }
        }
        public int CountMedication
        {
            get => _countMedication;
            set
            {
                _countMedication = value;
                OnPropertyChanged(nameof(Prise));
                OnPropertyChanged(nameof(CountMedication));
            }
        }

       

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewModel model)
        {
            Id = model.Id;
            NameMedication = model.NameMedication;
            TypeMedication = model.TypeMedication;
            DateManufacture = model.DateManufacture;
            DateExpiration = model.DateExpiration;
            PriceOneMedication = model.PriceOneMedication;
            CountMedication = model.CountMedication;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
