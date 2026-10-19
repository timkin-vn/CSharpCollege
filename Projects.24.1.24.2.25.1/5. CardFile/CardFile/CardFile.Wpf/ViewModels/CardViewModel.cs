using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        public string Fio => $"{LastName} {FirstName} {MiddleName}";

        public DateTime BirthDate { get; set; }
        public string BirthDateText => BirthDate.ToShortDateString();

        public string Department { get; set; }
        public string Position { get; set; }

        public DateTime EmploymentDate { get; set; }
        public string EmploymentDateText => EmploymentDate.ToShortDateString();

        public DateTime? DismissalDate { get; set; }
        public string DismissalDateText => DismissalDate?.ToShortDateString() ?? "-";

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
            OnPropertyChanged(nameof(MiddleName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Fio));
            OnPropertyChanged(nameof(Phone));
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