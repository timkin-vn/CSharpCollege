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
            var dto = dtos.LastOrDefault();
            if (dto != null)
            {
                return FromDto(dto);
            }

            var game = new GameModel
            {
                UserId = userId,
                GameBegin = DateTime.Now,
                ScoreCount = 0,
            };

            Shuffle(game);
            game.ScoreCount = 0;
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
                    if ((row+column)%2!=0)
                    {
                        model[row, column] *= -1;
                    }
                }
            }

            model[Constants.RowCount - 1, Constants.ColumnCount - 1] = Constants.FreeCellValue;
            model.FreeCellRow = Constants.RowCount - 1;
            model.FreeCellColumn = Constants.ColumnCount - 1;
        }

        public bool IsGameOver(GameModel model)
        {
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (model[row, column] == 1)
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

        public bool MakeMove(GameModel model, MoveDirection direction, bool isShuffling)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    if (model.FreeCellColumn == Constants.ColumnCount - 1)
                    {
                        return false;
                    }
                    model.ScoreCount+= model[model.FreeCellRow, model.FreeCellColumn + 1];
                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow, model.FreeCellColumn + 1];
                    if (!isShuffling)
                    {
                        model[model.FreeCellRow, model.FreeCellColumn] = 0;
                    }
                    model[model.FreeCellRow, model.FreeCellColumn + 1] = Constants.FreeCellValue;
                    model.FreeCellColumn++;
                    return true;

                case MoveDirection.Right:
                    if (model.FreeCellColumn == 0)
                    {
                        return false;
                    }
                    model.ScoreCount += model[model.FreeCellRow, model.FreeCellColumn - 1];
                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow, model.FreeCellColumn - 1];
                    if (!isShuffling)
                    {
                        model[model.FreeCellRow, model.FreeCellColumn] = 0;
                    }
                    model[model.FreeCellRow, model.FreeCellColumn - 1] = Constants.FreeCellValue;
                    model.FreeCellColumn--;
                    return true;

                case MoveDirection.Up:
                    if (model.FreeCellRow == Constants.RowCount - 1)
                    {
                        return false;
                    }
                    model.ScoreCount += model[model.FreeCellRow+1, model.FreeCellColumn];
                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow + 1, model.FreeCellColumn];
                    if (!isShuffling)
                    {
                        model[model.FreeCellRow, model.FreeCellColumn] = 0;
                    }
                    model[model.FreeCellRow + 1, model.FreeCellColumn] = Constants.FreeCellValue;
                    model.FreeCellRow++;
                    return true;

                case MoveDirection.Down:
                    if (model.FreeCellRow == 0)
                    {
                        return false;
                    }
                    model.ScoreCount += model[model.FreeCellRow-1, model.FreeCellColumn];
                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow - 1, model.FreeCellColumn];
                    if (!isShuffling)
                    {
                        model[model.FreeCellRow, model.FreeCellColumn] = 0;
                    }
                    model[model.FreeCellRow - 1, model.FreeCellColumn] = Constants.FreeCellValue;
                    model.FreeCellRow--;
                    return true;
            }

            return false;
        }

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            var gameDto = _gameRepository.GetByGameId(gameId);
            var gameModel = FromDto(gameDto);

            MakeMove(gameModel, direction, false);

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
                MakeMove(model, nextMove, true);
            }
        }

        private GameModel FromDto(GameDto dto)
        {
            var result = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                ScoreCount = dto.ScoreCount,
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
                ScoreCount = game.ScoreCount,
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
