using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private GameService _service = new GameService();
        private GameModel _model = new GameModel();
        private MainWindow _mainWindow;

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public ICommand CellClickCommand { get; }
        public ICommand NewGameCommand { get; }
        public ICommand ApplySettingsCommand { get; }

        public int RowCount => GameModel.RowCount;
        public int ColumnCount => GameModel.ColumnCount;

        private int _minesCount = 15;
        public int MinesCount
        {
            get => _minesCount;
            set
            {
                if (value < 5)
                    _minesCount = 5;
                else if (value > 50)
                    _minesCount = 50;
                else
                    _minesCount = value;

                OnPropertyChanged(nameof(MinesCount));
            }
        }

        private int _totalMines;
        public int TotalMines
        {
            get => _totalMines;
            set { _totalMines = value; OnPropertyChanged(nameof(TotalMines)); }
        }

        private int _flagsCount;
        public int FlagsCount
        {
            get => _flagsCount;
            set { _flagsCount = value; OnPropertyChanged(nameof(FlagsCount)); }
        }

        private string _gameStatus = "Игра начата!";
        public string GameStatus
        {
            get => _gameStatus;
            set { _gameStatus = value; OnPropertyChanged(nameof(GameStatus)); }
        }

        private Brush _statusColor = Brushes.Black;
        public Brush StatusColor
        {
            get => _statusColor;
            set { _statusColor = value; OnPropertyChanged(nameof(StatusColor)); }
        }

        private bool _isGameOver;
        public bool IsGameOver
        {
            get => _isGameOver;
            set { _isGameOver = value; OnPropertyChanged(nameof(IsGameOver)); }
        }

        public MainWindowViewModel()
        {
            CellClickCommand = new RelayCommand<CellViewModel>(OnCellClick);
            NewGameCommand = new RelayCommand(StartNewGame);
            ApplySettingsCommand = new RelayCommand(ApplySettings);

            // ссылка на главное окно для анимаций
            _mainWindow = Application.Current.MainWindow as MainWindow;

            StartNewGame();
        }

        public void StartNewGame()
        {
            _service.Initialize(_model, MinesCount);
            FromModel(_model);
            UpdateGameInfo();
            GameStatus = $"Игра начата! Мины: {MinesCount}";
            StatusColor = Brushes.Black;
            IsGameOver = false;
        }

        private void ApplySettings()
        {
            if (MinesCount < 5) MinesCount = 5;
            if (MinesCount > 50) MinesCount = 50;

            StartNewGame();
        }

        private async void OnCellClick(CellViewModel cell)
        {
            if (cell.IsFlagged || IsGameOver)
                return;

            _service.RevealCell(_model, cell.Row, cell.Column);
            FromModel(_model);
            UpdateGameInfo();

            if (_model.GameState == GameState.GameOver)
            {
                // Помечаем клетку, на которую нажали, как взорвавшуюся
                MarkExplodedCell(cell.Row, cell.Column);

                GameStatus = "Игра окончена! Вы проиграли! Перезапуск через 3 секунды...";
                StatusColor = Brushes.Red;
                IsGameOver = true;

                // Запускаем анимации
                _mainWindow?.PlayGameOverAnimations();

                RevealAllMines();

                // Увеличиваем время перед перезапуском для анимаций
                await Task.Delay(3000);
                StartNewGame();
            }
            else if (_model.GameState == GameState.Won)
            {
                GameStatus = "Поздравляем! Вы выиграли!";
                StatusColor = Brushes.Green;
                IsGameOver = true;
            }
        }

        private void MarkExplodedCell(int row, int column)
        {
            foreach (var cell in Cells)
            {
                if (cell.Row == row && cell.Column == column)
                {
                    cell.IsExploded = true;
                    break;
                }
            }
        }

        public void ToggleFlag(CellViewModel cell)
        {
            if (!cell.IsRevealed && !IsGameOver)
            {
                _service.ToggleFlag(_model, cell.Row, cell.Column);
                FromModel(_model);
                UpdateGameInfo();
            }
        }

        private void UpdateGameInfo()
        {
            TotalMines = _model.TotalMines;
            FlagsCount = _model.FlagsPlaced;
        }

        private void RevealAllMines()
        {
            foreach (var cell in Cells)
            {
                if (_model.HasMine(cell.Row, cell.Column) && !cell.IsRevealed)
                {
                    _model.SetRevealed(cell.Row, cell.Column, true);
                }
            }
            FromModel(_model);
        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        AdjacentMines = model.GetAdjacentMines(row, column),
                        IsRevealed = model.IsRevealed(row, column),
                        HasMine = model.HasMine(row, column),
                        IsFlagged = model.IsFlagged(row, column)
                    });
                }
            }
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
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;
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
        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke((T)parameter) ?? true;
        public void Execute(object parameter) => _execute((T)parameter);

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}