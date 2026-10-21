
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
        private static readonly Random Random = new Random();
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

            var model = new GameModel { UserId = userId };
            SpawnTile(model);
            SpawnTile(model);

            int id = _repository.Save(ToDto(model));
            return GetByGameId(id);
        }

        public bool? IsGameOver(int gameId)
        {
            var model = GetByGameId(gameId);
            return model == null ? (bool?)null : IsGameOver(model);
        }

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            var model = GetByGameId(gameId);

            if (model == null || IsGameOver(model))
            {
                return model;
            }

            bool changed = Move(model, direction);

            if (changed)
            {
                model.MoveCount++;
                SpawnTile(model);
                model.BestScore = Math.Max(model.BestScore, model.Score);
                _repository.Save(ToDto(model));
            }

            return model;
        }

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        private bool Move(GameModel model, MoveDirection direction)
        {
            bool changed = false;

            for (int line = 0; line < Constants.RowCount; line++)
            {
                var coordinates = new List<(int Row, int Column)>();

                for (int position = 0; position < Constants.ColumnCount; position++)
                {
                    int row = line;
                    int column = position;

                    switch (direction)
                    {
                        case MoveDirection.Left:
                            row = line;
                            column = position;
                            break;
                        case MoveDirection.Right:
                            row = line;
                            column = Constants.ColumnCount - 1 - position;
                            break;
                        case MoveDirection.Up:
                            row = position;
                            column = line;
                            break;
                        case MoveDirection.Down:
                            row = Constants.RowCount - 1 - position;
                            column = line;
                            break;
                        default:
                            return false;
                    }

                    coordinates.Add((row, column));
                }

                var values = coordinates
                    .Select(p => model[p.Row, p.Column])
                    .Where(value => value != 0)
                    .ToList();

                var merged = new List<int>();

                for (int i = 0; i < values.Count; i++)
                {
                    if (i + 1 < values.Count && values[i] == values[i + 1])
                    {
                        int value = values[i] * 2;
                        merged.Add(value);
                        model.Score += value;
                        i++;
                    }
                    else
                    {
                        merged.Add(values[i]);
                    }
                }

                while (merged.Count < Constants.ColumnCount)
                {
                    merged.Add(0);
                }

                for (int position = 0; position < coordinates.Count; position++)
                {
                    var p = coordinates[position];
                    int value = merged[position];

                    if (model[p.Row, p.Column] != value)
                    {
                        changed = true;
                    }

                    model[p.Row, p.Column] = value;
                }
            }

            return changed;
        }

        private void SpawnTile(GameModel model)
        {
            var empty = new List<(int Row, int Column)>();

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (model[row, column] == 0)
                    {
                        empty.Add((row, column));
                    }
                }
            }

            if (empty.Count == 0)
            {
                return;
            }

            var cell = empty[Random.Next(empty.Count)];
            model[cell.Row, cell.Column] = Random.Next(10) == 0 ? 4 : 2;
        }

        private bool IsGameOver(GameModel model)
        {
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    int value = model[row, column];

                    if (value == 0)
                    {
                        return false;
                    }

                    if (column + 1 < Constants.ColumnCount &&
                        value == model[row, column + 1])
                    {
                        return false;
                    }

                    if (row + 1 < Constants.RowCount &&
                        value == model[row + 1, column])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public GameModel NewGame(int userId)
        {
            var model = new GameModel
            {
                UserId = userId
            };

            SpawnTile(model);
            SpawnTile(model);

            int gameId = _repository.Save(ToDto(model));

            return GetByGameId(gameId);
        }

        private GameModel FromDto(GameDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            var model = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount,
                Score = dto.Score,
                BestScore = dto.BestScore
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = dto.Cells[row, column];
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
                MoveCount = model.MoveCount,
                Score = model.Score,
                BestScore = model.BestScore
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    dto.Cells[row, column] = model[row, column];
                }
            }

            return dto;
        }
    }
}
