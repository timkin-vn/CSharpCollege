using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public GameModel GetByGameId(int gameId)
        {
            return FromDto(_repository.GetByGameId(gameId));
        }

        public GameModel GetByUserId(int userId)
        {
            var dto = _repository.GetByUserId(userId).LastOrDefault();

            if (dto != null)
            {
                return FromDto(dto);
            }

            var game = new GameModel
            {
                UserId = userId,
                Score = 0,
                BestTile = 2
            };

            Initialize(game);

            var id = _repository.Save(ToDto(game));

            return GetByGameId(id);
        }

        private void Initialize(GameModel model)
        {
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    model[r, c] = 0;
                }
            }

            SpawnTile(model);
            SpawnTile(model);
        }

        private void SpawnTile(GameModel model)
        {
            var free = new List<(int row, int col)>();

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    if (model[r, c] == 0)
                    {
                        free.Add((r, c));
                    }
                }
            }

            if (free.Count == 0)
            {
                return;
            }

            var rnd = new Random();

            var pos = free[rnd.Next(free.Count)];

            model[pos.row, pos.col] =
                rnd.Next(100) < 90 ? 2 : 4;
        }

        public GameModel MoveLeft(int gameId)
        {
            return ExecuteMove(gameId, MoveLeftInternal);
        }

        public GameModel MoveRight(int gameId)
        {
            return ExecuteMove(gameId, MoveRightInternal);
        }

        public GameModel MoveUp(int gameId)
        {
            return ExecuteMove(gameId, MoveUpInternal);
        }

        public GameModel MoveDown(int gameId)
        {
            return ExecuteMove(gameId, MoveDownInternal);
        }

        private GameModel ExecuteMove(
            int gameId,
            Func<GameModel, bool> move)
        {
            var model = GetByGameId(gameId);

            bool changed = move(model);

            if (changed)
            {
                SpawnTile(model);
                _repository.Save(ToDto(model));
            }

            return model;
        }

        private bool MoveLeftInternal(GameModel model)
        {
            bool changed = false;

            for (int row = 0; row < 4; row++)
            {
                var values = new List<int>();

                for (int col = 0; col < 4; col++)
                {
                    if (model[row, col] != 0)
                    {
                        values.Add(model[row, col]);
                    }
                }

                for (int i = 0; i < values.Count - 1; i++)
                {
                    if (values[i] == values[i + 1])
                    {
                        values[i] *= 2;

                        model.Score += values[i];

                        if (values[i] > model.BestTile)
                        {
                            model.BestTile = values[i];
                        }

                        values.RemoveAt(i + 1);
                    }
                }

                while (values.Count < 4)
                {
                    values.Add(0);
                }

                for (int col = 0; col < 4; col++)
                {
                    if (model[row, col] != values[col])
                    {
                        changed = true;
                    }

                    model[row, col] = values[col];
                }
            }

            return changed;
        }

        private bool MoveRightInternal(GameModel model)
        {
            Rotate180(model);
            bool result = MoveLeftInternal(model);
            Rotate180(model);
            return result;
        }

        private bool MoveUpInternal(GameModel model)
        {
            RotateLeft(model);
            bool result = MoveLeftInternal(model);
            RotateRight(model);
            return result;
        }

        private bool MoveDownInternal(GameModel model)
        {
            RotateRight(model);
            bool result = MoveLeftInternal(model);
            RotateLeft(model);
            return result;
        }

        private void RotateRight(GameModel model)
        {
            int[,] temp = new int[4, 4];

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    temp[c, 3 - r] = model[r, c];
                }
            }

            Copy(temp, model);
        }

        private void RotateLeft(GameModel model)
        {
            int[,] temp = new int[4, 4];

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    temp[3 - c, r] = model[r, c];
                }
            }

            Copy(temp, model);
        }

        private void Rotate180(GameModel model)
        {
            RotateRight(model);
            RotateRight(model);
        }

        private void Copy(int[,] src, GameModel model)
        {
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    model[r, c] = src[r, c];
                }
            }
        }

        public bool IsGameOver(int gameId)
        {
            return false;
        }

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        private GameModel FromDto(GameDto dto)
        {
            var model = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                Score = dto.Score,
                BestTile = dto.BestTile
            };

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    model[r, c] = dto.Cells[r, c];
                }
            }

            return model;
        }

        private GameDto ToDto(GameModel model)
        {
            var dto = new GameDto
            {
                Id = model.Id,
                UserId = model.UserId,
                Score = model.Score,
                BestTile = model.BestTile
            };

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    dto.Cells[r, c] = model[r, c];
                }
            }

            return dto;
        }
    }
}