using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FifteenGame.Business.Models;

namespace FifteenGame.Wpf.ViewModels
{
    public class TicTacToeCellViewModel : INotifyPropertyChanged
    {
        private readonly int _position;
        private CellState _state;
        private readonly Action<int> _moveAction;
        private bool _isMouseOver;

        public event PropertyChangedEventHandler PropertyChanged;

        public TicTacToeCellViewModel(int position, Action<int> moveAction)
        {
            _position = position;
            _moveAction = moveAction;
            ClickCommand = new RelayCommand(MakeMove, CanMakeMove);
        }

        public CellState State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayText));
                OnPropertyChanged(nameof(IsEmpty));
            }
        }

        public bool IsMouseOver
        {
            get { return _isMouseOver; }
            set
            {
                _isMouseOver = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public string DisplayText
        {
            get
            {
                switch (State)
                {
                    case CellState.X:
                        return "X";
                    case CellState.O:
                        return "O";
                    default:
                        return "";
                }
            }
        }

        public bool IsEmpty => State == CellState.Empty;

        public ICommand ClickCommand { get; }

        private void MakeMove()
        {
            _moveAction?.Invoke(_position);
        }

        private bool CanMakeMove()
        {
            return State == CellState.Empty;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
} 