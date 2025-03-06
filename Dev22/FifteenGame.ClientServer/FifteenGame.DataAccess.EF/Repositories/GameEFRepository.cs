using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.EF.Repositories
{
    public class GameEFRepository : IGameRepository
    {
        public GameDto GetByGameId(int gameId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games.Include("Cells").FirstOrDefault(g => g.Id == gameId);
                if (game == null)
                {
                    return null;
                }

                return ToDto(game);
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var games = context.Games.Include("Cells").Where(g => g.UserId == userId);
                return games.Select(ToDto).ToList();
            }
        }

        public void Remove(int gameId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games.Include("Cells").FirstOrDefault(g => g.Id == gameId);
                if (game == null)
                {
                    return;
                }

                context.Games.Remove(game);
                context.SaveChanges();
            }
        }

        public int Save(GameDto gameDto)
        {
            if (gameDto.Id == 0)
            {
                return Create(gameDto);
            }

            return Update(gameDto);
        }

        private int Create(GameDto gameDto)
        {
            using (var context = new FifteenGameDataContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == gameDto.UserId);
                if (user == null)
                {
                    throw new Exception("Нет такого пользователя");
                }

                var newGame = new Game
                {
                    User = user,
                    GameStart = gameDto.GameBegin,
                    MoveCount = gameDto.MoveCount,
                    Cells = new List<Cell>(),
                };

                context.Games.Add(newGame);

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        if (gameDto.Cells[row, column] == Constants.FreeCellValue)
                        {
                            continue;
                        }

                        newGame.Cells.Add(new Cell
                        {
                            Row = row,
                            Column = column,
                            Value = gameDto.Cells[row, column],
                        });
                    }
                }

                context.SaveChanges();

                return newGame.Id;
            }
        }

        private int Update(GameDto gameDto)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games.Include("Cells").FirstOrDefault(g => g.Id == gameDto.Id);
                if (game == null)
                {
                    throw new Exception("Нет такой игры");
                }

                game.MoveCount = gameDto.MoveCount;
                var gameCells = game.Cells.ToArray();
                foreach (var cell in gameCells)
                {
                    context.Cells.Remove(cell);
                }

                context.SaveChanges();

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        if (gameDto.Cells[row, column] == Constants.FreeCellValue)
                        {
                            continue;
                        }

                        game.Cells.Add(new Cell
                        {
                            Row = row,
                            Column = column,
                            Value = gameDto.Cells[row, column],
                        });
                    }
                }

                context.SaveChanges();

                return game.Id;
            }
        }

        public GameDto ToDto(Game game)
        {
            var result = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                GameBegin = game.GameStart,
                MoveCount = game.MoveCount,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result.Cells[row, column] = Constants.FreeCellValue;
                }
            }

            foreach (var cell in game.Cells)
            {
                result.Cells[cell.Row, cell.Column] = cell.Value;
            }

            return result;
        }
    }
}
