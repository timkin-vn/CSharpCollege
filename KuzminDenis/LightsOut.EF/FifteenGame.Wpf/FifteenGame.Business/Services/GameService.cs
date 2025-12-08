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
            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    model[r, c] = false;
                }
            }
            model.MoveCount = model.MaxMoves;
        }

        public void MakeMoveAt(GameModel model, int row, int column)
        {
            if (model.MoveCount <= 0)
                return;

            Toggle(model, row, column);
            Toggle(model, row - 1, column);
            Toggle(model, row + 1, column);
            Toggle(model, row, column - 1);
            Toggle(model, row, column + 1);

            model.MoveCount--;
        }

        private void Toggle(GameModel model, int row, int column)
        {
            if (row >= 0 && row < GameModel.RowCount && column >= 0 && column < GameModel.ColumnCount)
            {
                model[row, column] = !model[row, column];
            }
        }

        public void Shuffle(GameModel model)
        {
            Initialize(model);
            var rnd = new Random();
            int moves = 50;
            for (int i = 0; i < moves; i++)
            {
                int r = rnd.Next(GameModel.RowCount);
                int c = rnd.Next(GameModel.ColumnCount);
                MakeMoveAt(model, r, c);
            }
            model.MoveCount = model.MaxMoves;
        }

        public bool IsGameOver(GameModel model)
        {
            bool allOff = true;
            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    if (model[r, c])
                    {
                        allOff = false;
                        break;
                    }
                }
                if (!allOff) break;
            }
            bool win = allOff;

            bool loss = (model.MoveCount <= 0) && !win;

            return win || loss;
        }

        public GameModel MakeMove(int gameId, int row, int column)
        {
            var dto = _repository.GetByGameId(gameId);
            if (dto == null) return null;

            var model = FromDto(dto);
            MakeMoveAt(model, row, column);
            _repository.Save(ToDto(model));
            return model;
        }

        public bool IsGameOver(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            if (dto == null) return false;

            var model = FromDto(dto);
            return IsGameOver(model);
        }

        public GameModel GetByUserId(int userId)
        {
            var existingGames = _repository.GetByUserId(userId).ToList();

            if (existingGames.Any())
            {
                var lastGame = existingGames.OrderByDescending(g => g.Id).First();

                return FromDto(lastGame);
            }

            var newModel = new GameModel
            {
                UserId = userId,
                MaxMoves = 50
            };

            Shuffle(newModel);

            newModel.Id = _repository.Save(ToDto(newModel));

            return newModel;
        }


        public GameModel GetByGameId(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            if (dto == null) return null;

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
                MaxMoves = 50,
                MoveCount = dto.MoveCount,
            };

            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    result[r, c] = dto.Cells[r, c] != 0;
                }
            }
            return result;
        }

        private GameDto ToDto(GameModel model)
        {
            var dto = new GameDto
            {
                Id = model.Id,
                UserId = model.UserId,
                MoveCount = model.MoveCount,
            };

            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    dto.Cells[r, c] = model[r, c] ? 1 : 0;
                }
            }
            return dto;
        }
    }
}