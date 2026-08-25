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

        public string BirthDateText => BirthDate.ToShortDateString();

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

        public string EmploymentDateText => EmploymentDate.ToShortDateString();

        /// <summary>
        /// Дата увольнения
        /// </summary>
        public DateTime? DismissalDate { get; set; }

        public string DismissalDateText => DismissalDate?.ToShortDateString() ?? "-";

        /// <summary>
        /// Оклад
        /// </summary>
        public decimal Salary { get; set; }

        public string SalaryText => Salary.ToString("c");

        public bool IsWorkingTillNow { get; set; }

        public bool IsDismissalDateEnabled => !IsWorkingTillNow;

        public void IsWorkingTillNowChecked()
        {
            DismissalDate = null;

            OnPropertyChanged(nameof(DismissalDate));
            OnPropertyChanged(nameof(IsDismissalDateEnabled));
        }

        public void IsWorkingTillNowUnchecked()
        {
            DismissalDate = DateTime.Today;

            OnPropertyChanged(nameof(DismissalDate));
            OnPropertyChanged(nameof(IsDismissalDateEnabled));
        }

        public void LoadViewModel(CardViewModel card)
        {
            Mapping.Mapper.Map(card, this);
            //Id = card.Id;
            //FirstName = card.FirstName;
            //MiddleName = card.MiddleName;
            //LastName = card.LastName;
            //BirthDate = card.BirthDate;
            //Department = card.Department;
            //Position = card.Position;
            //EmploymentDate = card.EmploymentDate;
            //DismissalDate = card.DismissalDate;
            //Salary = card.Salary;

            IsWorkingTillNow = !card.DismissalDate.HasValue;
            UpdateAll();
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
            OnPropertyChanged(nameof(IsWorkingTillNow));
            OnPropertyChanged(nameof(IsDismissalDateEnabled));
        }
    }
}
