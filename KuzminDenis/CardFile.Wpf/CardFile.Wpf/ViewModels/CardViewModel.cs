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

        /// <summary>
        /// Категория
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Производитель
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Серия
        /// </summary>
        public string Series { get; set; }

        public string Product => $"{Category} {Manufacturer} {Series}";

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        public string ReleaseDateText => ReleaseDate.ToShortDateString();

        /// <summary>
        /// Подразделение
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Дата трудоустройства
        /// </summary>
        public int WarrantyMonths { get; set; }

        /// <summary>
        /// Дата увольнения
        /// </summary>
        public DateTime? DiscontinuedDate { get; set; }

        public string DiscontinuedDateText => DiscontinuedDate?.ToShortDateString();

        /// <summary>
        /// Сумма оклада
        /// </summary>
        public string ProducingCountry { get; set; }

        public bool WorksTillNow { get; set; }

        public bool IsDiscontinuedDateEnabled => !WorksTillNow;

        public void LoadFromViewModel(CardViewModel viewModel)
        {
            Mapping.Mapper.Map(viewModel, this);
            WorksTillNow = !viewModel.DiscontinuedDate.HasValue;

            UpdateAll();
        }

        public void WorksTillNowChecked()
        {
            DiscontinuedDate = null;

            OnPropertyChanged(nameof(DiscontinuedDate));
            OnPropertyChanged(nameof(IsDiscontinuedDateEnabled));
        }

        public void WorksTillNowUnchecked()
        {
            DiscontinuedDate = DateTime.Today;

            OnPropertyChanged(nameof(DiscontinuedDate));
            OnPropertyChanged(nameof(IsDiscontinuedDateEnabled));
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Manufacturer));
            OnPropertyChanged(nameof(Series));
            OnPropertyChanged(nameof(Product));
            OnPropertyChanged(nameof(ReleaseDate));
            OnPropertyChanged(nameof(ReleaseDateText));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(StockQuantity));
            OnPropertyChanged(nameof(WarrantyMonths));
            OnPropertyChanged(nameof(DiscontinuedDate));
            OnPropertyChanged(nameof(DiscontinuedDateText));
            OnPropertyChanged(nameof(ProducingCountry));
            OnPropertyChanged(nameof(WorksTillNow));
            OnPropertyChanged(nameof(IsDiscontinuedDateEnabled));
        }
    }
}
