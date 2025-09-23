using Minesweeper.Business.Models;
using Minesweeper.Business.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Minesweeper.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private GameService _gameService = new GameService();
        private MineField _mineField = new MineField();

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        private string _gameStatus = "Добро пожаловать в Сапёр!";
        public string GameStatus
        {
            get => _gameStatus;
            set
            {
                _gameStatus = value;
                OnPropertyChanged(nameof(GameStatus));
            }
        }

        public ICommand NewGameCommand { get; }
        public ICommand CellRevealCommand { get; }
        public ICommand CellFlagCommand { get; }

        public MainWindowViewModel()
        {
            NewGameCommand = new RelayCommand(StartNewGame);
            CellRevealCommand = new RelayCommand<CellViewModel>(RevealCell);
            CellFlagCommand = new RelayCommand<CellViewModel>(ToggleFlag);

            InitializeGame();
        }

        private void InitializeGame()
        {
            _gameService.InitializeGame(_mineField);
            UpdateCellsCollection();
            UpdateGameStatus();
        }

        private void UpdateCellsCollection()
        {
            Cells.Clear();
            for (int row = 0; row < _mineField.Rows; row++)
            {
                for (int col = 0; col < _mineField.Columns; col++)
                {
                    var cellViewModel = new CellViewModel(_mineField.Cells[row, col]);
                    cellViewModel.RevealCommand = CellRevealCommand;
                    cellViewModel.FlagCommand = CellFlagCommand;
                    Cells.Add(cellViewModel);
                }
            }
        }

        private void RevealCell(CellViewModel cellViewModel)
        {
            _gameService.RevealCell(_mineField, cellViewModel.Row, cellViewModel.Column);
            UpdateAllCells();
            UpdateGameStatus();

            if (_mineField.State == GameState.Defeat)
            {
                MessageBox.Show("Вы проиграли! Нажата мина!", "Игра окончена",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (_mineField.State == GameState.Victory)
            {
                MessageBox.Show("Поздравляем! Вы победили!", "Победа!",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ToggleFlag(CellViewModel cellViewModel)
        {
            _gameService.ToggleFlag(_mineField, cellViewModel.Row, cellViewModel.Column);
            UpdateAllCells();
            UpdateGameStatus();
        }

        private void UpdateAllCells()
        {
            foreach (var cellViewModel in Cells)
            {
                cellViewModel.Update();
            }
        }

        private void UpdateGameStatus()
        {
            // Заменяем switch expression на обычный switch
            switch (_mineField.State)
            {
                case GameState.NotStarted:
                    GameStatus = "Игра не начата";
                    break;
                case GameState.InProgress:
                    GameStatus = $"Идёт игра. Открыто: {_mineField.RevealedCellsCount}";
                    break;
                case GameState.Victory:
                    GameStatus = "ПОБЕДА! Все мины обезврежены!";
                    break;
                case GameState.Defeat:
                    GameStatus = "ПОРАЖЕНИЕ! Вы наступили на мину!";
                    break;
                default:
                    GameStatus = "Неизвестное состояние игры";
                    break;
            }
        }

        private void StartNewGame()
        {
            _gameService.StartNewGame(_mineField);
            UpdateCellsCollection();
            UpdateGameStatus();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Упрощенные классы команд
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _execute();

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;

        public RelayCommand(Action<T> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            if (parameter is T typedParameter)
            {
                _execute(typedParameter);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}