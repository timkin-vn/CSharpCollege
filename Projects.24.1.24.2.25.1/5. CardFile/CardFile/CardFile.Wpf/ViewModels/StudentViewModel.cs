using System;

namespace CardFile.Wpf.ViewModels
{
    public class StudentViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public string Fio => $"{LastName} {FirstName} {MiddleName}".Trim();

        public DateTime BirthDate { get; set; }
        public string BirthDateText => BirthDate.ToShortDateString();

        public string Faculty { get; set; }
        public int Course { get; set; }
        public string Group { get; set; }
        public string RecordBookNumber { get; set; }
        public double AverageGrade { get; set; }
        public string AverageGradeText => AverageGrade.ToString("F2");

        public DateTime AdmissionDate { get; set; }
        public string AdmissionDateText => AdmissionDate.ToShortDateString();

        public DateTime? DismissalDate { get; set; }
        public string DismissalDateText => DismissalDate?.ToShortDateString() ?? "Обучается";

        public bool IsStudyingNow { get; set; }
        public bool IsDismissalDateEnabled => !IsStudyingNow;

        public void IsStudyingNowChecked()
        {
            DismissalDate = null;
            OnPropertyChanged(nameof(DismissalDate));
            OnPropertyChanged(nameof(IsDismissalDateEnabled));
        }

        public void IsStudyingNowUnchecked()
        {
            DismissalDate = DateTime.Today;
            OnPropertyChanged(nameof(DismissalDate));
            OnPropertyChanged(nameof(IsDismissalDateEnabled));
        }

        public void LoadViewModel(StudentViewModel student)
        {
            Id = student.Id;
            LastName = student.LastName;
            FirstName = student.FirstName;
            MiddleName = student.MiddleName;
            BirthDate = student.BirthDate;
            Faculty = student.Faculty;
            Course = student.Course;
            Group = student.Group;
            RecordBookNumber = student.RecordBookNumber;
            AverageGrade = student.AverageGrade;
            AdmissionDate = student.AdmissionDate;
            DismissalDate = student.DismissalDate;
            IsStudyingNow = !student.DismissalDate.HasValue;

            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(MiddleName));
            OnPropertyChanged(nameof(Fio));
            OnPropertyChanged(nameof(BirthDate));
            OnPropertyChanged(nameof(Faculty));
            OnPropertyChanged(nameof(Course));
            OnPropertyChanged(nameof(Group));
            OnPropertyChanged(nameof(RecordBookNumber));
            OnPropertyChanged(nameof(AverageGrade));
            OnPropertyChanged(nameof(AdmissionDate));
            OnPropertyChanged(nameof(DismissalDate));
            OnPropertyChanged(nameof(IsStudyingNow));
            OnPropertyChanged(nameof(IsDismissalDateEnabled));
        }
    }
}