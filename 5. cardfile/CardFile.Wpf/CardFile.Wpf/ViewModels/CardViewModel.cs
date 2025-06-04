using System;
using CardFile.Common.Infrastructure;
using Microsoft.Win32;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ProductionDate { get; set; }
        public int ShelfLifeDays { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public string ProductInfo => $"{ProductName} ({Manufacturer})";
        public string ProductionDateText => ProductionDate.ToShortDateString();
        public string ExpirationDateText => ExpirationDate?.ToShortDateString() ?? "Нет данных";
        public bool IsExpired => ExpirationDate.HasValue && ExpirationDate < DateTime.Now;

        public void LoadFromViewModel(CardViewModel viewModel)
        {
            Mapping.Mapper.Map(viewModel, this);
            UpdateAll();
        }

        public bool HasNoExpiration { get; set; }

        public bool IsExpirationDateEnabled => !HasNoExpiration;

        public void ExpirationChecked()
        {
            ExpirationDate = null;
            HasNoExpiration = true;
            OnPropertyChanged(nameof(ExpirationDate));
            OnPropertyChanged(nameof(IsExpirationDateEnabled));
        }

        public void ExpirationUnchecked()
        {
            ExpirationDate = DateTime.Today.AddDays(30);
            HasNoExpiration = false;
            OnPropertyChanged(nameof(ExpirationDate));
            OnPropertyChanged(nameof(IsExpirationDateEnabled));
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(ProductName));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Manufacturer));
            OnPropertyChanged(nameof(ProductionDate));
            OnPropertyChanged(nameof(ShelfLifeDays));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(QuantityInStock));
            OnPropertyChanged(nameof(ExpirationDate));
            OnPropertyChanged(nameof(ProductInfo));
            OnPropertyChanged(nameof(ProductionDateText));
            OnPropertyChanged(nameof(ExpirationDateText));
            OnPropertyChanged(nameof(IsExpired));
        }
    }
}