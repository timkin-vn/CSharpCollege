using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public void Initialize(GameModel model)
        {
            int value = 1;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = value++;
                }
            }

            model[Constants.RowCount - 1, Constants.ColumnCount - 1] = Constants.FreeCellValue;
            model.FreeCellRow = Constants.RowCount - 1;
            model.FreeCellColumn =  Constants.ColumnCount - 1;

            model.MoveCount = 0;
        }

        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    if (model.FreeCellColumn == Constants.ColumnCount - 1)
                    {
                        return false;
                    }

                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow, model.FreeCellColumn + 1];
                    model[model.FreeCellRow, model.FreeCellColumn + 1] = Constants.FreeCellValue;
                    model.FreeCellColumn++;
                    model.MoveCount++;
                    return true;

                case MoveDirection.Right:
                    if (model.FreeCellColumn == 0)
                    {
                        return false;
                    }

                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow, model.FreeCellColumn - 1];
                    model[model.FreeCellRow, model.FreeCellColumn - 1] = Constants.FreeCellValue;
                    model.FreeCellColumn--;
                    model.MoveCount++;
                    return true;

                case MoveDirection.Up:
                    if (model.FreeCellRow == Constants.RowCount - 1)
                    {
                        return false;
                    }

                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow + 1, model.FreeCellColumn];
                    model[model.FreeCellRow + 1, model.FreeCellColumn] = Constants.FreeCellValue;
                    model.FreeCellRow++;
                    model.MoveCount++;
                    return true;

                case MoveDirection.Down:
                    if (model.FreeCellRow == 0)
                    {
                        return false;
                    }

                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow - 1, model.FreeCellColumn];
                    model[model.FreeCellRow - 1, model.FreeCellColumn] = Constants.FreeCellValue;
                    model.FreeCellRow--;
                    model.MoveCount++;
                    return true;
            }

            return false;
        }

        public void Shuffle(GameModel model)
        {
            Initialize(model);

            var rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                var nextMove = (MoveDirection)(rnd.Next(4) + 1);
                MakeMove(model, nextMove);
            }

            model.MoveCount = 0;
        }

        public bool IsGameOver(GameModel model)
        {
            int freeCellRow = model.FreeCellRow;
            if (freeCellRow != Constants.RowCount - 1)
            {
                return false;
            }

            int freeCellColumn = model.FreeCellColumn;
            if (freeCellColumn != Constants.ColumnCount - 1)
            {
                return false;
            }

            int value = 1;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (row == freeCellRow && column == freeCellColumn)
                    {
                        if (model[row, column] != Constants.FreeCellValue)
                        {
                            return false;
                        }
                    }
                    else if (model[row, column] != value++)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            var dto = _repository.GetByGameId(gameId);
            var model = FromDto(dto);

            MakeMove(model, direction);

            _repository.Save(ToDto(model));
            return model;
        }

        public bool IsGameOver(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return IsGameOver(FromDto(dto));
        }

        public GameModel GetByUserId(int userId)
        {
            var dtos = _repository.GetByUserId(userId);
            var dto = dtos.LastOrDefault();
            if (dto != null)
            {
                return FromDto(dto);
            }

            var game = new GameModel
            {
                UserId = userId,
            };

            Shuffle(game);
            var gameId = _repository.Save(ToDto(game));

            return GetByGameId(gameId);
        }

        public GameModel GetByGameId(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return FromDto(dto);
        }

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        private GameModel FromDto(GameDto dto)
        {
            var result = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    int index = row * Constants.ColumnCount + column;
                    result[row, column] = dto.Cells[index];
                    if (result[row, column] == Constants.FreeCellValue)
                    {
                        result.FreeCellRow = row;
                        result.FreeCellColumn = column;
                    }
                }
            }

            return result;
        }

        private GameDto ToDto(GameModel game)
        {
            var dto = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                Score = 0,
                IsGameOver = false,
                HasWon = false,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    int index = row * Constants.ColumnCount + column;
                    dto.Cells[index] = game[row, column];
                }
            }

            return dto;
        }
    }
}
