using Saper.Models;
using Saper.Services;
using Saper.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Saper.WPF.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private GameService _gameService = new GameService();
        private GameModel _gameModel = new GameModel();
        private bool _isGameOver = false;
        private bool _showBombs = false; 

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public ICommand NewGameCommand { get; }
        public ICommand CellOpenCommand { get; }
        public ICommand CellFlagCommand { get; }
        public ICommand ShowBombsCommand { get; } 

        private string _statusText;
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
            ShowBombsCommand = new RelayCommand(ToggleShowBombs); 

            StartNewGame();
        }

        
        private void ToggleShowBombs()
        {
            _showBombs = !_showBombs; 
            UpdateCellsFromModel();

            if (_showBombs)
                StatusText = $"Ходов: {_gameModel.Moves} - Бомбы показаны";
            else
                StatusText = $"Ходов: {_gameModel.Moves}";
        }

        public void StartNewGame()
        {
            _isGameOver = false;
            _showBombs = false; // Сбрасываем при новой игре
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
                        IsGameOver = _isGameOver,
                        ShowBombs = _showBombs, // Передаем состояние показа
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
