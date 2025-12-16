using Nonogram.Business.Models;
using System;
using System.ComponentModel;

namespace Nonogram.Wpf.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private CellState _state;
        private bool _isCompleted;

        public int Row { get; set; }
        public int Column { get; set; }

        public CellState State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged(nameof(State));
                OnPropertyChanged(nameof(IsCrossed));
            }
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }
        public bool IsCrossed => State == CellState.Crossed;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}