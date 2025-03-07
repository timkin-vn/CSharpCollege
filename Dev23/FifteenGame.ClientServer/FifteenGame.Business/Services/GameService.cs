using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using System;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public GameModel GetByGameId(int gameId)
        {
            var dto = _gameRepository.GetByGameId(gameId);
            return FromDto(dto);
        }

        public GameModel GetByUserId(int userId)
        {
            var dtos = _gameRepository.GetByUserId(userId);
            if (dtos.Any())
            {
                return FromDto(dtos.Last());
            }

            var newGame = new GameModel
            {
                UserId = userId,
                GameStart = DateTime.Now,
            };

            Shuffle(newGame);
            newGame.MoveCount = 0;
            var id = _gameRepository.Save(ToDto(newGame));
            return GetByGameId(id);
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
            var game = FromDto(_gameRepository.GetByGameId(gameId));
            return IsGameOver(game);
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
            var game = FromDto(_gameRepository.GetByGameId(gameId));
            MakeMove(game, direction);
            _gameRepository.Save(ToDto(game));

            return game;
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
            if (dto == null)
            {
                return null;
            }

            var model = new GameModel
            {
                GameId = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount,
                GameStart = dto.GameStart,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = dto.Cells[row, column];
                    if (dto.Cells[row, column] == Constants.FreeCellValue)
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
                Id = model.GameId,
                UserId = model.UserId,
                MoveCount = model.MoveCount,
                GameStart = model.GameStart,
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
