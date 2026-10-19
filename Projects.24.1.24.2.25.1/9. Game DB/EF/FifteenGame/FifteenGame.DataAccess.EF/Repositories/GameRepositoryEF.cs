
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FifteenGame.DataAccess.EF.Repositories
{
    public class GameRepositoryEF : IGameRepository
    {
        public GameDto GetByGameId(int gameId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games
                    .AsNoTracking()
                    .Include("Cells")
                    .FirstOrDefault(g => g.Id == gameId);

                return ToDto(game);
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var games = context.Games
                    .AsNoTracking()
                    .Include("Cells")
                    .Where(g => g.UserId == userId)
                    .OrderBy(g => g.Id)
                    .ToList();

                return games.Select(ToDto).ToList();
            }
        }

        public void Remove(int gameId)
        {
            using (var context = new FifteenGameDataContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                var game = context.Games
                    .Include("Cells")
                    .FirstOrDefault(g => g.Id == gameId);

                if (game == null)
                {
                    return;
                }

                context.Cells.RemoveRange(game.Cells.ToList());
                context.Games.Remove(game);
                context.SaveChanges();

                transaction.Commit();
            }
        }

        public int Save(GameDto gameDto)
        {
            ValidateGame(gameDto);

            if (gameDto.Id == 0)
            {
                return Create(gameDto);
            }

            if (gameDto.Id < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "gameDto.Id", "Идентификатор игры не может быть отрицательным.");
            }

            return Update(gameDto);
        }

        private int Create(GameDto gameDto)
        {
            using (var context = new FifteenGameDataContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                var userExists = context.Users.Any(u => u.Id == gameDto.UserId);

                if (!userExists)
                {
                    throw new InvalidOperationException(
                        "Нельзя создать игру: пользователь не найден.");
                }

                var game = new Game
                {
                    UserId = gameDto.UserId,
                    MoveCount = gameDto.MoveCount,
                    Cells = new List<Cell>()
                };

                context.Games.Add(game);
                context.SaveChanges();

                AddCells(context, game.Id, gameDto);
                context.SaveChanges();

                transaction.Commit();
                return game.Id;
            }
        }

        private int Update(GameDto gameDto)
        {
            using (var context = new FifteenGameDataContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                var game = context.Games
                    .Include("Cells")
                    .FirstOrDefault(g => g.Id == gameDto.Id);

                if (game == null)
                {
                    throw new InvalidOperationException(
                        "Нельзя обновить игру: игра не найдена.");
                }

                if (game.UserId != gameDto.UserId)
                {
                    throw new InvalidOperationException(
                        "Нельзя изменить владельца существующей игры.");
                }

                game.MoveCount = gameDto.MoveCount;

                context.Cells.RemoveRange(game.Cells.ToList());
                context.SaveChanges();

                AddCells(context, game.Id, gameDto);
                context.SaveChanges();

                transaction.Commit();
                return game.Id;
            }
        }

        private void AddCells(
            FifteenGameDataContext context,
            int gameId,
            GameDto gameDto)
        {
            var cells = new List<Cell>();

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0;
                     column < Constants.ColumnCount;
                     column++)
                {
                    int value = gameDto.Cells[row, column];

                    if (value == Constants.FreeCellValue)
                    {
                        continue;
                    }

                    cells.Add(new Cell
                    {
                        GameId = gameId,
                        Row = row,
                        Column = column,
                        Value = value
                    });
                }
            }

            context.Cells.AddRange(cells);
        }

        private void ValidateGame(GameDto gameDto)
        {
            if (gameDto == null)
            {
                throw new ArgumentNullException("gameDto");
            }

            if (gameDto.UserId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    "gameDto.UserId",
                    "Необходимо указать корректного пользователя.");
            }

            if (gameDto.MoveCount < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "gameDto.MoveCount",
                    "Количество ходов не может быть отрицательным.");
            }

            var values = new HashSet<int>();
            int freeCellCount = 0;
            int expectedCellCount =
                Constants.RowCount * Constants.ColumnCount;

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0;
                     column < Constants.ColumnCount;
                     column++)
                {
                    int value = gameDto.Cells[row, column];

                    if (value == Constants.FreeCellValue)
                    {
                        freeCellCount++;
                        continue;
                    }

                    if (value < 1 || value >= expectedCellCount)
                    {
                        throw new ArgumentException(
                            "Поле содержит недопустимое значение: " + value);
                    }

                    if (!values.Add(value))
                    {
                        throw new ArgumentException(
                            "Поле содержит повторяющееся число: " + value);
                    }
                }
            }

            if (freeCellCount != 1)
            {
                throw new ArgumentException(
                    "На поле должна быть ровно одна пустая клетка.");
            }

            if (values.Count != expectedCellCount - 1)
            {
                throw new ArgumentException(
                    "Поле должно содержать все числа от 1 до 15.");
            }
        }

        private GameDto ToDto(Game game)
        {
            if (game == null)
            {
                return null;
            }

            var result = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                MoveCount = game.MoveCount
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0;
                     column < Constants.ColumnCount;
                     column++)
                {
                    result.Cells[row, column] = Constants.FreeCellValue;
                }
            }

            foreach (var cell in game.Cells)
            {
                if (cell.Row < 0 ||
                    cell.Row >= Constants.RowCount ||
                    cell.Column < 0 ||
                    cell.Column >= Constants.ColumnCount)
                {
                    throw new InvalidOperationException(
                        "В базе найдена клетка с недопустимыми координатами.");
                }

                result.Cells[cell.Row, cell.Column] = cell.Value;
            }

            return result;
        }
    }
}
