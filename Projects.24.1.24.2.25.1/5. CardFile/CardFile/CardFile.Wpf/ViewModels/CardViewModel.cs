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

        public string Color { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string ReleaseDateText => ReleaseDate.ToString("yyyy");

        public string BodyType { get; set; }

        public string Configuration { get; set; }

        public DateTime ArrivalDate { get; set; }

        public string ArrivalDateText => ArrivalDate.ToShortDateString();

        public DateTime? SaleDate { get; set; }

        public string SaleDateText => SaleDate?.ToShortDateString() ?? "-";

        public decimal Price { get; set; }

        public string PriceText => Price.ToString("N2") + " руб.";

        public bool IsInStock { get; set; }

        public bool IsSaleDateEnabled => !IsInStock;

        public void IsInStockChecked()
        {
            SaleDate = null;

            OnPropertyChanged(nameof(SaleDate));
            OnPropertyChanged(nameof(IsSaleDateEnabled));
        }

        public void IsInStockUnchecked()
        {
            SaleDate = DateTime.Today;

            OnPropertyChanged(nameof(SaleDate));
            OnPropertyChanged(nameof(IsSaleDateEnabled));
        }

        public void LoadViewModel(CardViewModel model)
        {
            Mapping.Mapper.Map(model, this);

            IsInStock = !model.SaleDate.HasValue;

            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Brand));
            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(Color));
            OnPropertyChanged(nameof(ReleaseDate));
            OnPropertyChanged(nameof(BodyType));
            OnPropertyChanged(nameof(Configuration));
            OnPropertyChanged(nameof(ArrivalDate));
            OnPropertyChanged(nameof(SaleDate));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(IsInStock));
            OnPropertyChanged(nameof(IsSaleDateEnabled));
        }
    }
}