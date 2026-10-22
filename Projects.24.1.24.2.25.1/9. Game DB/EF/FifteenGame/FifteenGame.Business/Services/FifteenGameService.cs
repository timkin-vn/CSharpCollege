using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using System;
using System.Linq;

namespace FifteenGame.Business.Services
{
    /// <summary>Сервис игры «Пятнашки» (реализация IGameService).</summary>
    public class FifteenGameService : IGameService
    {
        private static readonly Random Random = new Random();
        private readonly IGameRepository _repository;

        public FifteenGameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public GameModel GetByGameId(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return dto == null ? null : ToModel(dto);
        }

        public GameModel GetByUserId(int userId)
        {
            // последняя игра пользователя; если игр нет — создаём новую
            var dto = _repository.GetByUserId(userId).OrderByDescending(g => g.Id).FirstOrDefault();
            return dto != null ? ToModel(dto) : CreateNewGame(userId);
        }

        public bool IsGameOver(int gameId)
        {
            var model = GetByGameId(gameId);
            return model != null && IsSolved(model);
        }

        /// <summary>Фишка рядом со свободной клеткой сдвигается в указанном направлении.</summary>
        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            var model = GetByGameId(gameId);
            if (model == null) return null;

            if (TryMove(model, direction))
            {
                model.MoveCount++;
                _repository.Save(ToDto(model));
            }
            return model;
        }

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        private GameModel CreateNewGame(int userId)
        {
            var model = new GameModel { UserId = userId };
            int value = 1;
            for (int r = 0; r < Constants.RowCount; r++)
                for (int c = 0; c < Constants.ColumnCount; c++)
                    model[r, c] = value++;
            model.FreeCellRow = Constants.RowCount - 1;
            model.FreeCellColumn = Constants.ColumnCount - 1;
            model[model.FreeCellRow, model.FreeCellColumn] = Constants.FreeCellValue;

            // перемешиваем случайными допустимыми ходами — расклад всегда решаем
            var directions = new[] { MoveDirection.Left, MoveDirection.Right, MoveDirection.Up, MoveDirection.Down };
            for (int i = 0; i < 200; i++)
                TryMove(model, directions[Random.Next(directions.Length)]);
            model.MoveCount = 0;

            model.Id = _repository.Save(ToDto(model));
            return model;
        }

        private static bool TryMove(GameModel model, MoveDirection direction)
        {
            int fr = model.FreeCellRow, fc = model.FreeCellColumn;
            int tr = fr, tc = fc; // откуда берём фишку

            switch (direction)
            {
                case MoveDirection.Left: tc = fc + 1; break;
                case MoveDirection.Right: tc = fc - 1; break;
                case MoveDirection.Up: tr = fr + 1; break;
                case MoveDirection.Down: tr = fr - 1; break;
                default: return false;
            }

            if (tr < 0 || tr >= Constants.RowCount || tc < 0 || tc >= Constants.ColumnCount)
                return false;

            model[fr, fc] = model[tr, tc];
            model[tr, tc] = Constants.FreeCellValue;
            model.FreeCellRow = tr;
            model.FreeCellColumn = tc;
            return true;
        }

        private static bool IsSolved(GameModel model)
        {
            int expected = 1;
            for (int r = 0; r < Constants.RowCount; r++)
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    if (r == Constants.RowCount - 1 && c == Constants.ColumnCount - 1)
                        return model[r, c] == Constants.FreeCellValue;
                    if (model[r, c] != expected++) return false;
                }
            return true;
        }

        private static GameModel ToModel(GameDto dto)
        {
            var model = new GameModel { Id = dto.Id, UserId = dto.UserId, MoveCount = dto.MoveCount };
            for (int r = 0; r < Constants.RowCount; r++)
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    model[r, c] = dto.Cells[r, c];
                    if (dto.Cells[r, c] == Constants.FreeCellValue)
                    {
                        model.FreeCellRow = r;
                        model.FreeCellColumn = c;
                    }
                }
            return model;
        }

        private static GameDto ToDto(GameModel model)
        {
            var dto = new GameDto { Id = model.Id, UserId = model.UserId, MoveCount = model.MoveCount };
            for (int r = 0; r < Constants.RowCount; r++)
                for (int c = 0; c < Constants.ColumnCount; c++)
                    dto.Cells[r, c] = model[r, c];
            return dto;
        }
    }
}
