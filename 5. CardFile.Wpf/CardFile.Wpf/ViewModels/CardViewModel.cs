using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Id { get; set; }

        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Vin { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime? SaleDate { get; set; }

        public string PriceText => $"{Price:C}";
        public string SaleDateText => SaleDate?.ToShortDateString();

        public bool IsAvailable { get; set; }
        public bool IsSaleDateEnabled => !IsAvailable;

        public void LoadFromViewModel(CardViewModel viewModel)
        {
            Mapping.Mapper.Map(viewModel, this);
            IsAvailable = !viewModel.SaleDate.HasValue;
            UpdateAll();
        }

        public void IsAvailableChecked()
        {
            SaleDate = null;
            OnPropertyChanged(nameof(SaleDate));
            OnPropertyChanged(nameof(IsSaleDateEnabled));
        }

        public void IsAvailableUnchecked()
        {
            SaleDate = DateTime.Today;
            OnPropertyChanged(nameof(SaleDate));
            OnPropertyChanged(nameof(IsSaleDateEnabled));
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Brand));
            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(Year));
            OnPropertyChanged(nameof(Vin));
            OnPropertyChanged(nameof(Color));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(PriceText));
            OnPropertyChanged(nameof(ArrivalDate));
            OnPropertyChanged(nameof(SaleDate));
            OnPropertyChanged(nameof(SaleDateText));
            OnPropertyChanged(nameof(IsAvailable));
            OnPropertyChanged(nameof(IsSaleDateEnabled));
        }
    }
}