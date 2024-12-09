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
        private string _lettClass;

        private string _numClass;

        private string _classLed;

        private string _badClass;
    
        private int _childrenCount;

        public int Id { get; set; }

        public string Fio => $"{NumClass}{LettClass}";

        public string LettClass
        {
            get => _lettClass;
            set
            {
                _lettClass = value;
                OnPropertyChanged(nameof(Fio));
                OnPropertyChanged(nameof(LettClass));
            }
        }

        public string ClassLed
        {
            get => _classLed;
            set
            {
                _classLed = value;
                OnPropertyChanged(nameof(ClassLed));
            }
        }
        public string BadClass
        {
            get => _badClass;
            set
            {
                _badClass= value;
                OnPropertyChanged(nameof(BadClass));
            }
        }
        public string NumClass
        {
            get => _numClass;
            set
            {
                _numClass = value;
                OnPropertyChanged(nameof(Fio));
                OnPropertyChanged(nameof(NumClass));
            }
        }

       
        public int ChildrenCount
        {
            get => _childrenCount;
            set
            {
                _childrenCount = value;
                OnPropertyChanged(nameof(ChildrenCount));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewModel model)
        {
            Id = model.Id;
            LettClass = model.LettClass;
            ClassLed = model.ClassLed;
            NumClass = model.NumClass;
            BadClass = model.BadClass;
            ChildrenCount = model.ChildrenCount;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
