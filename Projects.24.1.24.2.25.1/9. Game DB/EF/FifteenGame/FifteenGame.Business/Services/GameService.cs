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
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = Constants.LightOffValue;
                }
            }

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
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (model[row, column] == Constants.LightOnValue)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public GameModel MakeMove(int gameId, int row, int column)
        {
            var dto = _repository.GetByGameId(gameId);
            var model = FromDto(dto);

            MakeMove(model, row, column);

            _repository.Save(ToDto(model));
            return model;
        }

        public bool MakeMove(GameModel model, int row, int column)
        {
            if (row < 0 || row >= Constants.RowCount || column < 0 || column >= Constants.ColumnCount)
            {
                return false;
            }

            Toggle(model, row, column);
            Toggle(model, row - 1, column);
            Toggle(model, row + 1, column);
            Toggle(model, row, column - 1);
            Toggle(model, row, column + 1);
            model.MoveCount++;
            return true;
        }

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        public void Shuffle(GameModel model)
        {
            Initialize(model);

            var rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                MakeMove(model, rnd.Next(Constants.RowCount), rnd.Next(Constants.ColumnCount));
            }

            model.MoveCount = 0;
        }

        private void Toggle(GameModel model, int row, int column)
        {
            if (row < 0 || row >= Constants.RowCount || column < 0 || column >= Constants.ColumnCount)
            {
                return;
            }

            model[row, column] = model[row, column] == Constants.LightOnValue
                ? Constants.LightOffValue
                : Constants.LightOnValue;
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
                MoveCount = dto.MoveCount,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result[row, column] = dto.Cells[row, column];
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
