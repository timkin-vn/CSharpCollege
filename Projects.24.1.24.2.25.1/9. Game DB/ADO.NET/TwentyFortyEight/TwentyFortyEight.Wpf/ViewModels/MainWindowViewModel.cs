using System;
using System.Collections.ObjectModel;
using Ninject;
using TwentyFortyEight.Common.BusinessModels;
using TwentyFortyEight.Common.Contracts.Services;
using TwentyFortyEight.Common.Definitions;
using TwentyFortyEight.Common.Infrastructure;

namespace TwentyFortyEight.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IGameService _gameService = NinjectKernel.Instance.Get<IGameService>();
        private GameModel _activeGame;
        private UserModel _currentPlayer;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public string UserName => _currentPlayer?.Name ?? "Гость";
        public int Score => _activeGame?.Score ?? 0;
        public int BestTile => _activeGame?.BestTile ?? 0;
        public bool IsGameActive => _currentPlayer != null;

        public MainWindowViewModel()
        {
        }

        public void CommitUser(UserModel user)
        {
            _currentPlayer = user;
            _activeGame = _gameService.GetByUserId(user.Id);

            UpdateGameBoard();

            OnPropertyChanged(nameof(IsGameActive));
            OnPropertyChanged(nameof(UserName));
        }

        public void MakeMove(MoveDirection direction, Action gameFinishedAction, Action gameWonAction)
        {
            if (_activeGame == null) return;

            _activeGame = _gameService.MakeMove(_activeGame.Id, direction);
            UpdateGameBoard();

            if (_gameService.IsGameWon(_activeGame.Id) && _activeGame.IsWon)
            {
                _activeGame.IsWon = false;
                gameWonAction?.Invoke();
            }
            else if (_gameService.IsGameOver(_activeGame.Id))
            {
                gameFinishedAction?.Invoke();
            }
        }

        public void NewGame()
        {
            if (_currentPlayer == null) return;

            _gameService.ResetGame(_currentPlayer.Id);
            _activeGame = _gameService.GetByUserId(_currentPlayer.Id);
            UpdateGameBoard();
        }

        private void UpdateGameBoard()
        {
            Cells.Clear();

            int maxRows = Constants.RowCount;
            int maxCols = Constants.ColumnCount;

            for (int r = 0; r < maxRows; r++)
            {
                for (int c = 0; c < maxCols; c++)
                {
                    var cell = new CellViewModel
                    {
                        Row = r,
                        Column = c,
                        Value = _activeGame[r, c]
                    };
                    Cells.Add(cell);
                }
            }

            OnPropertyChanged(nameof(BestTile));
            OnPropertyChanged(nameof(Score));
        }
    }
}