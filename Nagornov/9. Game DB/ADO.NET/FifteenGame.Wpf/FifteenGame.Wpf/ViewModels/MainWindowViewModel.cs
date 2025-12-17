using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IGameService _gameService;
        private readonly IUserService _userService;
        private UserModel _currentUser;
        private GameModel _currentGame;
        private Random _random = new Random();

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();
        public ICommand CellClickCommand { get; }
        public ICommand NewGameCommand { get; }
        public ICommand ApplySettingsCommand { get; }

        private bool _showLoginPanel = true;
        public bool ShowLoginPanel
        {
            get => _showLoginPanel;
            set { _showLoginPanel = value; OnPropertyChanged(); OnPropertyChanged(nameof(ShowGamePanel)); }
        }

        public bool ShowGamePanel => !_showLoginPanel;

        private int _minesCount = 15;
        public int MinesCount
        {
            get => _minesCount;
            set
            {
                if (value < 5) _minesCount = 5;
                else if (value > 50) _minesCount = 50;
                else _minesCount = value;
                OnPropertyChanged();
            }
        }

        private int _totalMines = 15;
        public int TotalMines
        {
            get => _totalMines;
            set { _totalMines = value; OnPropertyChanged(); }
        }

        private string _gameStatus = "Нажмите 'Новая игра'";
        public string GameStatus
        {
            get => _gameStatus;
            set { _gameStatus = value; OnPropertyChanged(); }
        }

        private Brush _statusColor = Brushes.Black;
        public Brush StatusColor
        {
            get => _statusColor;
            set { _statusColor = value; OnPropertyChanged(); }
        }

        private int _flagsCount = 0;
        public int FlagsCount
        {
            get => _flagsCount;
            set { _flagsCount = value; OnPropertyChanged(); }
        }

        private int _moveCount = 0;
        public int MoveCount
        {
            get => _moveCount;
            set { _moveCount = value; OnPropertyChanged(); }
        }

        private bool _isGameOver = false;
        public bool IsGameOver
        {
            get => _isGameOver;
            set { _isGameOver = value; OnPropertyChanged(); }
        }

        private string _userName = "";
        public string UserName
        {
            get => _userName;
            set { _userName = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel(IGameService gameService, IUserService userService)
        {
            Console.WriteLine("=== СОЗДАНИЕ VIEWMODEL ===");

            _gameService = gameService;
            _userService = userService;

            CellClickCommand = new RelayCommand<CellViewModel>(OnCellClick);
            NewGameCommand = new RelayCommand(StartNewGame);
            ApplySettingsCommand = new RelayCommand(ApplySettings);

            Console.WriteLine("ViewModel создан");
        }

        public void SetUser(UserModel user)
        {
            Console.WriteLine($"=== УСТАНОВКА ПОЛЬЗОВАТЕЛЯ ===");
            Console.WriteLine($"ID: {user.Id}, Name: '{user.Name}'");

            _currentUser = user;
            UserName = user.Name;
            ShowLoginPanel = false;
            LoadCurrentGame();
        }

        public void ClearUser()
        {
            Console.WriteLine("=== ОЧИСТКА ПОЛЬЗОВАТЕЛЯ ===");
            _currentUser = null;
            UserName = "";
            ShowLoginPanel = true;
            Cells.Clear();
            _currentGame = null;
            GameStatus = "Нажмите 'Новая игра'";
        }

        private void LoadCurrentGame()
        {
            if (_currentUser == null || _currentUser.Id <= 0)
                return;

            try
            {
                _currentGame = _gameService.GetCurrentGame(_currentUser.Id);
                if (_currentGame != null)
                {
                    Console.WriteLine($"Загружена сохраненная игра ID={_currentGame.Id}");
                    LoadGameFromModel(_currentGame);
                }
                else
                {
                    StartNewGame();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки игры: {ex.Message}");
                StartNewGame();
            }
        }

        public void StartNewGame()
        {
            if (_currentUser == null || _currentUser.Id <= 0)
            {
                Console.WriteLine("Не могу начать игру: пользователь не определен");
                return;
            }

            Console.WriteLine($"=== НОВАЯ ИГРА ===");
            Console.WriteLine($"Пользователь: ID={_currentUser.Id}, Name='{_currentUser.Name}'");
            Console.WriteLine($"Количество мин: {MinesCount}");

            FlagsCount = 0;
            MoveCount = 0;
            TotalMines = MinesCount;
            GameStatus = $"Новая игра! Мины: {MinesCount}";
            StatusColor = Brushes.Black;
            IsGameOver = false;

            try
            {
                _currentGame = _gameService.StartNewGame(_currentUser.Id, MinesCount);
                Console.WriteLine($"Создана новая игра ID={_currentGame?.Id}");
                Cells.Clear();
                InitializeCellsFromModel(_currentGame);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка создания игры: {ex.Message}");
                GameStatus = "Ошибка создания игры!";
                StatusColor = Brushes.Red;
            }
        }

        private void InitializeCellsFromModel(GameModel game)
        {
            if (game == null) return;

            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        AdjacentMines = game[row, column] >= 0 ? game[row, column] : 0,
                        HasMine = game.HasMine(row, column),
                        IsRevealed = game.IsRevealed(row, column),
                        IsFlagged = game[row, column] == -3
                    });
                }
            }
        }

        private void LoadGameFromModel(GameModel game)
        {
            if (game == null) return;

            MinesCount = game.MinesCount;
            MoveCount = game.MoveCount;
            FlagsCount = game.FlagsCount;
            IsGameOver = game.GameState != GameState.Playing;

            Cells.Clear();
            InitializeCellsFromModel(game);

            switch (game.GameState)
            {
                case GameState.Won:
                    GameStatus = "Поздравляем! Вы выиграли!";
                    StatusColor = Brushes.Green;
                    break;
                case GameState.GameOver:
                    GameStatus = "Игра окончена! Вы проиграли!";
                    StatusColor = Brushes.Red;
                    break;
                default:
                    GameStatus = $"Игра продолжается... Ходов: {MoveCount}";
                    StatusColor = Brushes.Black;
                    break;
            }
        }

        private void OnCellClick(CellViewModel cell)
        {
            if (cell.IsFlagged || IsGameOver || cell.IsRevealed || _currentGame == null)
                return;

            Console.WriteLine($"Клик по клетке: Row={cell.Row}, Column={cell.Column}");

            try
            {
                _currentGame = _gameService.RevealCell(_currentGame.Id, cell.Row, cell.Column);
                if (_currentGame != null)
                {
                    UpdateUIFromGameModel();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка обработки хода: {ex.Message}");
            }
        }

        public void ToggleFlag(CellViewModel cell)
        {
            if (!cell.IsRevealed && !IsGameOver && _currentGame != null)
            {
                try
                {
                    _currentGame = _gameService.ToggleFlag(_currentGame.Id, cell.Row, cell.Column);
                    if (_currentGame != null)
                    {
                        UpdateUIFromGameModel();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка установки флага: {ex.Message}");
                }
            }
        }

        private void UpdateUIFromGameModel()
        {
            if (_currentGame == null) return;

            MoveCount = _currentGame.MoveCount;
            FlagsCount = _currentGame.FlagsCount;

            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    var cell = Cells.FirstOrDefault(c => c.Row == row && c.Column == column);
                    if (cell != null)
                    {
                        cell.AdjacentMines = _currentGame[row, column] >= 0 ? _currentGame[row, column] : 0;
                        cell.IsRevealed = _currentGame.IsRevealed(row, column);
                        cell.IsFlagged = _currentGame[row, column] == -3;
                        cell.HasMine = _currentGame.HasMine(row, column);
                    }
                }
            }

            switch (_currentGame.GameState)
            {
                case GameState.Won:
                    GameStatus = "Поздравляем! Вы выиграли!";
                    StatusColor = Brushes.Green;
                    IsGameOver = true;
                    break;
                case GameState.GameOver:
                    GameStatus = "Игра окончена! Вы проиграли!";
                    StatusColor = Brushes.Red;
                    IsGameOver = true;
                    break;
                default:
                    GameStatus = $"Ходов: {MoveCount}, Флагов: {FlagsCount}";
                    StatusColor = Brushes.Black;
                    break;
            }
        }

        private void ApplySettings()
        {
            if (MinesCount < 5)
                MinesCount = 5;
            if (MinesCount > 50)
                MinesCount = 50;

            Console.WriteLine($"Применены настройки: {MinesCount} мин");
            StartNewGame();
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}