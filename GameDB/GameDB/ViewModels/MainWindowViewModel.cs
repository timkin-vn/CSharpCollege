using GameDB.Business.Models;
using GameDB.Business.Services;
using GameDB.Common.Models;
using GameDB.DataAccess;
using System.Collections.ObjectModel;
using System.Windows;
using GameDB.ViewModels;

namespace GameDB.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly GameService _gameService = new();
        private readonly GameModel _model = new();
        private UnitOfWork? _unitOfWork;
        private int _currentPlayerId;
        private string _currentUsername = string.Empty;
        private bool _isDbConnected;
        private bool _isGameInitialized;

        public ObservableCollection<CellViewModel> Cells { get; } = new();
        public int Score { get; private set; }

        private const string ConnectionString = "Host=localhost;Port=5432;Database=2048_game_db;Username=postgres;Password=postgres";

        public MainWindowViewModel()
        {
            InitializeDatabase();
        }

        public void Initialize()
        {
            InitializeGame();
        }

        private async void InitializeDatabase()
        {
            try
            {
                _unitOfWork = new UnitOfWork(ConnectionString);
                var initializer = new DatabaseInitializer(ConnectionString);
                await initializer.EnsureDatabaseCreatedAsync();

                _isDbConnected = true;
                
                // Показываем окно логина
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ShowLoginWindow();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к БД: {ex.Message}\n\nИгра будет запущена в офлайн-режиме.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                _isDbConnected = false;
                InitializeGame();
            }
        }

        private void ShowLoginWindow()
        {
            if (_unitOfWork == null)
            {
                InitializeGame();
                return;
            }

            var loginWindow = new LoginWindow(_unitOfWork);
            bool? result = loginWindow.ShowDialog();
            
            if (result == true)
            {
                _currentPlayerId = loginWindow.PlayerId;
                _currentUsername = loginWindow.Username;
                LoadSavedGame();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        private async void LoadSavedGame()
        {
            try
            {
                if (_unitOfWork == null || _currentPlayerId == 0)
                {
                    InitializeGame();
                    return;
                }

                var session = await _unitOfWork.Sessions.GetByPlayerIdAsync(_currentPlayerId);
                if (session != null && session.GameField != null && session.GameField.Length > 0)
                {
                    for (int row = 0; row < GameModel.RowCount; row++)
                    {
                        for (int col = 0; col < GameModel.ColumnCount; col++)
                        {
                            if (row < session.GameField.Length && col < session.GameField[row].Length)
                            {
                                _model[row, col] = session.GameField[row][col];
                            }
                        }
                    }
                    Score = session.CurrentScore;
                    OnPropertyChanged(nameof(Score));
                    LoadViewModel(_model);
                    _isGameInitialized = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            InitializeGame();
        }

        private void InitializeGame()
        {
            _gameService.Initialize(_model);
            Score = _model.Score;
            _isGameInitialized = true;
            OnPropertyChanged(nameof(Score));
            LoadViewModel(_model);
        }

        public void MakeMove(MoveDirection direction, Action gameOverAction, Action winAction)
        {
            // Проверяем, что игра инициализирована
            if (!_isGameInitialized)
            {
                InitializeGame();
                return;
            }

            if (!_gameService.MakeMove(_model, direction))
            {
                if (_gameService.IsGameOver(_model))
                {
                    _ = SaveGameResultAsync();
                    gameOverAction?.Invoke();
                }
                return;
            }

            Score = _model.Score;
            OnPropertyChanged(nameof(Score));
            LoadViewModel(_model);

            if (_isDbConnected && _currentPlayerId > 0 && _unitOfWork != null)
            {
                _ = SaveCurrentSessionAsync();
            }

            if (_model.HasWon)
            {
                winAction?.Invoke();
            }
            else if (_gameService.IsGameOver(_model))
            {
                _ = SaveGameResultAsync();
                gameOverAction?.Invoke();
            }
        }

        private async Task SaveCurrentSessionAsync()
        {
            if (_unitOfWork == null || _currentPlayerId == 0) return;

            try
            {
                var field = new int[4][];
                for (int row = 0; row < 4; row++)
                {
                    field[row] = new int[4];
                    for (int col = 0; col < 4; col++)
                    {
                        field[row][col] = _model[row, col];
                    }
                }

                await _unitOfWork.Sessions.SaveAsync(_currentPlayerId, Score, field);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка сохранения сессии: {ex.Message}");
            }
        }

        private async Task SaveGameResultAsync()
        {
            if (_unitOfWork == null || _currentPlayerId == 0) return;

            try
            {
                int maxTile = 0;
                for (int row = 0; row < 4; row++)
                {
                    for (int col = 0; col < 4; col++)
                    {
                        if (_model[row, col] > maxTile)
                            maxTile = _model[row, col];
                    }
                }

                await _unitOfWork.Leaderboards.UpdateAsync(_currentPlayerId, Score, maxTile);
                await _unitOfWork.Sessions.DeleteAsync(_currentPlayerId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка сохранения результата: {ex.Message}");
            }
        }

        public void ContinueAfterWin()
        {
            _gameService.ContinueAfterWin(_model);
        }

        public void RestartGame()
        {
            InitializeGame();
            if (_isDbConnected && _currentPlayerId > 0 && _unitOfWork != null)
            {
                _ = SaveCurrentSessionAsync();
            }
        }

        public async Task<IEnumerable<LeaderboardEntry>> GetLeaderboardAsync()
        {
            if (_unitOfWork == null || !_isDbConnected)
                return Enumerable.Empty<LeaderboardEntry>();

            try
            {
                return await _unitOfWork.Leaderboards.GetTopAsync(10);
            }
            catch
            {
                return Enumerable.Empty<LeaderboardEntry>();
            }
        }

        private void LoadViewModel(GameModel model)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Cells.Clear();
                for (int row = 0; row < GameModel.RowCount; row++)
                {
                    for (int column = 0; column < GameModel.ColumnCount; column++)
                    {
                        Cells.Add(CellViewModel.Create(row, column, model[row, column]));
                    }
                }
            });
        }
    }
}