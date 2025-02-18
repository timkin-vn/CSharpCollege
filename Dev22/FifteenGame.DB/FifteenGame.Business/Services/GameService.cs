using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using FifteenGame.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService()
        {
            _gameRepository = new GameRepository();
        }

        public GameModel GetByGameId(int gameId)
        {
            var dto = _gameRepository.GetByGameId(gameId);
            return FromDto(dto);
        }

        public GameModel GetByUserId(int userId)
        {
            var dtos = _gameRepository.GetByUserId(userId);
            var dto = dtos.LastOrDefault();
            if (dto != null)
            {
                return FromDto(dto);
            }

            var game = new GameModel
            {
                UserId = userId,
                GameBegin = DateTime.Now,
                MoveCount = 0,
            };

            Shuffle(game);
            game.MoveCount = 0;
            dto = ToDto(game);
            var gameId = _gameRepository.Save(dto);

            return GetByGameId(gameId);
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
            model.FreeCellColumn = Constants.ColumnCount - 1;
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

        public bool IsGameOver(int gameId)
        {
            var gameDto = _gameRepository.GetByGameId(gameId);
            var result = IsGameOver(FromDto(gameDto));
            return result;
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

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            var gameDto = _gameRepository.GetByGameId(gameId);
            var gameModel = FromDto(gameDto);

            MakeMove(gameModel, direction);

            _gameRepository.Save(ToDto(gameModel));
            return gameModel;
        }

        public void RemoveGame(int gameId)
        {
            _gameRepository.Remove(gameId);
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
        }

        private GameModel FromDto(GameDto dto)
        {
            var result = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount,
                GameBegin = dto.GameBegin,
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

        private GameDto ToDto(GameModel game)
        {
            var dto = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                MoveCount = game.MoveCount,
                GameBegin = game.GameBegin,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    dto.Cells[row, column] = game[row, column];
                }
            }

            return dto;
        }
    }
}
