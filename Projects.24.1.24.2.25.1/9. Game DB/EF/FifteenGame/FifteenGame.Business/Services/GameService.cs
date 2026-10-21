
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService : IGameService
    {
        private const int Size = 4;
        private readonly IGameRepository _repository;
        private static readonly Random Random = new Random();

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
            var dto = _repository.GetByUserId(userId)
                .OrderBy(g => g.Id)
                .LastOrDefault();

            if (dto == null)
            {
                return CreateNewGame(userId, 0);
            }

            var model = FromDto(dto);

            if (!IsValid2048Board(model))
            {
                int bestScore = model.BestScore;
                _repository.Remove(model.Id);
                return CreateNewGame(userId, bestScore);
            }

            return model;
        }

        public GameModel NewGame(int userId)
        {
            int bestScore = _repository.GetByUserId(userId)
                .Select(g => g.BestScore)
                .DefaultIfEmpty(0)
                .Max();

            foreach (var game in _repository.GetByUserId(userId).ToList())
            {
                _repository.Remove(game.Id);
            }

            return CreateNewGame(userId, bestScore);
        }

        public bool? IsGameOver(int gameId)
        {
            var model = GetByGameId(gameId);
            return model == null ? (bool?)null : IsGameOver(model);
        }

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            var model = GetByGameId(gameId);

            if (model == null)
            {
                return null;
            }

            if (IsGameOver(model))
            {
                return model;
            }

            bool changed = Move(model, direction);

            if (!changed)
            {
                return model;
            }

            model.MoveCount++;
            SpawnTile(model);
            model.BestScore = Math.Max(model.BestScore, model.Score);

            _repository.Save(ToDto(model));

            return model;
        }

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        private GameModel CreateNewGame(int userId, int bestScore)
        {
            var model = new GameModel
            {
                UserId = userId,
                Score = 0,
                BestScore = bestScore,
                MoveCount = 0
            };

            SpawnTile(model);
            SpawnTile(model);

            model.Id = _repository.Save(ToDto(model));

            return model;
        }

        private bool Move(GameModel model, MoveDirection direction)
        {
            if (direction == MoveDirection.None)
            {
                return false;
            }

            bool changed = false;

            for (int line = 0; line < Size; line++)
            {
                var values = new int[Size];

                for (int index = 0; index < Size; index++)
                {
                    int row;
                    int column;
                    GetCoordinates(direction, line, index, out row, out column);
                    values[index] = model[row, column];
                }

                int[] merged = MergeLine(values, model);

                for (int index = 0; index < Size; index++)
                {
                    int row;
                    int column;
                    GetCoordinates(direction, line, index, out row, out column);

                    if (model[row, column] != merged[index])
                    {
                        changed = true;
                        model[row, column] = merged[index];
                    }
                }
            }

            return changed;
        }


        private int[] MergeLine(int[] source, GameModel model)
        {
            var nonZero = source.Where(value => value != 0).ToList();
            var result = new List<int>();

            for (int index = 0; index < nonZero.Count; index++)
            {
                if (index + 1 < nonZero.Count &&
                    nonZero[index] == nonZero[index + 1])
                {
                    int mergedValue = nonZero[index] * 2;
                    result.Add(mergedValue);
                    model.Score += mergedValue;
                    index++;
                }
                else
                {
                    result.Add(nonZero[index]);
                }
            }

            while (result.Count < Size)
            {
                result.Add(0);
            }

            return result.ToArray();
        }


        private void GetCoordinates(
            MoveDirection direction,
            int line,
            int index,
            out int row,
            out int column)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    row = line;
                    column = index;
                    break;

                case MoveDirection.Right:
                    row = line;
                    column = Size - 1 - index;
                    break;

                case MoveDirection.Up:
                    row = index;
                    column = line;
                    break;

                case MoveDirection.Down:
                    row = Size - 1 - index;
                    column = line;
                    break;

                default:
                    row = line;
                    column = index;
                    break;
            }
        }

        private void SpawnTile(GameModel model)
        {
            var emptyCells = new List<Tuple<int, int>>();

            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    if (model[row, column] == 0)
                    {
                        emptyCells.Add(Tuple.Create(row, column));
                    }
                }
            }

            if (emptyCells.Count == 0)
            {
                return;
            }

            var cell = emptyCells[Random.Next(emptyCells.Count)];
            model[cell.Item1, cell.Item2] = Random.Next(10) == 0 ? 4 : 2;
        }

        private bool IsGameOver(GameModel model)
        {
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    if (model[row, column] == 0)
                    {
                        return false;
                    }

                    if (row + 1 < Size &&
                        model[row, column] == model[row + 1, column])
                    {
                        return false;
                    }

                    if (column + 1 < Size &&
                        model[row, column] == model[row, column + 1])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool IsValid2048Board(GameModel model)
        {
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    int value = model[row, column];

                    if (value < 0)
                    {
                        return false;
                    }

                    if (value != 0 && (value & (value - 1)) != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
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

            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
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

            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    dto.Cells[row, column] = model[row, column];
                }
            }

            return dto;
        }
    }
}
