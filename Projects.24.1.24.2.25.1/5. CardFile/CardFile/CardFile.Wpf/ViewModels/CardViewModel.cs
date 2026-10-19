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
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Бренд
        /// </summary>
        public string Brand { get; set; }

        public string ProductTitle => $"{Brand} {ProductName} {Category}";

        /// <summary>
        /// Дата поступления
        /// </summary>
        public DateTime DateAdded { get; set; }

        public string DateAddedText => DateAdded.ToShortDateString();

        /// <summary>
        /// Поставщик
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// Место хранения
        /// </summary>
        public string StorageLocation { get; set; }

        /// <summary>
        /// Дата последнего пополнения
        /// </summary>
        public DateTime LastRestockDate { get; set; }

        public string LastRestockDateText => LastRestockDate.ToShortDateString();

        /// <summary>
        /// Срок годности
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        public string ExpirationDateText => ExpirationDate?.ToShortDateString() ?? "-";

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        public string PriceText => Price.ToString("c");

        public bool IsExpirationDateNotSet { get; set; }

        public bool IsExpirationDateEnabled => !IsExpirationDateNotSet;

        public void ExpirationDateNotSetChecked()
        {
            ExpirationDate = null;

            OnPropertyChanged(nameof(ExpirationDate));
            OnPropertyChanged(nameof(IsExpirationDateEnabled));
        }

        public void ExpirationDateNotSetUnchecked()
        {
            ExpirationDate = DateTime.Today;

            OnPropertyChanged(nameof(ExpirationDate));
            OnPropertyChanged(nameof(IsExpirationDateEnabled));
        }

        public void LoadViewModel(CardViewModel model)
        {
            Mapping.Mapper.Map(model, this);
            //Id = model.Id;
            //ProductName = model.ProductName;
            //Category = model.Category;
            //Brand = model.Brand;
            //DateAdded = model.DateAdded;
            //Supplier = model.Supplier;
            //StorageLocation = model.StorageLocation;
            //LastRestockDate = model.LastRestockDate;
            //ExpirationDate = model.ExpirationDate;
            //Price = model.Price;

            IsExpirationDateNotSet = !model.ExpirationDate.HasValue;

            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(ProductName));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Brand));
            OnPropertyChanged(nameof(ProductTitle));
            OnPropertyChanged(nameof(DateAdded));
            OnPropertyChanged(nameof(Supplier));
            OnPropertyChanged(nameof(StorageLocation));
            OnPropertyChanged(nameof(LastRestockDate));
            OnPropertyChanged(nameof(ExpirationDate));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(IsExpirationDateNotSet));
            OnPropertyChanged(nameof(IsExpirationDateEnabled));
        }
    }
}
