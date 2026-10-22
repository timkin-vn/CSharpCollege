using System;
using System.Collections.Generic;
using System.Linq;
using TwentyFortyEight.Common.BusinessModels;
using TwentyFortyEight.Common.Contracts.Repositories;
using TwentyFortyEight.Common.Contracts.Services;
using TwentyFortyEight.Common.Definitions;
using TwentyFortyEight.Common.Dtos;

namespace TwentyFortyEight.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _dbContext;
        private readonly Random _random = new Random();

        private readonly Dictionary<int, GameModel> _cachedGames = new Dictionary<int, GameModel>();

        public GameService(IGameRepository repository)
        {
            _dbContext = repository;
        }

        public GameModel GetByGameId(int gameId)
        {
            return _cachedGames.TryGetValue(gameId, out var game) ? game : null;
        }

        public GameModel GetByUserId(int userId)
        {
            if (_cachedGames.TryGetValue(userId, out var cached))
            {
                return cached;
            }

            // Явно указываем тип GameDto
            GameDto latestGameDto = null;
            try
            {
                var dbGames = _dbContext.GetByUserId(userId);
                latestGameDto = dbGames?.LastOrDefault();
            }
            catch
            {
                // Игнорируем ошибку, если база недоступна
            }

            if (latestGameDto != null)
            {
                var restoredModel = new GameModel
                {
                    Id = latestGameDto.Id,
                    UserId = latestGameDto.UserId,
                    Score = latestGameDto.Score,
                    BestTile = latestGameDto.BestTile,
                    IsWon = latestGameDto.IsWon
                };

                // Копируем ячейки из DTO в модель
                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int col = 0; col < Constants.ColumnCount; col++)
                    {
                        restoredModel[row, col] = latestGameDto.Cells[row, col];
                    }
                }

                _cachedGames[userId] = restoredModel;
                return restoredModel;
            }

            return InitializeNewGame(userId);
        }

        public void ResetGame(int userId)
        {
            if (_cachedGames.TryGetValue(userId, out var existing))
            {
                try
                {
                    _dbContext.Remove(existing.Id);
                }
                catch
                {
                    // Игнорируем ошибку удаления из БД
                }
                _cachedGames.Remove(userId);
            }

            InitializeNewGame(userId);
        }

        private GameModel InitializeNewGame(int userId)
        {
            var newGame = new GameModel
            {
                Id = 1, // Фиктивный ID для работы в памяти
                UserId = userId,
                Score = 0,
                BestTile = 2,
                IsWon = false
            };

            PrepareEmptyBoard(newGame);
            SpawnRandomTile(newGame);
            SpawnRandomTile(newGame);

            try
            {
                newGame.Id = _dbContext.Save(MapToDto(newGame));
            }
            catch
            {
                // Если база не работает, игра продолжит работать в памяти
            }

            _cachedGames[userId] = newGame;

            return newGame;
        }

        private void PrepareEmptyBoard(GameModel model)
        {
            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    model[r, c] = 0;
                }
            }
        }

        private void SpawnRandomTile(GameModel model)
        {
            var emptySpots = new List<(int r, int c)>();

            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    if (model[r, c] == 0) emptySpots.Add((r, c));
                }
            }

            if (!emptySpots.Any()) return;

            var targetSpot = emptySpots[_random.Next(emptySpots.Count)];
            model[targetSpot.r, targetSpot.c] = _random.Next(10) == 0 ? 4 : 2;
        }

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            var currentGame = GetByGameId(gameId);

            if (currentGame == null || currentGame.IsWon || IsGameOver(gameId))
            {
                return currentGame;
            }

            bool isChanged = false;

            for (int lineIndex = 0; lineIndex < 4; lineIndex++)
            {
                var extractedLine = new int[4];

                for (int i = 0; i < 4; i++)
                {
                    extractedLine[i] = GetTileValue(currentGame, direction, lineIndex, i);
                }

                var mergedLine = MergeLine(extractedLine, currentGame);

                for (int i = 0; i < 4; i++)
                {
                    if (GetTileValue(currentGame, direction, lineIndex, i) != mergedLine[i])
                    {
                        isChanged = true;
                    }

                    SetTileValue(currentGame, direction, lineIndex, i, mergedLine[i]);
                }
            }

            if (isChanged)
            {
                SpawnRandomTile(currentGame);

                for (int r = 0; r < Constants.RowCount; r++)
                {
                    for (int c = 0; c < Constants.ColumnCount; c++)
                    {
                        if (currentGame[r, c] >= Constants.WinTile)
                        {
                            currentGame.IsWon = true;
                        }
                    }
                }

                try
                {
                    _dbContext.Save(MapToDto(currentGame));
                }
                catch
                {
                    // Игнорируем ошибку сохранения, чтобы игра не прерывалась
                }
            }

            return currentGame;
        }

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

        public bool IsGameOver(int gameId)
        {
            var game = GetByGameId(gameId);
            if (game == null) return true;

            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 4; c++)
                    if (game[r, c] == 0) return false;

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    if (c + 1 < 4 && game[r, c] == game[r, c + 1]) return false;
                    if (r + 1 < 4 && game[r, c] == game[r + 1, c]) return false;
                }
            }

            return true;
        }

        public bool IsGameWon(int gameId)
        {
            var game = GetByGameId(gameId);
            return game?.IsWon ?? false;
        }

        public void RemoveGame(int gameId)
        {
            _cachedGames.Remove(gameId);
        }

        private GameDto MapToDto(GameModel model)
        {
            var dto = new GameDto
            {
                Id = model.Id,
                UserId = model.UserId,
                Score = model.Score,
                BestTile = model.BestTile,
                IsWon = model.IsWon
            };

            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    dto.Cells[r, c] = model[r, c];
                }
            }

            return dto;
        }
    }
}