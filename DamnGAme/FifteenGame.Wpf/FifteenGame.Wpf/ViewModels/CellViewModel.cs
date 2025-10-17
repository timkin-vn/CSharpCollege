using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private char _state;
        public char State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged(nameof(State));
                OnPropertyChanged(nameof(Text));
                OnPropertyChanged(nameof(Color));
            }
        }

        public string Text { get { return GetText(); } }

        public Brush Color { get { return (Brush)new BrushConverter().ConvertFrom(GetColor()); } }

        public int Row { get; set; }
        public int Column { get; set; }
        public MoveDirection Direction { get; set; } // Сохраняем для структуры

        private string GetText()
        {
            if (State == 'H') return "X";
            if (State == 'M') return "O";
            if (State == 'F') return "F";
            return "";
        }

        private string GetColor()
        {
            if (State == 'H') return "Red";
            if (State == 'M') return "Gray";
            if (State == 'F') return "Yellow";
            return "LightBlue"; // Пустая клетка
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}