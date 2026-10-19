using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Id { get; set; }

        /// <summary>
        /// Название компании
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отрасль
        /// </summary>
        public string Industry { get; set; }

        /// <summary>
        /// Год основания
        /// </summary>
        public int FoundationYear { get; set; }

        /// <summary>
        /// Выручка (млрд руб.)
        /// </summary>
        public decimal Revenue { get; set; }

        public string RevenueText => $"{Revenue:N0} млрд ₽";

        /// <summary>
        /// Число сотрудников
        /// </summary>
        public int Employees { get; set; }

        public string EmployeesText => $"{Employees:N0} чел.";

        /// <summary>
        /// Информация о компании
        /// </summary>
        public string Description { get; set; }

        public void LoadViewModel(CardViewModel model)
        {
            Mapping.Mapper.Map(model, this);

            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Industry));
            OnPropertyChanged(nameof(FoundationYear));
            OnPropertyChanged(nameof(Revenue));
            OnPropertyChanged(nameof(RevenueText));
            OnPropertyChanged(nameof(Employees));
            OnPropertyChanged(nameof(EmployeesText));
            OnPropertyChanged(nameof(Description));
        }
    }
}