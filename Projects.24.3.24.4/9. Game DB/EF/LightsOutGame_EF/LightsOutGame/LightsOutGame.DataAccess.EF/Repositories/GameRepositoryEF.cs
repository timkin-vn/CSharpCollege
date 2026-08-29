using LightsOutGame.Common.Contracts.Repositories;
using LightsOutGame.Common.Definitions;
using LightsOutGame.Common.Dtos;
using LightsOutGame.DataAccess.EF.DataContext;
using LightsOutGame.DataAccess.EF.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutGame.DataAccess.EF.Repositories
{
    public class GameRepositoryEF : IGameRepository
    {
        public GameDto GetByGameId(int gameId)
        {
            using (var context = new LightsOutGameDataContext())
            {
                return ToDto(context.Games.Include("Cells").FirstOrDefault(g => g.Id == gameId));
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            using (var context = new LightsOutGameDataContext())
            {
                var games = context.Games.Include("Cells").Where(g => g.UserId == userId);
                return games.Select(ToDto).ToList();
            }
        }

        public void Remove(int gameId)
        {
            using (var context = new LightsOutGameDataContext())
            {
                var game = context.Games.Include("Cells").FirstOrDefault(g => g.Id == gameId);
                if (game == null)
                {
                    return;
                }

                context.Cells.RemoveRange(game.Cells);
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
            using (var context = new LightsOutGameDataContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == gameDto.UserId);
                if (user == null)
                {
                    throw new Exception("Нет такого пользователя");
                }

                var newGame = new Game
                {
                    User = user,
                    MoveCount = gameDto.MoveCount,
                    Cells = new List<Cell>(),
                };

                context.Games.Add(newGame);

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        newGame.Cells.Add(new Cell
                        {
                            Row = row,
                            Column = column,
                            IsOn = gameDto.Cells[row, column],
                        });
                    }
                }

                context.SaveChanges();
                return newGame.Id;
            }
        }

        private int Update(GameDto gameDto)
        {
            using (var context = new LightsOutGameDataContext())
            {
                var game = context.Games.Include("Cells").FirstOrDefault(g => g.Id == gameDto.Id);
                if (game == null)
                {
                    throw new Exception("Нет такой игры");
                }

                game.MoveCount = gameDto.MoveCount;

                foreach (var cell in game.Cells)
                {
                    cell.IsOn = gameDto.Cells[cell.Row, cell.Column];
                }

                context.SaveChanges();
                return gameDto.Id;
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
                MoveCount = game.MoveCount,
            };

            foreach (var cell in game.Cells)
            {
                result.Cells[cell.Row, cell.Column] = cell.IsOn;
            }

            return result;
        }
    }
}
