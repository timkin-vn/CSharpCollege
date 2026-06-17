using FifteenGame.Business.Services;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Wpf.Infrastructure;
using FifteenGame.Common.Definitions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly GameService _gameService;
        private readonly UserService _userService;
        private GameModel _currentGame;
        private UserModel _currentUser;

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public ICommand CellClickCommand { get; }
        public ICommand RestartCommand { get; }
        public ICommand ChangeUserCommand { get; }

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

        private string _userInfo = "Пользователь: Гость";
        public string UserInfo
        {
            get => _userInfo;
            set
            {
                _userInfo = value;
                OnPropertyChanged(nameof(UserInfo));
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

        public bool HasSelectedLilyPad => _currentGame?.SelectedLilyPadRow.HasValue ?? false;

        public MainWindowViewModel(GameService gameService, UserService userService)
        {
            _gameService = gameService;
            _userService = userService;

            CellClickCommand = new RelayCommand(OnCellClick);
            RestartCommand = new RelayCommand(OnRestart);
            ChangeUserCommand = new RelayCommand(OnChangeUser);
        }

        public void SetUser(UserModel user)
        {
            _currentUser = user;
            UserInfo = $"User: {user.Name}";
            Initialize();
        }

        private void OnChangeUser(object parameter)
        {
            var loginWindow = new Views.UserLoginWindow();
            var loginViewModel = new UserLoginWindowViewModel(_userService);
            loginWindow.DataContext = loginViewModel;
            loginWindow.Owner = Application.Current.MainWindow;
            loginWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            var result = loginWindow.ShowDialog();

            if (result == true && loginViewModel.CurrentUser != null)
            {
                SetUser(loginViewModel.CurrentUser);
            }
        }

        public void Initialize()
        {
            try
            {
                if (_currentUser == null) return;

                _currentGame = _gameService.GetByUserId(_currentUser.Id);
                UpdateFromModel();
                UpdateGameStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing game: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OnCellClick(object parameter)
        {
            if (parameter is CellViewModel cell && !_currentGame.IsGameOver)
            {
                var cellType = (CellType)_currentGame[cell.Row, cell.Column];

                if (cellType == CellType.Algae)
                {
                    _gameService.RemoveAlgae(_currentGame, cell.Row, cell.Column);
                    _gameService.SaveGame(_currentGame);
                    UpdateFromModel();
                    UpdateGameStatus();
                    OnPropertyChanged(nameof(HasSelectedLilyPad));
                }
                else if (cellType == CellType.LilyPad)
                {
                    _gameService.SelectLilyPad(_currentGame, cell.Row, cell.Column);
                    _gameService.SaveGame(_currentGame);
                    UpdateFromModel();
                    UpdateGameStatus();
                    OnPropertyChanged(nameof(HasSelectedLilyPad));
                }
                else if (HasSelectedLilyPad && cellType == CellType.Water)
                {
                    _gameService.MoveLilyPad(_currentGame, cell.Row, cell.Column);
                    _gameService.SaveGame(_currentGame);
                    UpdateFromModel();
                    UpdateGameStatus();
                    OnPropertyChanged(nameof(HasSelectedLilyPad));
                }
            }
        }

        private void OnRestart(object parameter)
        {
            if (_currentGame != null)
            {
                _gameService.SaveGame(_currentGame);
            }

            var newGame = new GameModel { UserId = _currentUser.Id };
            _gameService.Initialize(newGame);
            _gameService.SaveGame(newGame);

            _currentGame = newGame;
            UpdateFromModel();
            UpdateGameStatus();
        }

        private void UpdateFromModel()
        {
            Cells.Clear();
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        Type = (CellType)_currentGame[row, column]
                    });
                }
            }
        }

        private void UpdateGameStatus()
        {
            IsGameOver = _currentGame.IsGameOver;

            if (_currentGame.IsGameOver)
            {
                if (_currentGame.IsWin)
                {
                    GameStatus = $"🎉 Победа! Лягушка дома! 🎉\nПотрачено ходов: {_currentGame.MoveCount}";
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
                GameStatus = $"Ходов: {_currentGame.MoveCount} - {mode}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}