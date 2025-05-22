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
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        public string Fio => $"{LastName} {FirstName} {MiddleName}";

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Подразделение
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Дата трудоустройства
        /// </summary>
        public DateTime EmploymentDate { get; set; }

        /// <summary>
        /// Дата увольнения
        /// </summary>
        public DateTime? DismissalDate { get; set; }

        /// <summary>
        /// Сумма оклада
        /// </summary>
        public decimal Salary { get; set; }

        public bool WorksTillNow { get; set; }

        public bool IsDismissalDateEnabled => !WorksTillNow;

        public void LoadFromViewModel(CardViewModel viewModel)
        {
            Id = viewModel.Id;
            FirstName = viewModel.FirstName;
            MiddleName = viewModel.MiddleName;
            LastName = viewModel.LastName;
            BirthDate = viewModel.BirthDate;
            Department = viewModel.Department;
            Position = viewModel.Position;
            EmploymentDate = viewModel.EmploymentDate;
            DismissalDate = viewModel.DismissalDate;
            WorksTillNow = !viewModel.DismissalDate.HasValue;
            Salary = viewModel.Salary;

            UpdateAll();
        }

        public void WorksTillNowChecked()
        {
            DismissalDate = null;

            OnPropertyChanged(nameof(DismissalDate));
            OnPropertyChanged(nameof(IsDismissalDateEnabled));
        }

        public void WorksTillNowUnchecked()
        {
            DismissalDate = DateTime.Today;

            OnPropertyChanged(nameof(DismissalDate));
            OnPropertyChanged(nameof(IsDismissalDateEnabled));
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(MiddleName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Fio));
            OnPropertyChanged(nameof(BirthDate));
            OnPropertyChanged(nameof(Department));
            OnPropertyChanged(nameof(Position));
            OnPropertyChanged(nameof(EmploymentDate));
            OnPropertyChanged(nameof(DismissalDate));
            OnPropertyChanged(nameof(Salary));
            OnPropertyChanged(nameof(WorksTillNow));
            OnPropertyChanged(nameof(IsDismissalDateEnabled));
        }
    }
}
