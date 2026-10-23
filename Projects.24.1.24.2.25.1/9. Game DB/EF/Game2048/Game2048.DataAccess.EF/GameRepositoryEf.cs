using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Game2048.Common;
using Game2048.Common.Interfaces;
using Game2048.Common.Models;
using Game2048.DataAccess.EF.Entities;

namespace Game2048.DataAccess.EF
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
                        Score = game.Score,
                        MoveCount = game.MoveCount,
                        Cells = new List<CellEntity>()
                    };
                    for (int r = 0; r < Constants.Size; r++)
                        for (int c = 0; c < Constants.Size; c++)
                            entity.Cells.Add(new CellEntity { Row = r, Column = c, Value = game.Field[r, c] });

                    ctx.Games.Add(entity);
                    ctx.SaveChanges();
                    game.Id = entity.Id;
                }
                else
                {
                    var entity = ctx.Games.Include("Cells").FirstOrDefault(g => g.Id == game.Id);
                    if (entity == null) return;
                    entity.Score = game.Score;
                    entity.MoveCount = game.MoveCount;
                    foreach (var cell in entity.Cells)
                        cell.Value = game.Field[cell.Row, cell.Column];
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
                Score = entity.Score,
                MoveCount = entity.MoveCount
            };
            foreach (var cell in entity.Cells)
            {
                if (cell.Row >= 0 && cell.Row < Constants.Size &&
                    cell.Column >= 0 && cell.Column < Constants.Size)
                    model.Field[cell.Row, cell.Column] = cell.Value;
            }
            return model;
        }
    }
}
