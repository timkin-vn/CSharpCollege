using System;
using System.Collections.ObjectModel;
using System.Linq;
using TwentyFortyEight.Common.BusinessModels;
using TwentyFortyEight.Common.Definitions;

namespace TwentyFortyEight.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly Random _random = new Random();
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
            if (user == null) return;
            _currentPlayer = user;

            // Создаем игру ЛОКАЛЬНО, без базы данных
            NewGame();

            OnPropertyChanged(nameof(IsGameActive));
            OnPropertyChanged(nameof(UserName));
        }

        public void MakeMove(MoveDirection direction, Action gameFinishedAction, Action gameWonAction)
        {
            if (_activeGame == null) return;

            bool isChanged = false;

            // ЛОКАЛЬНАЯ ЛОГИКА СДВИГА
            for (int lineIndex = 0; lineIndex < 4; lineIndex++)
            {
                var extractedLine = new int[4];
                for (int i = 0; i < 4; i++)
                {
                    extractedLine[i] = GetTileValue(_activeGame, direction, lineIndex, i);
                }

                var mergedLine = MergeLine(extractedLine, _activeGame);

                for (int i = 0; i < 4; i++)
                {
                    if (GetTileValue(_activeGame, direction, lineIndex, i) != mergedLine[i])
                    {
                        isChanged = true;
                    }
                    SetTileValue(_activeGame, direction, lineIndex, i, mergedLine[i]);
                }
            }

            if (isChanged)
            {
                SpawnRandomTile(_activeGame);
                UpdateGameBoard();
                OnPropertyChanged(nameof(Score));
                OnPropertyChanged(nameof(BestTile));
            }
        }

        public void NewGame()
        {
            if (_currentPlayer == null) return;

            _activeGame = new GameModel
            {
                Id = 1,
                UserId = _currentPlayer.Id,
                Score = 0,
                BestTile = 2,
                IsWon = false
            };

            // Очищаем поле
            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 4; c++)
                    _activeGame[r, c] = 0;

            SpawnRandomTile(_activeGame);
            SpawnRandomTile(_activeGame);

            UpdateGameBoard();
        }

        private void SpawnRandomTile(GameModel model)
        {
            var emptySpots = new System.Collections.Generic.List<(int r, int c)>();
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    if (model[r, c] == 0) emptySpots.Add((r, c));
                }
            }

            if (!emptySpots.Any()) return;

            var targetSpot = emptySpots[_random.Next(emptySpots.Count)];
            model[targetSpot.r, targetSpot.c] = _random.Next(10) == 0 ? 4 : 2;
        }

        private void UpdateGameBoard()
        {
            Cells.Clear();
            if (_activeGame == null) return;

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = r,
                        Column = c,
                        Value = _activeGame[r, c]
                    });
                }
            }
        }

        // Вспомогательные методы (скопированы из GameService)
        private int GetTileValue(GameModel model, MoveDirection dir, int line, int offset)
        {
            switch (dir)
            {
                case MoveDirection.Left: return model[line, offset];
                case MoveDirection.Right: return model[line, 3 - offset];
                case MoveDirection.Up: return model[offset, line];
                case MoveDirection.Down: return model[3 - offset, line];
                default: return 0;
            }
        }

        private void SetTileValue(GameModel model, MoveDirection dir, int line, int offset, int value)
        {
            switch (dir)
            {
                case MoveDirection.Left: model[line, offset] = value; break;
                case MoveDirection.Right: model[line, 3 - offset] = value; break;
                case MoveDirection.Up: model[offset, line] = value; break;
                case MoveDirection.Down: model[3 - offset, line] = value; break;
            }
        }

        private int[] MergeLine(int[] line, GameModel game)
        {
            var nonZeroTiles = line.Where(x => x != 0).ToList();

            for (int i = 0; i < nonZeroTiles.Count - 1; i++)
            {
                if (nonZeroTiles[i] == nonZeroTiles[i + 1])
                {
                    nonZeroTiles[i] *= 2;
                    game.Score += nonZeroTiles[i];

                    if (nonZeroTiles[i] > game.BestTile)
                    {
                        game.BestTile = nonZeroTiles[i];
                    }
                    nonZeroTiles.RemoveAt(i + 1);
                }
            }

            while (nonZeroTiles.Count < 4)
            {
                nonZeroTiles.Add(0);
            }

            return nonZeroTiles.ToArray();
        }
    }
}