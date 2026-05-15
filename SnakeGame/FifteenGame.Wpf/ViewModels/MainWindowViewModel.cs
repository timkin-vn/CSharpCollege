using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private SnakeService _service = new SnakeService();
        private SnakeGameModel _model = new SnakeGameModel();

        public ObservableCollection<SnakeCellViewModel> Cells { get; } = new ObservableCollection<SnakeCellViewModel>();

        private int _score;
        public int Score
        {
            get => _score;
            set { _score = value; OnPropertyChanged(nameof(Score)); }
        }

        private bool _isGameOver;
        public bool IsGameOver
        {
            get => _isGameOver;
            set { _isGameOver = value; OnPropertyChanged(nameof(IsGameOver)); }
        }

        private string _gameStatus;
        public string GameStatus
        {
            get => _gameStatus;
            set { _gameStatus = value; OnPropertyChanged(nameof(GameStatus)); }
        }

        public ICommand NewGameCommand { get; }

        private System.Timers.Timer _gameTimer;

        public MainWindowViewModel()
        {
            NewGameCommand = new RelayCommand(_ => NewGame());
            InitializeGame();
        }

        public void InitializeGame()
        {
            _service.GenerateRandomFood(_model);
            LoadViewModel();
            StartGameLoop();
        }

        public void NewGame()
        {
            _model = new SnakeGameModel();
            _service.GenerateRandomFood(_model);
            IsGameOver = false;
            Score = 0;
            GameStatus = "";
            LoadViewModel();
            StartGameLoop();
        }

        public void StartGameLoop()
        {
            _gameTimer?.Stop();
            _gameTimer = new System.Timers.Timer(150);
            _gameTimer.Elapsed += (s, e) =>
            {
                if (!IsGameOver)
                {
                    App.Current.Dispatcher.Invoke(() => MakeMove());
                }
            };
            _gameTimer.Start();
        }

        public void ChangeDirection(Direction newDirection)
        {
            if (!IsGameOver)
            {
                _service.Move(_model, newDirection);
                LoadViewModel();

                if (_model.IsGameOver)
                {
                    IsGameOver = true;
                    GameStatus = $"Игра окончена! Ваш счёт: {_model.Score}";
                    _gameTimer?.Stop();
                }
                else
                {
                    Score = _model.Score;
                }
            }
        }

        private void MakeMove()
        {
            if (!IsGameOver)
            {
                _service.Move(_model);
                LoadViewModel();

                if (_model.IsGameOver)
                {
                    IsGameOver = true;
                    GameStatus = $"Игра окончена! Ваш счёт: {_model.Score}";
                    _gameTimer?.Stop();
                }
                else
                {
                    Score = _model.Score;
                }
            }
        }

        private void LoadViewModel()
        {
            Cells.Clear();

            for (int row = 0; row < SnakeGameModel.Height; row++)
            {
                for (int col = 0; col < SnakeGameModel.Width; col++)
                {
                    var point = new SnakePoint(col, row);
                    var type = CellType.Empty;

                    foreach (var snakePart in _model.Snake)
                    {
                        if (snakePart.X == point.X && snakePart.Y == point.Y)
                        {
                            type = CellType.Snake;
                            break;
                        }
                    }

                    if (_model.Food.X == point.X && _model.Food.Y == point.Y)
                    {
                        type = CellType.Food;
                    }

                    bool isHead = false;
                    if (_model.Snake.Count > 0 && _model.Snake[0].X == point.X && _model.Snake[0].Y == point.Y)
                    {
                        isHead = true;
                    }

                    string text = "";
                    if (type == CellType.Food)
                        text = "🍎";
                    else if (type == CellType.Snake && isHead)
                        text = "🐍";
                    else if (type == CellType.Snake)
                        text = "■";
                    else
                        text = "";

                    Cells.Add(new SnakeCellViewModel
                    {
                        Position = point,
                        Type = type,
                        IsHead = isHead,
                        Text = text
                    });
                }
            }
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}