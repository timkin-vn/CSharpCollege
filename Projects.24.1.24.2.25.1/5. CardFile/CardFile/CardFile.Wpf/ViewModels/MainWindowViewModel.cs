using CardFile.Business.Models;
using CardFile.Business.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace CardFile.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private StudentFileService _service = new StudentFileService();
        public ObservableCollection<StudentViewModel> Students { get; } = new ObservableCollection<StudentViewModel>();
        public StudentViewModel SelectedStudent { get; set; }
        public bool IsEditButtonEnabled => SelectedStudent != null;
        public bool IsDeleteButtonEnabled => SelectedStudent != null;
        public string FileName { get; private set; }
        public string WindowTitle => string.IsNullOrEmpty(FileName) ? "Картотека студентов" : $"Картотека студентов: {Path.GetFileName(FileName)}";

        public void WindowLoaded() => LoadAllData();

        public StudentViewModel GetSelectedStudent() => SelectedStudent;

        public StudentViewModel GetNewStudent() => new StudentViewModel
        {
            BirthDate = new DateTime(2005, 1, 1),
            AdmissionDate = DateTime.Today,
            Course = 1,
            AverageGrade = 4.0
        };

        public void SaveEditedStudent(StudentViewModel student)
        {
            var index = Students.ToList().FindIndex(s => s.Id == student.Id);
            if (index < 0) throw new Exception("Студент с таким Id не существует");

            var id = _service.Save(ToBusiness(student));
            if (id < 0) Students.RemoveAt(index);
            else Students[index].LoadViewModel(student);
        }

        public void SaveNewStudent(StudentViewModel student)
        {
            var newStudent = new StudentViewModel();
            newStudent.LoadViewModel(student);
            var id = _service.Save(ToBusiness(student));
            if (id < 0) return;
            newStudent.Id = id;
            Students.Add(newStudent);
        }

        public void DeleteSelectedStudent()
        {
            if (SelectedStudent == null) return;
            _service.Delete(SelectedStudent.Id);
            var index = Students.ToList().FindIndex(s => s.Id == SelectedStudent.Id);
            Students.RemoveAt(index);
            SelectedStudent = null;
            OnPropertyChanged(nameof(SelectedStudent));
        }

        public void SelectionChanged()
        {
            OnPropertyChanged(nameof(IsDeleteButtonEnabled));
            OnPropertyChanged(nameof(IsEditButtonEnabled));
        }

        public void SaveToFile(string fileName)
        {
            _service.SaveToFile(fileName);
            FileName = fileName;
            OnPropertyChanged(nameof(WindowTitle));
        }

        public void SaveToFile()
        {
            if (string.IsNullOrEmpty(FileName))
                throw new InvalidOperationException("Файл не выбран");
            _service.SaveToFile(FileName);
        }

        public void OpenFromFile(string fileName)
        {
            _service.OpenFromFile(fileName);
            LoadAllData();
            FileName = fileName;
            OnPropertyChanged(nameof(WindowTitle));
        }
        private void LoadAllData()
        {
            Students.Clear();
            foreach (var student in _service.GetAll())
                Students.Add(ToViewModel(student));
        }

        private StudentViewModel ToViewModel(Student student) => new StudentViewModel
        {
            Id = student.Id,
            LastName = student.LastName,
            FirstName = student.FirstName,
            MiddleName = student.MiddleName,
            BirthDate = student.BirthDate,
            Faculty = student.Faculty,
            Course = student.Course,
            Group = student.Group,
            RecordBookNumber = student.RecordBookNumber,
            AverageGrade = student.AverageGrade,
            AdmissionDate = student.AdmissionDate,
            DismissalDate = student.DismissalDate
        };

        private Student ToBusiness(StudentViewModel student) => new Student
        {
            Id = student.Id,
            LastName = student.LastName,
            FirstName = student.FirstName,
            MiddleName = student.MiddleName,
            BirthDate = student.BirthDate,
            Faculty = student.Faculty,
            Course = student.Course,
            Group = student.Group,
            RecordBookNumber = student.RecordBookNumber,
            AverageGrade = student.AverageGrade,
            AdmissionDate = student.AdmissionDate,
            DismissalDate = student.DismissalDate
        };
    }
}