using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string Fio => $"{LastName} — \"{FirstName}\"";

        public DateTime BirthDate { get; set; }
        public string BirthDateText => BirthDate.ToShortDateString();

        public string Department { get; set; } 
        public string Position { get; set; }

        public DateTime EmploymentDate { get; set; }
        public DateTime? DismissalDate { get; set; }

        public decimal Salary { get; set; }
        public string SalaryText => Salary.ToString("c");

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
