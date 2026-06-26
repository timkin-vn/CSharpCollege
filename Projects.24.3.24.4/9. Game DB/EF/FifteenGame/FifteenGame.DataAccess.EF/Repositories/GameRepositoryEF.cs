using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FifteenGame.DataAccess.EF.Repositories
{
    public class GameRepositoryEF : IGameRepository
    {
        public GameDto GetByGameId(int gameId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games
                    .Include(g => g.Cells)
                    .FirstOrDefault(g => g.Id == gameId);

                return ToDto(game);
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var games = context.Games
                    .Include(g => g.Cells)
                    .Where(g => g.UserId == userId)
                    .ToList();

                return games.Select(ToDto).ToList();
            }
        }

        public void Remove(int gameId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games.FirstOrDefault(g => g.Id == gameId);
                if (game != null)
                {
                    context.Games.Remove(game);
                    context.SaveChanges();
                }
            }
        }

        public int Save(GameDto dto)
        {
            using (var context = new FifteenGameDataContext())
            {
                var entity = context.Games
                    .Include(g => g.Cells)
                    .FirstOrDefault(g => g.Id == dto.Id);

                if (entity == null)
                {
                    entity = new Game
                    {
                        UserId = dto.UserId,
                        Money = dto.Money,
                        TurnsPlayed = dto.MoveCount,
                        Cells = new List<Cell>()
                    };

                    context.Games.Add(entity);
                }
                else
                {
                    entity.Money = dto.Money;
                    entity.TurnsPlayed = dto.MoveCount;
                    context.Cells.RemoveRange(entity.Cells);
                    entity.Cells.Clear();
                }

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        entity.Cells.Add(new Cell
                        {
                            Row = row,
                            Column = column,
                            PeopleCount = dto.PeopleCount[row, column],
                            HasShop = dto.HasShop[row, column],
                            IsVeggie = dto.IsVeggie[row, column],
                            IsRevealed = dto.IsRevealed[row, column]
                        });
                    }
                }

                context.SaveChanges();
                return entity.Id;
            }
        }

        private GameDto ToDto(Game game)
        {
            if (game == null) return null;

            var dto = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                Money = game.Money,
                MoveCount = game.TurnsPlayed
            };

            if (game.Cells != null)
            {
                foreach (var cell in game.Cells)
                {
                    if (cell.Row < Constants.RowCount && cell.Column < Constants.ColumnCount)
                    {
                        dto.PeopleCount[cell.Row, cell.Column] = cell.PeopleCount;
                        dto.HasShop[cell.Row, cell.Column] = cell.HasShop;
                        dto.IsVeggie[cell.Row, cell.Column] = cell.IsVeggie;
                        dto.IsRevealed[cell.Row, cell.Column] = cell.IsRevealed;
                    }
                }
            }

            return dto;
        }
    }
}