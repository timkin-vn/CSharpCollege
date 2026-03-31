using FrogGame.Business.Models;
using FrogGame.Business.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace FrogGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private GameService _service = new GameService();
        private GameField _model = new GameField();

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public ICommand CellClickCommand { get; }
        public ICommand RestartCommand { get; }

        private string _gameStatus = "Начните игру!";
        public string GameStatus
        {
            get => _gameStatus;
            set
            {
                _gameStatus = value;
                OnPropertyChanged(nameof(GameStatus));
            }
        }

        private bool _isGameOver;
        public bool IsGameOver
        {
            get => _isGameOver;
            set
            {
                _isGameOver = value;
                OnPropertyChanged(nameof(IsGameOver));
            }
        }

        public bool HasSelectedLilyPad => _model.SelectedLilyPadRow.HasValue;

        public MainWindowViewModel()
        {
            CellClickCommand = new RelayCommand(OnCellClick);
            RestartCommand = new RelayCommand(OnRestart);
            Initialize();
        }

        public void Initialize()
        {
            _service.Initialize(_model);
            UpdateFromModel();
            UpdateGameStatus();
        }

        private void OnCellClick(object parameter)
        {
            if (parameter is CellViewModel cell && !_model.IsGameOver)
            {
                if (cell.Type == CellType.Algae)
                {
                    _service.RemoveAlgae(_model, cell.Row, cell.Column);
                    UpdateFromModel();
                    UpdateGameStatus();
                    OnPropertyChanged(nameof(HasSelectedLilyPad));
                }
                else if (cell.Type == CellType.LilyPad)
                {
                    if (HasSelectedLilyPad)
                    {
                        _service.SelectLilyPad(_model, cell.Row, cell.Column);
                    }
                    else
                    {
                        _service.SelectLilyPad(_model, cell.Row, cell.Column);
                    }
                    UpdateFromModel();
                    UpdateGameStatus();
                    OnPropertyChanged(nameof(HasSelectedLilyPad));
                }
                else if (HasSelectedLilyPad && cell.Type == CellType.Water)
                {
                    _service.MoveLilyPad(_model, cell.Row, cell.Column);
                    UpdateFromModel();
                    UpdateGameStatus();
                    OnPropertyChanged(nameof(HasSelectedLilyPad));
                }
            }
        }

        private void OnRestart(object parameter)
        {
            Initialize();
            OnPropertyChanged(nameof(HasSelectedLilyPad));
        }

        private void UpdateFromModel()
        {
            Cells.Clear();
            for (int row = 0; row < GameField.RowCount; row++)
            {
                for (int column = 0; column < GameField.ColumnCount; column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        Type = _model[row, column]
                    });
                }
            }
        }

        private void UpdateGameStatus()
        {
            IsGameOver = _model.IsGameOver;

            if (_model.IsGameOver)
            {
                if (_model.IsWin)
                {
                    GameStatus = $"🎉 Победа! Лягушка дома! 🎉\nПотрачено ходов: {_model.MovesCount}";
                }
                else
                {
                    GameStatus = "Игра завершена";
                }
            }
            else
            {
                string mode = HasSelectedLilyPad ?
                    "Выберите куда переместить кувшинку (кликните на воду)" :
                    "Убирайте водоросли (🌱) или выбирайте кувшинки (🌿)";
                GameStatus = $"Ходов: {_model.MovesCount} - {mode}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
    }
}