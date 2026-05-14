using Minesweeper.BusinessProxy.Services;
using Minesweeper.Common.Dto;
using Minesweeper.Common.Request;
using Minesweeper.WpfClient.Helpers;
using Minesweeper.WpfClient.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Minesweeper.WpfClient.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private readonly IGameServiceProxy _gameService;
        private readonly IUserServiceProxy _userService;
        private UserResponse _currentUser;
        private GameResponse _currentGame;
        private bool _isLoading;
        private string _statusMessage;
        private GameStats _gameStats;

        public GameViewModel(IGameServiceProxy gameService, IUserServiceProxy userService)
        {
            _gameService = gameService;
            _userService = userService;
            Cells = new ObservableCollection<CellViewModel>();

            NewGameCommand = new RelayCommand(async _ => await StartNewGame(), _ => !IsLoading);
            ExitCommand = new RelayCommand(_ => RequestExit?.Invoke(this, EventArgs.Empty));
        }

        public event EventHandler RequestExit;

        public UserResponse CurrentUser
        {
            get => _currentUser;
            set
            {
                if (SetProperty(ref _currentUser, value))
                {
                    OnPropertyChanged(nameof(WelcomeMessage));
                }
            }
        }

        public GameResponse CurrentGame
        {
            get => _currentGame;
            set => SetProperty(ref _currentGame, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (SetProperty(ref _isLoading, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public string GameStats
        {
            get => _gameStats?.ToString() ?? "";
            set
            {
                if (_gameStats == null) _gameStats = new GameStats();
                OnPropertyChanged();
            }
        }

        public string WelcomeMessage => CurrentUser != null
            ? $"Игрок: {CurrentUser.Username} | Побед: {CurrentUser.GamesWon} | Win Rate: {CurrentUser.WinRate:F1}%"
            : "";

        public ObservableCollection<CellViewModel> Cells { get; }

        public ICommand NewGameCommand { get; }
        public ICommand ExitCommand { get; }

        public async Task Initialize(UserResponse user)
        {
            CurrentUser = user;

            if (user.Id > 0)
            {
                try
                {
                    var activeGame = await _gameService.GetLastActiveGame(user.Id);
                    if (activeGame != null && !activeGame.IsGameOver && !activeGame.IsGameWon)
                    {
                        await LoadGame(activeGame.Id);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось загрузить активную игру: {ex.Message}", "Внимание",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            await StartNewGame();
        }

        public async Task StartNewGame(int size = 10, int mines = 5)
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Создание новой игры...";

                if (CurrentUser.Id > 0)
                {
                    CurrentGame = await _gameService.CreateGame(CurrentUser.Id, size, mines);
                }
                else
                {
                    var guest = await _userService.GetOrCreateUser($"Guest_{DateTime.Now.Ticks}");
                    CurrentUser = guest;
                    CurrentGame = await _gameService.CreateGame(guest.Id, size, mines);
                }

                BuildGrid();
                UpdateStats();
                StatusMessage = "Игра началась!";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания игры: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                StatusMessage = "Ошибка создания игры";
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task LoadGame(int gameId)
        {
            try
            {
                IsLoading = true;
                CurrentGame = await _gameService.GetGame(gameId);
                BuildGrid();
                UpdateStats();

                if (CurrentGame.IsGameOver)
                    StatusMessage = "Игра окончена - ПОРАЖЕНИЕ";
                else if (CurrentGame.IsGameWon)
                    StatusMessage = "Игра окончена - ПОБЕДА!";
                else
                    StatusMessage = "Игра загружена";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки игры: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void BuildGrid()
        {
            Cells.Clear();

            if (CurrentGame?.VisibleField == null) return;

            for (int row = 0; row < CurrentGame.FieldSize; row++)
            {
                for (int col = 0; col < CurrentGame.FieldSize; col++)
                {
                    var cellValue = CurrentGame.VisibleField[row][col];
                    var cell = new CellViewModel(new CellPosition(row, col), cellValue);
                    cell.CellClicked += OnCellClicked;
                    cell.CellRightClicked += OnCellRightClicked;
                    Cells.Add(cell);
                }
            }
        }

        private async void OnCellClicked(object sender, CellClickEventArgs e)
        {
            if (CurrentGame == null || CurrentGame.IsGameOver || CurrentGame.IsGameWon || IsLoading)
                return;

            var cell = Cells.FirstOrDefault(c => c.Position.Row == e.Position.Row && c.Position.Column == e.Position.Column);
            if (cell == null || cell.IsFlagged || cell.IsRevealed)
                return;

            await MakeMove(e.Position.Row, e.Position.Column, "open");
        }

        private async void OnCellRightClicked(object sender, CellClickEventArgs e)
        {
            if (CurrentGame == null || CurrentGame.IsGameOver || CurrentGame.IsGameWon || IsLoading)
                return;

            var cell = Cells.FirstOrDefault(c => c.Position.Row == e.Position.Row && c.Position.Column == e.Position.Column);
            if (cell == null || cell.IsRevealed)
                return;

            await MakeMove(e.Position.Row, e.Position.Column, "toggle_flag");
        }

        private async Task MakeMove(int row, int col, string action)
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Выполнение хода...";

                if (action == "open")
                {
                    CurrentGame = await _gameService.OpenCell(CurrentGame.Id, row, col);
                }
                else if (action == "toggle_flag")
                {
                    CurrentGame = await _gameService.ToggleFlag(CurrentGame.Id, row, col);
                }

                UpdateGridFromResponse();
                UpdateStats();

                if (CurrentGame.IsGameOver)
                {
                    StatusMessage = "Игра окончена - ПОРАЖЕНИЕ!";
                    MessageBox.Show("Вы попали на мину!\nИгра окончена.", "Поражение",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (CurrentGame.IsGameWon)
                {
                    StatusMessage = "Игра окончена - ПОБЕДА!";
                    MessageBox.Show("Поздравляем! Вы выиграли!", "Победа",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    StatusMessage = "Ход выполнен";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении хода: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                StatusMessage = "Ошибка выполнения хода";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void UpdateGridFromResponse()
        {
            if (CurrentGame?.VisibleField == null) return;

            for (int row = 0; row < CurrentGame.FieldSize; row++)
            {
                for (int col = 0; col < CurrentGame.FieldSize; col++)
                {
                    var cell = Cells.FirstOrDefault(c => c.Position.Row == row && c.Position.Column == col);
                    if (cell != null)
                    {
                        cell.UpdateState(CurrentGame.VisibleField[row][col]);
                    }
                }
            }
        }

        private void UpdateStats()
        {
            if (CurrentGame == null) return;

            _gameStats = new GameStats
            {
                FieldSize = CurrentGame.FieldSize,
                MineCount = CurrentGame.MineCount,
                FlagsPlaced = CurrentGame.FlagsPlaced,
                CellsRevealed = CurrentGame.CellsRevealed,
                CellsRemaining = CurrentGame.CellsRemaining
            };

            OnPropertyChanged(nameof(GameStats));
        }
    }

    public class CellViewModel : BaseViewModel
    {
        private CellViewModelState _state;

        public event EventHandler<CellClickEventArgs> CellClicked;
        public event EventHandler<CellClickEventArgs> CellRightClicked;

        public CellViewModel(CellPosition position, int value)
        {
            Position = position;
            _state = new CellViewModelState();
            UpdateState(value);

            ClickCommand = new RelayCommand(_ => OnCellClick());
            RightClickCommand = new RelayCommand(_ => OnCellRightClick());
        }

        public CellPosition Position { get; }

        public int Value
        {
            get => _state.Value;
            private set
            {
                _state.Value = value;
                OnPropertyChanged();
            }
        }

        public bool IsRevealed
        {
            get => _state.IsRevealed;
            private set
            {
                _state.IsRevealed = value;
                OnPropertyChanged();
            }
        }

        public bool IsMine
        {
            get => _state.IsMine;
            private set
            {
                _state.IsMine = value;
                OnPropertyChanged();
            }
        }

        public bool IsFlagged
        {
            get => _state.IsFlagged;
            private set
            {
                _state.IsFlagged = value;
                OnPropertyChanged();
            }
        }

        public string DisplayText
        {
            get => _state.DisplayText;
            private set
            {
                _state.DisplayText = value;
                OnPropertyChanged();
            }
        }

        public Brush BackgroundColor
        {
            get => HexToBrush(_state.BackgroundColorHex);
            private set
            {
                _state.BackgroundColorHex = BrushToHex(value);
                OnPropertyChanged();
            }
        }

        public Brush ForegroundColor
        {
            get => HexToBrush(_state.ForegroundColorHex);
            private set
            {
                _state.ForegroundColorHex = BrushToHex(value);
                OnPropertyChanged();
            }
        }

        public ICommand ClickCommand { get; }
        public ICommand RightClickCommand { get; }

        public void UpdateState(int newValue)
        {
            Value = newValue;

            switch (newValue)
            {
                case -2:
                    IsFlagged = true;
                    IsRevealed = false;
                    IsMine = false;
                    DisplayText = "🚩";
                    BackgroundColor = Brushes.LightYellow;
                    ForegroundColor = Brushes.Black;
                    break;

                case -1:
                    IsFlagged = false;
                    IsRevealed = false;
                    IsMine = false;
                    DisplayText = "";
                    BackgroundColor = Brushes.LightBlue;
                    ForegroundColor = Brushes.Black;
                    break;

                case 9:
                    IsFlagged = false;
                    IsRevealed = true;
                    IsMine = true;
                    DisplayText = "💣";
                    BackgroundColor = Brushes.Red;
                    ForegroundColor = Brushes.Black;
                    break;

                case 0:
                    IsFlagged = false;
                    IsRevealed = true;
                    IsMine = false;
                    DisplayText = "";
                    BackgroundColor = Brushes.White;
                    ForegroundColor = Brushes.Black;
                    break;

                default:
                    IsFlagged = false;
                    IsRevealed = true;
                    IsMine = false;
                    DisplayText = newValue.ToString();
                    BackgroundColor = Brushes.White;
                    ForegroundColor = GetNumberColor(newValue);
                    break;
            }
        }

        private void OnCellClick()
        {
            CellClicked?.Invoke(this, new CellClickEventArgs(Position));
        }

        private void OnCellRightClick()
        {
            CellRightClicked?.Invoke(this, new CellClickEventArgs(Position));
        }

        private static Brush GetNumberColor(int number)
        {
            switch (number)
            {
                case 1: return Brushes.Blue;
                case 2: return Brushes.Green;
                case 3: return Brushes.Red;
                case 4: return Brushes.DarkBlue;
                case 5: return Brushes.DarkRed;
                case 6: return Brushes.Teal;
                case 7: return Brushes.Black;
                case 8: return Brushes.Gray;
                default: return Brushes.Black;
            }
        }
        private static string BrushToHex(Brush brush)
        {
            if (brush is SolidColorBrush solidBrush)
            {
                var color = solidBrush.Color;
                return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
            }
            return "#FFFFFFFF";
        }

        private static Brush HexToBrush(string hex)
        {
            if (string.IsNullOrEmpty(hex))
                return Brushes.Transparent;

            try
            {
                var converter = new System.Windows.Media.BrushConverter();
                return (Brush)converter.ConvertFromString(hex);
            }
            catch
            {
                return Brushes.Transparent;
            }
        }
    }
}