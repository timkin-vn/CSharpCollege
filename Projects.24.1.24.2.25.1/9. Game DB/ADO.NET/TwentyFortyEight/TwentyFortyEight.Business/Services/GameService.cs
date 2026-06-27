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
        private readonly IGameRepository _gameDataRepository;
        private readonly Random _rng = new Random();
        private readonly Dictionary<int, GameModel> _activeSessions = new Dictionary<int, GameModel>();

        public GameService(IGameRepository repository)
        {
            _gameDataRepository = repository;
        }

        public GameModel GetByGameId(int gameId)
        {
            return _activeSessions.TryGetValue(gameId, out var session) ? session : null;
        }

        public GameModel GetByUserId(int userId)
        {
            if (_activeSessions.TryGetValue(userId, out var session))
            {
                return session;
            }

            var savedGames = _gameDataRepository.GetByUserId(userId);
            var lastGameDto = savedGames?.LastOrDefault();

            if (lastGameDto != null)
            {
                var restoredGame = new GameModel
                {
                    Id = lastGameDto.Id,
                    UserId = lastGameDto.UserId,
                    Score = lastGameDto.Score,
                    BestTile = lastGameDto.BestTile,
                    IsWon = lastGameDto.IsWon
                };

                for (int r = 0; r < Constants.RowCount; r++)
                {
                    for (int c = 0; c < Constants.ColumnCount; c++)
                    {
                        restoredGame[r, c] = lastGameDto.Cells[r, c];
                    }
                }

                _activeSessions[userId] = restoredGame;
                return restoredGame;
            }

            var freshGame = new GameModel
            {
                Id = 0,
                UserId = userId,
                Score = 0,
                BestTile = 2,
                IsWon = false
            };

            SetupInitialGrid(freshGame);

            var dtoToSave = MapToDto(freshGame);
            freshGame.Id = _gameDataRepository.Save(dtoToSave);

            _activeSessions[userId] = freshGame;
            return freshGame;
        }

        public void ResetGame(int userId)
        {
            if (_activeSessions.TryGetValue(userId, out var session))
            {
                _gameDataRepository.Remove(session.Id);
                _activeSessions.Remove(userId);
            }

            var freshGame = new GameModel
            {
                Id = 0,
                UserId = userId,
                Score = 0,
                BestTile = 2,
                IsWon = false
            };

            SetupInitialGrid(freshGame);

            var dtoToSave = MapToDto(freshGame);
            freshGame.Id = _gameDataRepository.Save(dtoToSave);

            _activeSessions[userId] = freshGame;
        }

        private void SetupInitialGrid(GameModel model)
        {
            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    model[r, c] = 0;
                }
            }

            GenerateTile(model);
            GenerateTile(model);
        }

        private void GenerateTile(GameModel model)
        {
            var freeSlots = new List<(int r, int c)>();

            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    if (model[r, c] == 0)
                    {
                        freeSlots.Add((r, c));
                    }
                }
            }

            if (freeSlots.Count == 0)
            {
                return;
            }

            var chosenSlot = freeSlots[_rng.Next(freeSlots.Count)];
            model[chosenSlot.r, chosenSlot.c] = _rng.Next(10) == 0 ? 4 : 2;
        }

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            var game = GetByGameId(gameId);
            if (game == null || game.IsWon || IsGameOver(gameId))
            {
                return game;
            }

            bool hasMatrixChanged = false;

            for (int idx = 0; idx < 4; idx++)
            {
                int[] linearSlice = new int[4];
                for (int step = 0; step < 4; step++)
                {
                    linearSlice[step] = ExtractValue(game, direction, idx, step);
                }

                int[] processedSlice = CompressAndCombine(linearSlice, game);

                for (int step = 0; step < 4; step++)
                {
                    if (ExtractValue(game, direction, idx, step) != processedSlice[step])
                    {
                        hasMatrixChanged = true;
                    }
                    ApplyValue(game, direction, idx, step, processedSlice[step]);
                }
            }

            if (hasMatrixChanged)
            {
                GenerateTile(game);

                for (int r = 0; r < Constants.RowCount; r++)
                {
                    for (int c = 0; c < Constants.ColumnCount; c++)
                    {
                        if (game[r, c] >= Constants.WinTile)
                        {
                            game.IsWon = true;
                        }
                    }
                }

                _gameDataRepository.Save(MapToDto(game));
            }

            return game;
        }

        private int ExtractValue(GameModel model, MoveDirection dir, int line, int index)
        {
            switch (dir)
            {
                case MoveDirection.Left: return model[line, index];
                case MoveDirection.Right: return model[line, 3 - index];
                case MoveDirection.Up: return model[index, line];
                case MoveDirection.Down: return model[3 - index, line];
                default: return 0;
            }
        }

        private void ApplyValue(GameModel model, MoveDirection dir, int line, int index, int value)
        {
            switch (dir)
            {
                case MoveDirection.Left: model[line, index] = value; break;
                case MoveDirection.Right: model[line, 3 - index] = value; break;
                case MoveDirection.Up: model[index, line] = value; break;
                case MoveDirection.Down: model[3 - index, line] = value; break;
            }
        }

        private int[] CompressAndCombine(int[] source, GameModel model)
        {
            var activeValues = new List<int>();
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] != 0)
                {
                    activeValues.Add(source[i]);
                }
            }

            for (int i = 0; i < activeValues.Count - 1; i++)
            {
                if (activeValues[i] == activeValues[i + 1])
                {
                    activeValues[i] *= 2;
                    model.Score += activeValues[i];

                    if (activeValues[i] > model.BestTile)
                    {
                        model.BestTile = activeValues[i];
                    }

                    activeValues.RemoveAt(i + 1);
                }
            }

            while (activeValues.Count < 4)
            {
                activeValues.Add(0);
            }

            return activeValues.ToArray();
        }

        public bool IsGameOver(int gameId)
        {
            var game = GetByGameId(gameId);
            if (game == null)
            {
                return true;
            }

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    if (game[r, c] == 0)
                    {
                        return false;
                    }
                }
            }

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
            return game != null && game.IsWon;
        }

        public void RemoveGame(int gameId)
        {
            if (_activeSessions.ContainsKey(gameId))
            {
                _activeSessions.Remove(gameId);
            }
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