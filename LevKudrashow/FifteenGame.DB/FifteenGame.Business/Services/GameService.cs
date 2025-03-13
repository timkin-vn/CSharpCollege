using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using FifteenGame.DataAccess.Repoistories;
using System;
using System.Linq;

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
            if (dtos.Any())
            {
                return FromDto(dtos.Last());
            }

            var newGame = new GameModel
            {
                UserId = userId,
                GameStart = DateTime.Now,
            };

            Initialize(newGame);
            newGame.PlayerCount = 0;
            var id = _gameRepository.Save(ToDto(newGame));
            return GetByGameId(id);
        }
        int[,] list = new int[5, 5] {
            {0,1,0,0,0},
            {0,0,0,1,0},
            {0,1,1,1,1},
            {0,1,0,0,0},
            {0,0,0,1,0}};
        public void Initialize(GameModel model)
        {
            int value = 1;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (list[row, column] == 0)
                    {
                        model[row, column] = value++;
                    }
                    else
                    {
                        model[row, column] = -value++;
                    }
                }
            }
            model.PlayerCount = 0;
            model[Constants.RowCount - 1, Constants.ColumnCount - 1] = model.PlayerCount;
            model.PlayerRow = Constants.RowCount - 1;
            model.PlayerColumn = Constants.ColumnCount - 1;
        }

        public bool IsGameOver(GameModel model)
        {
            int freeCellRow = model.PlayerRow;
            int freeCellColumn = model.PlayerColumn;
            if (freeCellRow != 1 || freeCellColumn != Constants.ColumnCount - 1)
            {
                return false;
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
                    if (model.PlayerColumn == Constants.ColumnCount - 1)
                    {
                        return false;
                    }

                    model.PlayerCount += model[model.PlayerRow, model.PlayerColumn + 1];
                    model[model.PlayerRow, model.PlayerColumn + 1] = 0;
                    model[model.PlayerRow, model.PlayerColumn] = model[model.PlayerRow, model.PlayerColumn + 1];
                    model[model.PlayerRow, model.PlayerColumn + 1] = model.PlayerCount;
                    model.PlayerColumn++;
                    return true;

                case MoveDirection.Right:
                    if (model.PlayerColumn == 0)
                    {
                        return false;
                    }

                    model.PlayerCount += model[model.PlayerRow, model.PlayerColumn - 1];
                    model[model.PlayerRow, model.PlayerColumn - 1] = 0;
                    model[model.PlayerRow, model.PlayerColumn] = model[model.PlayerRow, model.PlayerColumn - 1];
                    model[model.PlayerRow, model.PlayerColumn - 1] = model.PlayerCount;
                    model.PlayerColumn--;
                    return true;

                case MoveDirection.Up:
                    if (model.PlayerRow == Constants.RowCount - 1)
                    {
                        return false;
                    }

                    model.PlayerCount += model[model.PlayerRow + 1, model.PlayerColumn];
                    model[model.PlayerRow + 1, model.PlayerColumn] = 0;
                    model[model.PlayerRow, model.PlayerColumn] = model[model.PlayerRow + 1, model.PlayerColumn];
                    model[model.PlayerRow + 1, model.PlayerColumn] = model.PlayerCount;
                    model.PlayerRow++;
                    return true;

                case MoveDirection.Down:
                    if (model.PlayerRow == 0)
                    {
                        return false;
                    }

                    model.PlayerCount += model[model.PlayerRow - 1, model.PlayerColumn];
                    model[model.PlayerRow - 1, model.PlayerColumn] = 0;
                    model[model.PlayerRow, model.PlayerColumn] = model[model.PlayerRow - 1, model.PlayerColumn];
                    model[model.PlayerRow - 1, model.PlayerColumn] = model.PlayerCount;
                    model.PlayerRow--;
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
                PlayerCount = dto.PlayerCount,
                GameStart = dto.GameStart,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = dto.Cells[row, column];
                    if (dto.Cells[row, column] == dto.PlayerCount)
                    {
                        model.PlayerRow = row;
                        model.PlayerColumn = column;
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
                PlayerCount = model.PlayerCount,
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
