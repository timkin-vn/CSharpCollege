using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entites;
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
            using (var db = new FifteenGameDbContext())
            {
                var game = db.Games.Include("Cells").FirstOrDefault(g => g.Id == gameId);
                if (game == null)
                {
                    return null;
                }

                return ToDto(game);
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            using (var db = new FifteenGameDbContext())
            {
                return db.Games.Include("Cells").Where(g => g.UserId == userId).Select(ToDto).ToList();
            }
        }

        public void Remove(int gameId)
        {
            using (var db = new FifteenGameDbContext())
            {
                var game = db.Games.FirstOrDefault(g => g.Id == gameId);
                if (game == null)
                {
                    return;
                }

                db.Games.Remove(game);
                db.SaveChanges();
            }
        }

        public int Save(GameDto gameDto)
        {
            if (gameDto.Id == 0)
            {
                return Create(gameDto);
            }
            else
            {
                return Update(gameDto);
            }
        }

        private int Create(GameDto gameDto)
        {
            using (var db = new FifteenGameDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == gameDto.UserId);
                if (user == null)
                {
                    return 0;
                }

                var newGame = new Game
                {
                    User = user,
                    MoveCount = gameDto.MoveCount,
                    GameStart = gameDto.GameStart,
                    Cells = new List<Cell>(),
                };

                db.Games.Add(newGame);

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        if (gameDto.Cells[row, column] == Constants.FreeCellValue.ToString())

                        {
                            continue;
                        }

                        newGame.Cells.Add(new Cell
                        {
                            Row = row,
                            Column = column,
                            Value = int.TryParse(gameDto.Cells[row, column], out var val) ? val : 0

                        });
                    }
                }

                db.SaveChanges();

                return newGame.Id;
            }
        }

        private int Update(GameDto gameDto)
        {
            using (var db = new FifteenGameDbContext())
            {
                var game = db.Games.Include("Cells").FirstOrDefault(g => g.Id == gameDto.Id);
                if (game == null)
                {
                    return 0;
                }

                game.MoveCount = gameDto.MoveCount;
                var cells = new List<Cell>(game.Cells);
                foreach (var cell in cells)
                {
                    db.Cells.Remove(cell);
                }

                db.SaveChanges();

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        if (gameDto.Cells[row, column] == Constants.FreeCellValue.ToString())

                        {
                            continue;
                        }

                        game.Cells.Add(new Cell
                        {
                            Row = row,
                            Column = column,
                            Value = int.TryParse(gameDto.Cells[row, column], out var val) ? val : 0

                        });
                    }
                }

                db.SaveChanges();
                return game.Id;
            }
        }

        private GameDto ToDto(Game game)
        {
            var result = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                MoveCount = game.MoveCount,
                GameStart = game.GameStart,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result.Cells[row, column] = Constants.FreeCellValue.ToString();

                }
            }

            foreach (var cell in game.Cells)
            {
                result.Cells[cell.Row, cell.Column] = cell.Value.ToString();

            }

            return result;
        }
    }
}
