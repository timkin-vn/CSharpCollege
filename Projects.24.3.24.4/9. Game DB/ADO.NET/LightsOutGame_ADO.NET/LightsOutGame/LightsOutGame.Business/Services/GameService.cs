using LightsOutGame.Common.BusinessModels;
using LightsOutGame.Common.Contracts.Repositories;
using LightsOutGame.Common.Contracts.Services;
using LightsOutGame.Common.Definitions;
using LightsOutGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        private readonly Random _random = new Random();

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
                    model[row, column] = false;
                }
            }
        }

        public bool IsGameOver(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return IsGameOver(FromDto(dto));
        }

        public bool IsGameOver(GameModel model)
        {
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (model[row, column])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public GameModel PressCell(int gameId, int row, int column)
        {
            var dto = _repository.GetByGameId(gameId);
            var model = FromDto(dto);

            PressCell(model, row, column);

            _repository.Save(ToDto(model));
            return model;
        }

        public bool PressCell(GameModel model, int row, int column)
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

            int presses = _random.Next(8, 16);
            for (int i = 0; i < presses; i++)
            {
                int row = _random.Next(Constants.RowCount);
                int column = _random.Next(Constants.ColumnCount);
                PressCell(model, row, column);
            }

            if (IsGameOver(model))
            {
                PressCell(model, _random.Next(Constants.RowCount), _random.Next(Constants.ColumnCount));
            }

            model.MoveCount = 0;
        }

        private void Toggle(GameModel model, int row, int column)
        {
            if (row < 0 || row >= Constants.RowCount || column < 0 || column >= Constants.ColumnCount)
            {
                return;
            }

            model[row, column] = !model[row, column];
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
