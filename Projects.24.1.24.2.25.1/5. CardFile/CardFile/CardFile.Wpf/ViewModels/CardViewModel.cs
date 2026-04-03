using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Id { get; set; }

        // Используем старые свойства под новые данные книги
        public string FirstName { get; set; } // Название
        public string LastName { get; set; }  // Автор
        public string MiddleName { get; set; } // Доп. информация или серия

        // Это свойство отвечает за текст в общем списке (ListBox)
        // Было: Фамилия Имя Отчество -> Стало: Автор — "Название"
        public string Fio => $"{LastName} — \"{FirstName}\"";

        public DateTime BirthDate { get; set; } // Дата издания
        public string BirthDateText => BirthDate.ToShortDateString();

        public string Department { get; set; } // Жанр
        public string Position { get; set; }   // Издательство

        public DateTime EmploymentDate { get; set; }
        public DateTime? DismissalDate { get; set; }

        public decimal Salary { get; set; } // Цена
        // Выводит цену в формате валюты (например, "500,00 ₽")
        public string SalaryText => Salary.ToString("c");

        // Логика работы (можно оставить как есть для совместимости)
        public bool IsWorkingTillNow { get; set; }
        public bool IsDismissalDateEnabled => !IsWorkingTillNow;

        public void IsWorkingTillNowChecked() { DismissalDate = null; OnPropertyChanged(nameof(DismissalDate)); }
        public void IsWorkingTillNowUnchecked() { DismissalDate = DateTime.Today; OnPropertyChanged(nameof(DismissalDate)); }

        public void LoadViewModel(CardViewModel model)
        {
            Mapping.Mapper.Map(model, this);
            IsWorkingTillNow = !model.DismissalDate.HasValue;
            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Fio));
            OnPropertyChanged(nameof(Department));
            OnPropertyChanged(nameof(Position));
            OnPropertyChanged(nameof(Salary));
            OnPropertyChanged(nameof(SalaryText));
        }
    }
}
