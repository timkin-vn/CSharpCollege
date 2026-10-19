using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using System;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository
                ?? throw new ArgumentNullException(nameof(repository));
        }

        public GameModel GetByGameId(int gameId)
        {
            if (gameId <= 0)
                return null;

            return FromDto(_repository.GetByGameId(gameId));
        }

        public GameModel GetByUserId(int userId)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            var games = _repository.GetByUserId(userId);
            var dto = games?.OrderBy(g => g.Id).LastOrDefault();

            if (dto != null)
                return FromDto(dto);

            var game = new GameModel { UserId = userId };

            Shuffle(game);

            int gameId = _repository.Save(ToDto(game));

            return GetByGameId(gameId);
        }

        public void Initialize(GameModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

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

        public bool IsGameOver(int gameId)
        {
            var model = GetByGameId(gameId);
            return model != null && IsGameOver(model);
        }

        public bool IsGameOver(GameModel model)
        {
            if (model == null)
                return false;

            if (model.FreeCellRow != Constants.RowCount - 1 ||
                model.FreeCellColumn != Constants.ColumnCount - 1)
            {
                return false;
            }

            int expected = 1;

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

                    if (model[row, column] != expected++)
                        return false;
                }
            }

            return true;
        }

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            var model = GetByGameId(gameId);

            if (model == null)
                return null;

            if (!MakeMove(model, direction))
                return model;

            _repository.Save(ToDto(model));

            return model;
        }

        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            if (model == null)
                return false;

            switch (direction)
            {
                case MoveDirection.Left:
                    if (model.FreeCellColumn >= Constants.ColumnCount - 1)
                        return false;

                    model[model.FreeCellRow, model.FreeCellColumn] =
                        model[model.FreeCellRow, model.FreeCellColumn + 1];

                    model[model.FreeCellRow, model.FreeCellColumn + 1] =
                        Constants.FreeCellValue;

                    model.FreeCellColumn++;
                    break;

                case MoveDirection.Right:
                    if (model.FreeCellColumn <= 0)
                        return false;

                    model[model.FreeCellRow, model.FreeCellColumn] =
                        model[model.FreeCellRow, model.FreeCellColumn - 1];

                    model[model.FreeCellRow, model.FreeCellColumn - 1] =
                        Constants.FreeCellValue;

                    model.FreeCellColumn--;
                    break;

                case MoveDirection.Up:
                    if (model.FreeCellRow >= Constants.RowCount - 1)
                        return false;

                    model[model.FreeCellRow, model.FreeCellColumn] =
                        model[model.FreeCellRow + 1, model.FreeCellColumn];

                    model[model.FreeCellRow + 1, model.FreeCellColumn] =
                        Constants.FreeCellValue;

                    model.FreeCellRow++;
                    break;

                case MoveDirection.Down:
                    if (model.FreeCellRow <= 0)
                        return false;

                    model[model.FreeCellRow, model.FreeCellColumn] =
                        model[model.FreeCellRow - 1, model.FreeCellColumn];

                    model[model.FreeCellRow - 1, model.FreeCellColumn] =
                        Constants.FreeCellValue;

                    model.FreeCellRow--;
                    break;

                default:
                    return false;
            }

            model.MoveCount++;
            return true;
        }

        public void RemoveGame(int gameId)
        {
            if (gameId <= 0)
                return;

            _repository.Remove(gameId);
        }

        public void Shuffle(GameModel model)
        {
            Initialize(model);

            var random = new Random();

            for (int i = 0; i < 1000; i++)
            {
                var direction = (MoveDirection)(random.Next(4) + 1);
                MakeMove(model, direction);
            }

            model.MoveCount = 0;
        }

        private GameModel FromDto(GameDto dto)
        {
            if (dto == null)
                return null;

            var model = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0;
                     column < Constants.ColumnCount;
                     column++)
                {
                    model[row, column] = dto.Cells[row, column];

                    if (model[row, column] == Constants.FreeCellValue)
                    {
                        model.FreeCellRow = row;
                        model.FreeCellColumn = column;
                    }
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
                MoveCount = model.MoveCount
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0;
                     column < Constants.ColumnCount;
                     column++)
                {
                    dto.Cells[row, column] = model[row, column];
                }
            }

            return dto;
        }
    }
}