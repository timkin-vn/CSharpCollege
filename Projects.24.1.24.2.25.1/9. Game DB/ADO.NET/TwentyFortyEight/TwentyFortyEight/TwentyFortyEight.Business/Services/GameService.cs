using System;
using System.Collections.Generic;
using System.Linq;
using TwentyFortyEight.Common.BusinessModels;
using TwentyFortyEight.Common.Contracts.Repositories;
using TwentyFortyEight.Common.Contracts.Services;
using TwentyFortyEight.Common.Definitions;

namespace TwentyFortyEight.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;
        private readonly Random _random = new Random();

        private readonly Dictionary<int, GameModel> _games =
            new Dictionary<int, GameModel>();

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public GameModel GetByGameId(int gameId)
        {
            return _games.ContainsKey(gameId) ? _games[gameId] : null;
        }

        public GameModel GetByUserId(int userId)
        {
            var existing = _games.Values
                .FirstOrDefault(g => g.UserId == userId);

            if (existing != null)
                return existing;

            var game = new GameModel
            {
                Id = userId,
                UserId = userId,
                Score = 0,
                BestTile = 2,
                IsWon = false
            };

            Initialize(game);

            _games[userId] = game;

            return game;
        }

        public void ResetGame(int userId)
        {
            // Remove old game for this user
            var old = _games.Values.FirstOrDefault(g => g.UserId == userId);
            if (old != null)
                _games.Remove(old.Id);

            // Create fresh game
            var game = new GameModel
            {
                Id = userId,
                UserId = userId,
                Score = 0,
                BestTile = 2,
                IsWon = false
            };

            Initialize(game);

            _games[userId] = game;
        }

        private void Initialize(GameModel model)
        {
            for (int row = 0; row < Constants.RowCount; row++)
                for (int column = 0; column < Constants.ColumnCount; column++)
                    model[row, column] = 0;

            AddRandomTile(model);
            AddRandomTile(model);
        }

        private void AddRandomTile(GameModel model)
        {
            var emptyCells = new List<(int Row, int Column)>();

            for (int row = 0; row < Constants.RowCount; row++)
                for (int column = 0; column < Constants.ColumnCount; column++)
                    if (model[row, column] == 0)
                        emptyCells.Add((row, column));

            if (!emptyCells.Any())
                return;

            var selected = emptyCells[_random.Next(emptyCells.Count)];
            // 90% chance of 2, 10% chance of 4
            model[selected.Row, selected.Column] = _random.Next(10) == 0 ? 4 : 2;
        }

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            var model = GetByGameId(gameId);

            if (model == null)
                return null;

            if (model.IsWon || IsGameOver(gameId))
                return model;

            bool moved = false;

            switch (direction)
            {
                case MoveDirection.Left:  moved = MoveLeft(model);  break;
                case MoveDirection.Right: moved = MoveRight(model); break;
                case MoveDirection.Up:    moved = MoveUp(model);    break;
                case MoveDirection.Down:  moved = MoveDown(model);  break;
            }

            if (moved)
            {
                AddRandomTile(model);

                // Check win condition
                for (int r = 0; r < Constants.RowCount; r++)
                    for (int c = 0; c < Constants.ColumnCount; c++)
                        if (model[r, c] >= Constants.WinTile)
                            model.IsWon = true;
            }

            return model;
        }

        private bool MoveLeft(GameModel model)
        {
            bool moved = false;
            for (int row = 0; row < 4; row++)
            {
                int[] line = Enumerable.Range(0, 4).Select(col => model[row, col]).ToArray();
                var merged = Merge(line, model);
                for (int col = 0; col < 4; col++)
                {
                    if (model[row, col] != merged[col]) moved = true;
                    model[row, col] = merged[col];
                }
            }
            return moved;
        }

        private bool MoveRight(GameModel model)
        {
            bool moved = false;
            for (int row = 0; row < 4; row++)
            {
                int[] line = Enumerable.Range(0, 4).Select(col => model[row, 3 - col]).ToArray();
                var merged = Merge(line, model);
                for (int col = 0; col < 4; col++)
                {
                    if (model[row, 3 - col] != merged[col]) moved = true;
                    model[row, 3 - col] = merged[col];
                }
            }
            return moved;
        }

        private bool MoveUp(GameModel model)
        {
            bool moved = false;
            for (int col = 0; col < 4; col++)
            {
                int[] line = Enumerable.Range(0, 4).Select(row => model[row, col]).ToArray();
                var merged = Merge(line, model);
                for (int row = 0; row < 4; row++)
                {
                    if (model[row, col] != merged[row]) moved = true;
                    model[row, col] = merged[row];
                }
            }
            return moved;
        }

        private bool MoveDown(GameModel model)
        {
            bool moved = false;
            for (int col = 0; col < 4; col++)
            {
                int[] line = Enumerable.Range(0, 4).Select(row => model[3 - row, col]).ToArray();
                var merged = Merge(line, model);
                for (int row = 0; row < 4; row++)
                {
                    if (model[3 - row, col] != merged[row]) moved = true;
                    model[3 - row, col] = merged[row];
                }
            }
            return moved;
        }

        private int[] Merge(int[] line, GameModel model)
        {
            var values = line.Where(x => x != 0).ToList();

            for (int i = 0; i < values.Count - 1; i++)
            {
                if (values[i] == values[i + 1])
                {
                    values[i] *= 2;
                    model.Score += values[i];
                    if (values[i] > model.BestTile)
                        model.BestTile = values[i];
                    values.RemoveAt(i + 1);
                }
            }

            while (values.Count < 4)
                values.Add(0);

            return values.ToArray();
        }

        public bool IsGameOver(int gameId)
        {
            var game = GetByGameId(gameId);
            if (game == null) return true;

            // If any empty cell exists → not over
            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                    if (game[row, col] == 0)
                        return false;

            // If any adjacent merge is possible → not over
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (col + 1 < 4 && game[row, col] == game[row, col + 1]) return false;
                    if (row + 1 < 4 && game[row, col] == game[row + 1, col]) return false;
                }
            }

            return true;
        }

        public bool IsGameWon(int gameId)
        {
            var game = GetByGameId(gameId);
            return game != null && game.IsWon;
        }

        public void RemoveGame(int gameId)
        {
            if (_games.ContainsKey(gameId))
                _games.Remove(gameId);
        }
    }
}
