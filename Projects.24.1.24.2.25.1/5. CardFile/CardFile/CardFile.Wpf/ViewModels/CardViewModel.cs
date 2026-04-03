using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CardFile.Wpf.ViewModels
{
    // Вьюмодель карточки товара
    public class CardViewModel : ViewModelBase
    {
        private bool _isNotWrittenOff;
        public int Id { get; set; }

        public string ProductName { get; set; }
        public string ProductModel { get; set; }
        public string ProductColor { get; set; }
        public string FullProductName => $"{ProductName} {ProductModel} {ProductColor}".Trim();

        public DateTime ManufactureDate { get; set; }
        public string ManufactureDateText => ManufactureDate.ToShortDateString();

        public string Category { get; set; }
        public string Manufacturer { get; set; }

        public DateTime ReceiptDate { get; set; }
        public string ReceiptDateText => ReceiptDate.ToShortDateString();

        public DateTime? WriteOffDate { get; set; }
        public string WriteOffDateText => WriteOffDate?.ToShortDateString() ?? "-";

        public decimal Price { get; set; }
        public string PriceText => Price.ToString("c");

        public bool IsNotWrittenOff
        {
            get => _isNotWrittenOff;
            set
            {
                if (_isNotWrittenOff != value)
                {
                    _isNotWrittenOff = value;
                    if (_isNotWrittenOff)
                        WriteOffDate = null;
                    else
                        WriteOffDate = DateTime.Today;
                    OnPropertyChanged(nameof(WriteOffDate));
                    OnPropertyChanged(nameof(IsWriteOffDateEnabled));
                    OnPropertyChanged(nameof(IsNotWrittenOff));
                }
            }
        }

        public bool IsWriteOffDateEnabled => !IsNotWrittenOff;

        public void LoadViewModel(CardViewModel model)
        {
            Mapping.Mapper.Map(model, this);
            IsNotWrittenOff = !model.WriteOffDate.HasValue;
            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(ProductName));
            OnPropertyChanged(nameof(ProductModel));
            OnPropertyChanged(nameof(ProductColor));
            OnPropertyChanged(nameof(FullProductName));
            OnPropertyChanged(nameof(ManufactureDate));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Manufacturer));
            OnPropertyChanged(nameof(ReceiptDate));
            OnPropertyChanged(nameof(WriteOffDate));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(IsNotWrittenOff));
            OnPropertyChanged(nameof(IsWriteOffDateEnabled));
        }
    }
}