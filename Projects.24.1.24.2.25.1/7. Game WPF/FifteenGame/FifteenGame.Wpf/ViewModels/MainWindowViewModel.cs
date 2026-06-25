using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FifteenGame.Business.Models;
using FifteenGame.Business.Services;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private GameManager _gameManager;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        private int _levelNumber;
        public int LevelNumber
        {
            get => _levelNumber;
            set { _levelNumber = value; OnPropertyChanged(nameof(LevelNumber)); }
        }

        private int _keysCount;
        public int KeysCount
        {
            get => _keysCount;
            set { _keysCount = value; OnPropertyChanged(nameof(KeysCount)); }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(nameof(StatusMessage)); }
        }

        public ICommand MoveUpCommand { get; }
        public ICommand MoveDownCommand { get; }
        public ICommand MoveLeftCommand { get; }
        public ICommand MoveRightCommand { get; }
        public ICommand RestartCommand { get; }

        public MainWindowViewModel()
        {
            _gameManager = new GameManager();
            _gameManager.LevelChanged += OnLevelChanged;
            _gameManager.GameFinished += OnGameFinished;

            MoveUpCommand = new RelayCommand(() => MakeMove(-1, 0));
            MoveDownCommand = new RelayCommand(() => MakeMove(1, 0));
            MoveLeftCommand = new RelayCommand(() => MakeMove(0, -1));
            MoveRightCommand = new RelayCommand(() => MakeMove(0, 1));
            RestartCommand = new RelayCommand(() => _gameManager.RestartGame());

            LoadLevel();
        }

        private void OnLevelChanged() => LoadLevel();
        private void OnGameFinished() => StatusMessage = "Поздравляем! Вы прошли игру!";

        private void LoadLevel()
        {
            var level = _gameManager.CurrentLevel;
            var player = _gameManager.Player;

            LevelNumber = _gameManager.CurrentLevelIndex + 1;
            KeysCount = player.Keys;
            StatusMessage = "";

            Cells.Clear();
            for (int r = 0; r < Level.Rows; r++)
                for (int c = 0; c < Level.Columns; c++)
                {
                    var cell = level.Grid[r, c];
                    if (cell == null) continue;

                    var vm = new CellViewModel(r, c, cell.Type);

                    // Игрок
                    if (r == player.Row && c == player.Column)
                    {
                        vm.BackgroundColor = "#FF4444";
                        vm.DisplayText = "😊";
                    }

                    // Переключатель
                    if (cell.Type == CellType.Switch && level.SwitchStates.ContainsKey((r, c)))
                    {
                        bool active = level.SwitchStates[(r, c)];
                        vm.DisplayText = active ? "🔹" : "⚡";
                        vm.BackgroundColor = active ? "#66FF66" : "#FFAA66";
                    }

                    // Дверь
                    if (cell.Type == CellType.Door)
                    {
                        bool isOpen = level.DoorStates.ContainsKey((r, c)) && level.DoorStates[(r, c)];
                        vm.DisplayText = isOpen ? "🚪(открыта)" : "🚪(закрыта)";
                        vm.BackgroundColor = isOpen ? "#66FF66" : "#CC9966";
                    }

                    // Выход
                    if (cell.Type == CellType.Exit)
                    {
                        bool canExit = level.AreAllSwitchesActive();
                        vm.DisplayText = canExit ? "🚪(выход)" : "🚪(закрыт)";
                        vm.BackgroundColor = canExit ? "#66FF66" : "#FF6666";
                    }

                    Cells.Add(vm);
                }
        }

        private void MakeMove(int dRow, int dCol)
        {
            if (_gameManager.IsGameOver) return;

            bool moved = _gameManager.TryMove(dRow, dCol);
            if (moved)
            {
                LoadLevel();
                if (_gameManager.IsLevelComplete)
                {
                    StatusMessage = $"Уровень {LevelNumber} пройден! Переход на следующий...";
                    _gameManager.NextLevel();
                    LoadLevel();
                }
            }
            else
            {
                StatusMessage = "Невозможно двигаться в этом направлении";
            }
        }
    }
}