using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardFile.Common.Infrastructure;

namespace CardFile.WPF.ViewModels
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

        public string FIO => $"{LastName} {FirstName} {MiddleName}";

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }
        public string BirthDateString => BirthDate.ToShortDateString();
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
        public string EmploymentDateString => EmploymentDate.ToShortDateString();
        /// <summary>
        /// Дата увольнения
        /// </summary>
        public DateTime? DismissalDate { get; set; }
        public string DissimalDateString => DismissalDate?.ToShortDateString() ?? "-";
        /// <summary>
        /// Оклад
        /// </summary>
        public decimal Salary { get; set; }
        public string SalaryText => Salary.ToString("c");
        public bool IsWorkingTillNow { get; set; }

        public bool IsDesmissalDateEnabled => !IsWorkingTillNow;
        public void LoadViewModel(CardViewModel model)
        {
            Mapping.Mapper.Map(model, this);
            //Id = model.Id;
            //FirstName = model.FirstName;
            //MiddleName = model.MiddleName;
            //LastName = model.LastName;
            //BirthDate = model.BirthDate;
            //Department = model.Department;
            //Position = model.Position;
            //EmploymentDate = model.EmploymentDate;
            //DismissalDate = model.DismissalDate;
            //Salary = model.Salary;
            IsWorkingTillNow = !model.DismissalDate.HasValue;

            UpdateAll();
        }

        public void IsWorkingTillNowChecked()
        {
            DismissalDate = null;
            OnPropertyChanged(nameof(DismissalDate));
            OnPropertyChanged(nameof(IsDesmissalDateEnabled));
        }
        public void IsWorkingTillNowUnChecked()
        {
            DismissalDate = DateTime.Today;
            OnPropertyChanged(nameof(DismissalDate));
            OnPropertyChanged(nameof(IsDesmissalDateEnabled));
        }
        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(MiddleName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(FIO));
            OnPropertyChanged(nameof(BirthDate));
            OnPropertyChanged(nameof(Department));
            OnPropertyChanged(nameof(Position));
            OnPropertyChanged(nameof(EmploymentDate));
            OnPropertyChanged(nameof(DismissalDate));
            OnPropertyChanged(nameof(Salary));
            OnPropertyChanged(nameof(IsWorkingTillNow));
            OnPropertyChanged(nameof(IsDesmissalDateEnabled));
            OnPropertyChanged(nameof(SalaryText));
            OnPropertyChanged(nameof(BirthDateString));
            OnPropertyChanged(nameof(EmploymentDateString));
            OnPropertyChanged(nameof(DissimalDateString));
        }
    }
}
