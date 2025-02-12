using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048.Business.Models;

namespace Game2048.WPF.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private int _num;
        public int Num
        {
            get => _num;
            set
            {
                _num = value;
                OnPropertyChanged(nameof(Num));
                OnPropertyChanged(nameof(Text));
            }
        }

        public string Text => Num == 0 ? string.Empty : Num.ToString();

        public int Row { get; set; }
        public int Column { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
