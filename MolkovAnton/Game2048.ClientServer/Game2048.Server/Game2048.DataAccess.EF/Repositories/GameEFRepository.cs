using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048.Common.Definitions;
using Game2048.Common.Dtos;
using Game2048.Common.Repositories;
using Game2048.DataAccess.EF.DataContext;
using Game2048.DataAccess.EF.Entities;

namespace Game2048.DataAccess.EF.Repositories
{
    public class GameEFRepository : IGameRepository
    {
        public GameDto GetByGameId(int gameId)
        {
            using (var db = new Game2048DBContext())
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
            using (var db = new Game2048DBContext())
            {
                return db.Games.Include("Cells").Where(g => g.UserId == userId).Select(ToDto).ToList();
            }
        }

        public void Remove(int gameId)
        {
            using (var db = new Game2048DBContext())
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
            using (var db = new Game2048DBContext())
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
                    for (int col = 0; col < Constants.ColumnCount; col++)
                    {
                        int value = gameDto.Cells[row, col];
                        if (value == 0) continue;

                        newGame.Cells.Add(new Cell
                        {
                            Row = row,
                            Column = col,
                            Value = value
                        });
                    }
                }

                db.SaveChanges();

                return newGame.Id;
            }
        }

        private int Update(GameDto gameDto)
        {
            using (var db = new Game2048DBContext())
            {
                var game = db.Games.Include("Cells").FirstOrDefault(g => g.Id == gameDto.Id);
                if (game == null)
                {
                    return 0;
                }

                game.MoveCount = gameDto.MoveCount;

                db.Cells.RemoveRange(game.Cells);
                db.SaveChanges();

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int col = 0; col < Constants.ColumnCount; col++)
                    {
                        int value = gameDto.Cells[row, col];
                        if (value == 0) continue;

                        game.Cells.Add(new Cell
                        {
                            Row = row,
                            Column = col,
                            Value = value
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
                GameStart = game.GameStart
            };

            foreach (var cell in game.Cells)
            {
                result.Cells[cell.Row, cell.Column] = cell.Value;
            }

            return result;
        }
    }
}
