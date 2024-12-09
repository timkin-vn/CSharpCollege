using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : INotifyPropertyChanged
    {

        private string _CourseNumber;

        private string _Tutor;

        private string _SpecialProgram;
    
        private int _NumberStudents;

        public int Id { get; set; }

        public string Fio => $"{CourseNumber}";

        public string Tutor
        {
            get => _Tutor;
            set
            {
                _Tutor = value;
                OnPropertyChanged(nameof(Tutor));
            }
        }
        public string SpecialProgram
        {
            get => _SpecialProgram;
            set
            {
                _SpecialProgram = value;
                OnPropertyChanged(nameof(SpecialProgram));
            }
        }
        public string CourseNumber
        {
            get => _CourseNumber;
            set
            {
                _CourseNumber = value;
                OnPropertyChanged(nameof(Fio));
                OnPropertyChanged(nameof(CourseNumber));
            }
        }

       
        public int NumberStudents
        {
            get => _NumberStudents;
            set
            {
                _NumberStudents = value;
                OnPropertyChanged(nameof(NumberStudents));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewModel model)
        {
            Id = model.Id;
            Tutor = model.Tutor;
            CourseNumber = model.CourseNumber;
            SpecialProgram = model.SpecialProgram;
            NumberStudents = model.NumberStudents;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
