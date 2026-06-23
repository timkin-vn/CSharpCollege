using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Minesweeper.Common;
using Minesweeper.Common.Interfaces;
using Minesweeper.Common.Models;
using Minesweeper.DataAccess.EF.Entities;

namespace Minesweeper.DataAccess.EF
{
    public class GameRepositoryEf : IGameRepository
    {
        public GameModel GetByUserId(int userId)
        {
            using (var ctx = new GameDbContext())
            {
                var entity = ctx.Games.Include("Cells")
                    .Where(g => g.UserId == userId)
                    .OrderByDescending(g => g.Id)
                    .FirstOrDefault();
                return entity == null ? null : ToModel(entity);
            }
        }

        public void SaveGame(GameModel game)
        {
            using (var ctx = new GameDbContext())
            {
                if (game.Id == 0)
                {
                    var entity = new GameEntity
                    {
                        UserId = game.UserId,
                        IsLost = game.IsLost,
                        MinesPlaced = game.MinesPlaced,
                        MoveCount = game.MoveCount,
                        Cells = new List<CellEntity>()
                    };
                    for (int r = 0; r < Constants.Size; r++)
                        for (int c = 0; c < Constants.Size; c++)
                        {
                            var cell = game.Field[r, c];
                            entity.Cells.Add(new CellEntity
                            {
                                Row = r,
                                Column = c,
                                IsMine = cell.IsMine,
                                IsRevealed = cell.IsRevealed,
                                IsFlagged = cell.IsFlagged,
                                AdjacentMines = cell.AdjacentMines
                            });
                        }

                    ctx.Games.Add(entity);
                    ctx.SaveChanges();
                    game.Id = entity.Id;
                }
                else
                {
                    var entity = ctx.Games.Include("Cells").FirstOrDefault(g => g.Id == game.Id);
                    if (entity == null) return;
                    entity.IsLost = game.IsLost;
                    entity.MinesPlaced = game.MinesPlaced;
                    entity.MoveCount = game.MoveCount;
                    foreach (var cell in entity.Cells)
                    {
                        var source = game.Field[cell.Row, cell.Column];
                        cell.IsMine = source.IsMine;
                        cell.IsRevealed = source.IsRevealed;
                        cell.IsFlagged = source.IsFlagged;
                        cell.AdjacentMines = source.AdjacentMines;
                    }
                    ctx.SaveChanges();
                }
            }
        }

        public void Remove(int gameId)
        {
            using (var ctx = new GameDbContext())
            {
                var entity = ctx.Games.Include("Cells").FirstOrDefault(g => g.Id == gameId);
                if (entity == null) return;
                ctx.Cells.RemoveRange(entity.Cells);
                ctx.Games.Remove(entity);
                ctx.SaveChanges();
            }
        }

        private static GameModel ToModel(GameEntity entity)
        {
            var model = new GameModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                IsLost = entity.IsLost,
                MinesPlaced = entity.MinesPlaced,
                MoveCount = entity.MoveCount
            };
            foreach (var cell in entity.Cells)
            {
                if (cell.Row >= 0 && cell.Row < Constants.Size &&
                    cell.Column >= 0 && cell.Column < Constants.Size)
                {
                    model.Field[cell.Row, cell.Column] = new Cell
                    {
                        IsMine = cell.IsMine,
                        IsRevealed = cell.IsRevealed,
                        IsFlagged = cell.IsFlagged,
                        AdjacentMines = cell.AdjacentMines
                    };
                }
            }
            return model;
        }
    }
}
