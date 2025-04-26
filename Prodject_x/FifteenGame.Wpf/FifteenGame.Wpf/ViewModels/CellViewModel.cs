using System;
using System.ComponentModel;
using System.Windows.Input;

namespace FifteenGame.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private readonly int _row;
        private readonly int _col;
        private bool _isOpen;
        private bool _isFlagged;
        private bool _isMine;
        private int _neighborMines;

        public event PropertyChangedEventHandler PropertyChanged;

        public CellViewModel(int row, int col)
        {
            _row = row;
            _col = col;
            OpenCommand = new RelayCommand(Open);
            FlagCommand = new RelayCommand(Flag);
        }

        public ICommand OpenCommand { get; }
        public ICommand FlagCommand { get; }

        public int Row => _row;
        public int Col => _col;

        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                _isOpen = value;
                OnPropertyChanged(nameof(IsOpen));
            }
        }

        public bool IsFlagged
        {
            get => _isFlagged;
            set
            {
                _isFlagged = value;
                OnPropertyChanged(nameof(IsFlagged));
            }
        }

        public bool IsMine
        {
            get => _isMine;
            set
            {
                _isMine = value;
                OnPropertyChanged(nameof(IsMine));
            }
        }

        public int NeighborMines
        {
            get => _neighborMines;
            set
            {
                _neighborMines = value;
                OnPropertyChanged(nameof(NeighborMines));
            }
        }

        public Action<int, int> OnOpen { get; set; }
        public Action<int, int> OnFlag { get; set; }

        private void Open(object parameter)
        {
            OnOpen?.Invoke(_row, _col);
        }

        private void Flag(object parameter)
        {
            OnFlag?.Invoke(_row, _col);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Простая реализация ICommand
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        public RelayCommand(Action<object> execute) => _execute = execute;
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _execute(parameter);
    }
}