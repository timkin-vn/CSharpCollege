using Game2048.Common.Models;
using Game2048.Common.Services;
using Game2048.DataAccess.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Game2048.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly GameService _gameService;
        private readonly ScoreRepository _scoreRepository;
        private GameModel _currentGame;
        private bool _wasGameOverHandled = false;
        private string _currentPlayerName;

        public ObservableCollection<CellViewModel> Cells { get; set; }
        public ObservableCollection<PlayerScore> HighScores { get; set; }

        public MainWindowViewModel()
        {
            _gameService = new GameService();
            _scoreRepository = new ScoreRepository();
            Cells = new ObservableCollection<CellViewModel>();
            HighScores = new ObservableCollection<PlayerScore>();
            
            InitializeCells();
            LoadHighScores();
        }

        public void StartNewGame()
        {
            _currentGame = _gameService.InitializeGame();
            _wasGameOverHandled = false;
            UpdateUI();
        }

        public void StartNewGameWithPlayer()
        {
            _currentPlayerName = null; // Сбрасываем имя для нового ввода
            StartNewGame();
        }

        private void InitializeCells()
        {
            Cells.Clear();
            for (int i = 0; i < 16; i++)
            {
                Cells.Add(new CellViewModel());
            }
        }

        public void Move(MoveDirection direction)
        {
            if (_currentGame == null || _currentGame.IsGameOver || _currentGame.IsWon) return;

            var newGame = _gameService.Move(_currentGame, direction);
            
            // Check if game actually changed
            bool changed = false;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (_currentGame.Board[row, col] != newGame.Board[row, col])
                    {
                        changed = true;
                        break;
                    }
                }
                if (changed) break;
            }

            if (changed)
            {
                _currentGame = newGame;
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    int index = row * 4 + col;
                    Cells[index].Value = _currentGame.Board[row, col];
                }
            }

            OnPropertyChanged(nameof(Score));
            OnPropertyChanged(nameof(IsGameOver));
            OnPropertyChanged(nameof(IsWon));

            // Handle game over for score saving
            if (_currentGame != null && (_currentGame.IsGameOver || _currentGame.IsWon) && !_wasGameOverHandled)
            {
                HandleGameOver();
            }
        }

        public int Score => _currentGame?.Score ?? 0;
        public bool IsGameOver => _currentGame?.IsGameOver ?? false;
        public bool IsWon => _currentGame?.IsWon ?? false;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void HandleGameOver()
        {
            _wasGameOverHandled = true;
            SaveScore();
        }

        public void SaveScore()
        {
            try
            {
                string playerName = string.IsNullOrEmpty(_currentPlayerName) ? "Player" : _currentPlayerName;
                _scoreRepository.AddScore(playerName, _currentGame.Score);
                LoadHighScores();
            }
            catch (System.Exception ex)
            {
                // Handle database error silently for now
                System.Diagnostics.Debug.WriteLine($"Database error: {ex.Message}");
            }
        }

        public void SetPlayerName(string playerName)
        {
            _currentPlayerName = playerName;
        }

        public void LoadHighScores()
        {
            try
            {
                var scores = _scoreRepository.GetTopScores();
                HighScores.Clear();
                foreach (var score in scores)
                {
                    HighScores.Add(score);
                }
                OnPropertyChanged(nameof(HighScores));
            }
            catch (System.Exception ex)
            {
                // Handle database error silently for now
                System.Diagnostics.Debug.WriteLine($"Database error: {ex.Message}");
            }
        }
    }
}
