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
        private readonly IGameRepository _repository;
        private readonly Random _random = new Random();

        public GameService(IGameRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            _repository = repository;
        }

        public GameModel GetByGameId(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return dto == null ? null : FromDto(dto);
        }

        public GameModel GetByUserId(int userId)
        {
            var dto = _repository.GetByUserId(userId)
                .OrderByDescending(game => game.Id)
                .FirstOrDefault();

            if (dto != null)
                return FromDto(dto);

            return NewGame(userId);
        }

        public GameModel NewGame(int userId)
        {
            var previous = _repository.GetByUserId(userId)
                .OrderByDescending(game => game.Id)
                .FirstOrDefault();

            var model = new GameModel
            {
                UserId = userId,
                BestScore = previous == null
                    ? 0
                    : Math.Max(previous.BestScore, previous.Score)
            };

            SpawnTile(model);
            SpawnTile(model);

            model.IsGameOver = IsGameOver(model);
            model.Id = _repository.Save(ToDto(model));

            return model;
        }

        public bool IsGameOver(int gameId)
        {
            var model = GetByGameId(gameId);
            return model == null || IsGameOver(model);
        }

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            var model = GetByGameId(gameId);

            if (model == null || IsGameOver(model))
                return model;

            bool changed = Move(model, direction);

            if (!changed)
                return model;

            SpawnTile(model);
            model.MoveCount++;
            model.BestScore = Math.Max(model.BestScore, model.Score);
            model.IsGameOver = IsGameOver(model);
            model.Id = _repository.Save(ToDto(model));

            return model;
        }

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        private static bool Move(GameModel model, MoveDirection direction)
        {
            bool changed = false;

            for (int line = 0; line < 4; line++)
            {
                int[] values = new int[4];

                for (int index = 0; index < 4; index++)
                {
                    int row;
                    int column;

                    GetPosition(direction, line, index, out row, out column);
                    values[index] = model[row, column];
                }

                var compacted = values
                    .Where(value => value != 0)
                    .ToList();

                for (int index = 0; index < compacted.Count - 1; index++)
                {
                    if (compacted[index] != compacted[index + 1])
                        continue;

                    compacted[index] *= 2;
                    model.Score += compacted[index];
                    compacted.RemoveAt(index + 1);
                }

                while (compacted.Count < 4)
                    compacted.Add(0);

                for (int index = 0; index < 4; index++)
                {
                    int row;
                    int column;

                    GetPosition(direction, line, index, out row, out column);

                    if (model[row, column] != compacted[index])
                        changed = true;

                    model[row, column] = compacted[index];
                }
            }

            return changed;
        }

        private static void GetPosition(
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
                    column = 3 - index;
                    break;

                case MoveDirection.Up:
                    row = index;
                    column = line;
                    break;

                case MoveDirection.Down:
                    row = 3 - index;
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
            var empty = new List<Tuple<int, int>>();

            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    if (model[row, column] == 0)
                        empty.Add(Tuple.Create(row, column));
                }
            }

            if (empty.Count == 0)
                return;

            var position = empty[_random.Next(empty.Count)];

            model[position.Item1, position.Item2] =
                _random.Next(10) == 0 ? 4 : 2;
        }

        private static bool IsGameOver(GameModel model)
        {
            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    if (model[row, column] == 0)
                        return false;

                    if (row < 3 &&
                        model[row, column] == model[row + 1, column])
                        return false;

                    if (column < 3 &&
                        model[row, column] == model[row, column + 1])
                        return false;
                }
            }

            return true;
        }

        private static GameModel FromDto(GameDto dto)
        {
            var model = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount,
                Score = dto.Score,
                BestScore = Math.Max(dto.BestScore, dto.Score)
            };

            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                    model[row, column] = dto.Cells[row, column];
            }

            model.IsGameOver = IsGameOver(model);

            return model;
        }

        private static GameDto ToDto(GameModel model)
        {
            var dto = new GameDto
            {
                Id = model.Id,
                UserId = model.UserId,
                MoveCount = model.MoveCount,
                Score = model.Score,
                BestScore = model.BestScore
            };

            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                    dto.Cells[row, column] = model[row, column];
            }

            return dto;
        }
    }
}