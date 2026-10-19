
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

        private static readonly Random RandomGenerator = new Random();
        private static readonly object RandomLock = new object();

        public GameService(IGameRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _repository = repository;
        }

        public GameModel GetByGameId(int gameId)
        {
            if (gameId <= 0)
            {
                return null;
            }

            return FromDto(_repository.GetByGameId(gameId));
        }

        public GameModel GetByUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException("userId");
            }

            var dtos = _repository.GetByUserId(userId);
            var dto = dtos == null
                ? null
                : dtos.OrderBy(g => g.Id).LastOrDefault();

            if (dto != null)
            {
                return FromDto(dto);
            }

            var game = new GameModel
            {
                UserId = userId
            };

            Shuffle(game);

            var gameId = _repository.Save(ToDto(game));
            return GetByGameId(gameId);
        }

        public void Initialize(GameModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            int value = 1;

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0;
                     column < Constants.ColumnCount;
                     column++)
                {
                    model[row, column] = value++;
                }
            }

            model[Constants.RowCount - 1,
                  Constants.ColumnCount - 1] = Constants.FreeCellValue;

            model.FreeCellRow = Constants.RowCount - 1;
            model.FreeCellColumn = Constants.ColumnCount - 1;
            model.MoveCount = 0;
        }

        public bool? IsGameOver(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);

            if (dto == null)
            {
                return null;
            }

            return IsGameOver(FromDto(dto));
        }

        public bool IsGameOver(GameModel model)
        {
            if (model == null)
            {
                return false;
            }

            if (model.FreeCellRow != Constants.RowCount - 1 ||
                model.FreeCellColumn != Constants.ColumnCount - 1)
            {
                return false;
            }

            int expectedValue = 1;

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0;
                     column < Constants.ColumnCount;
                     column++)
                {
                    if (row == Constants.RowCount - 1 &&
                        column == Constants.ColumnCount - 1)
                    {
                        return model[row, column] == Constants.FreeCellValue;
                    }

                    if (model[row, column] != expectedValue)
                    {
                        return false;
                    }

                    expectedValue++;
                }
            }

            return true;
        }

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            var dto = _repository.GetByGameId(gameId);

            if (dto == null)
            {
                return null;
            }

            var model = FromDto(dto);

            if (model == null || IsGameOver(model))
            {
                return null;
            }

            if (!MakeMove(model, direction))
            {
                return null;
            }

            _repository.Save(ToDto(model));
            return model;
        }

        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            if (model == null)
            {
                return false;
            }

            int row = model.FreeCellRow;
            int column = model.FreeCellColumn;

            if (row < 0 || row >= Constants.RowCount ||
                column < 0 || column >= Constants.ColumnCount)
            {
                return false;
            }

            int targetRow = row;
            int targetColumn = column;

            switch (direction)
            {
                case MoveDirection.Left:
                    targetColumn++;
                    break;

                case MoveDirection.Right:
                    targetColumn--;
                    break;

                case MoveDirection.Up:
                    targetRow++;
                    break;

                case MoveDirection.Down:
                    targetRow--;
                    break;

                default:
                    return false;
            }

            if (targetRow < 0 || targetRow >= Constants.RowCount ||
                targetColumn < 0 || targetColumn >= Constants.ColumnCount)
            {
                return false;
            }

            model[row, column] = model[targetRow, targetColumn];
            model[targetRow, targetColumn] = Constants.FreeCellValue;

            model.FreeCellRow = targetRow;
            model.FreeCellColumn = targetColumn;
            model.MoveCount++;

            return true;
        }

        public void RemoveGame(int gameId)
        {
            if (gameId <= 0)
            {
                return;
            }

            _repository.Remove(gameId);
        }

        public void Shuffle(GameModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            Initialize(model);

            lock (RandomLock)
            {
                const int shuffleSteps = 1000;

                MoveDirection previousMove = 0;

                for (int i = 0; i < shuffleSteps; i++)
                {
                    var availableMoves = GetAvailableMoves(model);

                    if (availableMoves.Count > 1)
                    {
                        var reverseMove = GetReverseDirection(previousMove);
                        availableMoves.Remove(reverseMove);
                    }

                    var nextMove = availableMoves[
                        RandomGenerator.Next(availableMoves.Count)];

                    MakeMove(model, nextMove);
                    previousMove = nextMove;
                }
            }

            model.MoveCount = 0;
        }

        private List<MoveDirection> GetAvailableMoves(GameModel model)
        {
            var moves = new List<MoveDirection>();

            int row = model.FreeCellRow;
            int column = model.FreeCellColumn;

            if (column < Constants.ColumnCount - 1)
            {
                moves.Add(MoveDirection.Left);
            }

            if (column > 0)
            {
                moves.Add(MoveDirection.Right);
            }

            if (row < Constants.RowCount - 1)
            {
                moves.Add(MoveDirection.Up);
            }

            if (row > 0)
            {
                moves.Add(MoveDirection.Down);
            }

            return moves;
        }

        private MoveDirection GetReverseDirection(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    return MoveDirection.Right;

                case MoveDirection.Right:
                    return MoveDirection.Left;

                case MoveDirection.Up:
                    return MoveDirection.Down;

                case MoveDirection.Down:
                    return MoveDirection.Up;

                default:
                    return 0;
            }
        }

        private GameModel FromDto(GameDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            var result = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount
            };

            int freeCells = 0;

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0;
                     column < Constants.ColumnCount;
                     column++)
                {
                    result[row, column] = dto.Cells[row, column];

                    if (result[row, column] == Constants.FreeCellValue)
                    {
                        result.FreeCellRow = row;
                        result.FreeCellColumn = column;
                        freeCells++;
                    }
                }
            }

            if (freeCells != 1)
            {
                throw new InvalidOperationException(
                    "Сохранённая игра содержит некорректное игровое поле.");
            }

            return result;
        }

        private GameDto ToDto(GameModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            var result = new GameDto
            {
                Id = model.Id,
                UserId = model.UserId,
                MoveCount = model.MoveCount
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0;
                     column < Constants.ColumnCount;
                     column++)
                {
                    result.Cells[row, column] = model[row, column];
                }
            }

            return result;
        }
    }
}
