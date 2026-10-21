using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
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

        public GameModel GetByGameId(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return FromDto(dto);
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

        public void Initialize(GameModel model)
        {
            int pairCount = Constants.RowCount * Constants.ColumnCount / 2;

            int value = 1;

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = value;

                    if (column % 2 == 1)
                    {
                        value++;
                    }

                    if (value > pairCount)
                    {
                        value = 1;
                    }
                }
            }

            model.MoveCount = 0;
        }

        public bool IsGameOver(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return IsGameOver(FromDto(dto));
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
                        return model[row, column] == Constants.FreeCellValue;
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

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        public void Shuffle(GameModel model)
        {
            Initialize(model);

            var values = new List<int>();

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    values.Add(model[row, column]);
                }
            }

            var random = new Random();

            for (int i = values.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                int temp = values[i];
                values[i] = values[j];
                values[j] = temp;
            }

            int index = 0;

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = values[index];
                    index++;
                }
            }

            model.MoveCount = 0;
        }

        private GameModel FromDto(GameDto dto)
        {
            var result = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result[row, column] = dto.Cells[row, column];
                    if (result[row, column] == Constants.FreeCellValue)
                    {
                        result.FreeCellRow = row;
                        result.FreeCellColumn = column;
                    }
                }
            }

            return result;
        }

        private GameDto ToDto(GameModel model)
        {
            var result = new GameDto
            {
                Id = model.Id,
                UserId = model.UserId,
                MoveCount = model.MoveCount,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result.Cells[row, column] = model[row, column];
                }
            }

            return result;
        }
    }
}
