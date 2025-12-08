using System;
using System.Collections.Generic;
using System.Linq;
using Игра.Common.BusinessModels;
using Игра.Common.Definitions;
using Игра.Common.Dtos;
using Игра.Common.Repositories;
using Игра.Common.Services;

namespace Игра.Business.Services
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
            for (int r = 0; r < Constants.Size; r++)
            {
                for (int c = 0; c < Constants.Size; c++)
                {
                    model.Cells[r, c] = false;
                }
            }

            var rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                int rr = rnd.Next(Constants.Size);
                int cc = rnd.Next(Constants.Size);
                Toggle(model, rr, cc);
            }

            model.MoveCount = 0;
        }

        private void Toggle(GameModel model, int row, int column)
        {
            int[] TRow = { 0, 0, 0, 1, -1 };
            int[] TCol = { 0, 1, -1, 0, 0 };

            for (int i = 0; i < TRow.Length; i++)
            {
                int newRow = row + TRow[i];
                int newCol = column + TCol[i];

                if (newRow >= 0 && newRow < Constants.Size && newCol >= 0 && newCol < Constants.Size)
                {
                    model.Cells[newRow, newCol] = !model.Cells[newRow, newCol];
                }
            }
        }

        private bool CheckForWin(GameModel model)
        {
            for (int r = 0; r < Constants.Size; r++)
            {
                for (int c = 0; c < Constants.Size; c++)
                {
                    if (!model.Cells[r, c])
                        return false;
                }
            }
            return true;
        }

        private GameModel FromDto(GameDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            var model = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount
            };

            for (int r = 0; r < Constants.Size; r++)
            {
                for (int c = 0; c < Constants.Size; c++)
                {
                    model.Cells[r, c] = dto.Cells[r, c] == 1;
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

            for (int r = 0; r < Constants.Size; r++)
            {
                for (int c = 0; c < Constants.Size; c++)
                {
                    dto.Cells[r, c] = model.Cells[r, c] ? 1 : 0;
                }
            }

            return dto;
        }

        public GameModel MakeMove(int gameId, int row, int column)
        {
            var dto = _repository.GetByGameId(gameId);
            GameModel model;

            if (dto == null)
            {
                model = new GameModel { UserId = 0 };
                Initialize(model);
                var newId = _repository.Save(ToDto(model));
                model.Id = newId;
                return model;
            }

            model = FromDto(dto);

            Toggle(model, row, column);
            model.MoveCount++;

            _repository.Save(ToDto(model));
            return model;
        }

        public bool IsGameOver(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            var model = FromDto(dto);

            if (model == null)
            {
                return false;
            }

            return CheckForWin(model);
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
                UserId = userId
            };

            Initialize(game);
            var id = _repository.Save(ToDto(game));
            return GetByGameId(id);
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
    }
}