using SimpleMinesweeper.Models;
using SimpleMinesweeper.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;

namespace SimpleMinesweeper.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private GameService _gameService = new GameService();
        private GameModel _gameModel = new GameModel();
        private bool _isGameOver = false;

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public ICommand NewGameCommand { get; }
        public ICommand CellOpenCommand { get; }
        public ICommand CellFlagCommand { get; }

        private string _statusText = "Ходов: 0";
        public string StatusText
        {
            get => _statusText;
            set
            {
                _statusText = value;
                OnPropertyChanged(nameof(StatusText));
            }
        }

        public MainWindowViewModel()
        {
            NewGameCommand = new RelayCommand(StartNewGame);
            CellOpenCommand = new RelayCommand<CellViewModel>(OpenCell);
            CellFlagCommand = new RelayCommand<CellViewModel>(ToggleFlag);

            StartNewGame();
        }

        public void StartNewGame()
        {
            _isGameOver = false;
            _gameService.InitializeGame(_gameModel);
            UpdateCellsFromModel();
            UpdateStatusText();
        }

        private void OpenCell(CellViewModel cellVm)
        {
            if (_isGameOver) return;

            _gameService.OpenCell(_gameModel, cellVm.Row, cellVm.Column);
            UpdateCellsFromModel();
            UpdateStatusText();

            if (_gameModel.State != GameState.Playing)
            {
                _isGameOver = true;

                string message = _gameModel.State == GameState.Won
                    ? $"Поздравляю! Вы выиграли за {_gameModel.Moves} ходов!"
                    : "Вы наткнулись на бомбу! Игра окончена.";

                // Обновляем ViewModel для отображения всех бомб
                UpdateCellsFromModel();

                if (MessageBox.Show(message + " Начать новую игру?", "Игра окончена",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    StartNewGame();
                }
            }
        }

        private void ToggleFlag(CellViewModel cellVm)
        {
            if (_isGameOver) return;

            _gameService.ToggleFlag(_gameModel, cellVm.Row, cellVm.Column);
            UpdateCellsFromModel();
        }

        private void UpdateCellsFromModel()
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.Size; row++)
            {
                for (int col = 0; col < GameModel.Size; col++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = col,
                        IsOpened = _gameModel.OpenedCells[row, col],
                        IsFlagged = _gameModel.FlaggedCells[row, col],
                        HasBomb = _gameModel.Board[row, col] == 1,
                        AdjacentBombs = _gameService.CountAdjacentBombs(_gameModel, row, col),
                        IsGameOver = _isGameOver, // Передаем состояние игры
                        OpenCommand = CellOpenCommand,
                        FlagCommand = CellFlagCommand
                    });
                }
            }
        }

        private void UpdateStatusText()
        {
            StatusText = $"Ходов: {_gameModel.Moves}";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Реализации ICommand (оставляем как было)
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        public RelayCommand(Action execute) => _execute = execute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _execute();
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;

        public RelayCommand(Action<T> execute) => _execute = execute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _execute((T)parameter);
    }
}