using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly GameService _gameEngine = new GameService();
        private readonly GameModel _gameData = new GameModel();

        public ObservableCollection<CellViewModel> GridItems { get; } = new ObservableCollection<CellViewModel>();

        private int _currentScore;
        public int Score
        {
            get => _currentScore;
            set
            {
                if (_currentScore == value) return;
                _currentScore = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        private string _matchState;
        public string GameStatus
        {
            get => _matchState;
            set
            {
                if (_matchState == value) return;
                _matchState = value;
                OnPropertyChanged(nameof(GameStatus));
            }
        }

        public MainWindowViewModel()
        {
            RestartSession();
        }

        public void RestartSession()
        {
            _gameEngine.ResetState(_gameData);
            Score = _gameData.Score;
            GameStatus = string.Empty;
            SyncGridData();
        }

        public void HandleDirection(MoveDirection dir)
        {
            if (_gameData.IsWin)
            {
                GameStatus = "Win";
                return;
            }

            bool isExecuted = _gameEngine.MakeMove(_gameData, dir);
            if (!isExecuted) return;

            Score = _gameData.Score;
            SyncGridData();

            for (int r = 0; r < GameModel.Size; r++)
            {
                for (int c = 0; c < GameModel.Size; c++)
                {
                    if (_gameData[r, c] == 2048 && !_gameData.IsWin)
                    {
                        _gameData.IsWin = true;
                        GameStatus = "Win";
                        MessageBox.Show("Цель достигнута!", "Победа", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
            }

            if (_gameEngine.CheckIfLost(_gameData))
            {
                GameStatus = "Lose";
                MessageBox.Show("Ходов больше нет.", "Конец игры", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void SyncGridData()
        {
            GridItems.Clear();
            for (int r = 0; r < GameModel.Size; r++)
            {
                for (int c = 0; c < GameModel.Size; c++)
                {
                    GridItems.Add(new CellViewModel
                    {
                        Row = r,
                        Column = c,
                        Value = _gameData[r, c]
                    });
                }
            }
        }
    }
}