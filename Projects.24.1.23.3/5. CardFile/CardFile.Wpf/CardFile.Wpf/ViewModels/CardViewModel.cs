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

        public string Fio => $"{LastName} {FirstName} {MiddleName[0]}.";

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
        /// Оклад
        /// </summary>
        public decimal Salary { get; set; }

        public bool IsWorkingTillNow { get; set; }

        public bool IsDismissalDateEnabled => !IsWorkingTillNow;

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
            OnPropertyChanged(nameof(IsDismissalDateEnabled));
        }

        public void IsWorkingTillNowUnchecked()
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
            OnPropertyChanged(nameof(IsWorkingTillNow));
            OnPropertyChanged(nameof(IsDismissalDateEnabled));
        }
    }
}
