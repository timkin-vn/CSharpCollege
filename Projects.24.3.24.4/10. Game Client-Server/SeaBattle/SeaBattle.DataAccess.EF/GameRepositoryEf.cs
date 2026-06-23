using System.Collections.Generic;
using System.Linq;
using SeaBattle.Common.Interfaces;
using SeaBattle.Common.Models;
using SeaBattle.DataAccess.EF.Entities;

namespace SeaBattle.DataAccess.EF
{
    public class GameRepositoryEf : IGameRepository
    {
        public GameModel Save(GameModel game)
        {
            using (var ctx = new GameDbContext())
            {
                var e = new GameEntity { UserId = game.UserId, MoveCount = game.MoveCount, Won = game.Won };
                ctx.Games.Add(e);
                ctx.SaveChanges();
                game.Id = e.Id;
                return game;
            }
        }

        public IList<GameModel> GetByUserId(int userId)
        {
            using (var ctx = new GameDbContext())
            {
                return ctx.Games
                    .Where(g => g.UserId == userId)
                    .OrderByDescending(g => g.Id)
                    .ToList()
                    .Select(e => new GameModel { Id = e.Id, UserId = e.UserId, MoveCount = e.MoveCount, Won = e.Won })
                    .ToList();
            }
        }

        public void Remove(int gameId)
        {
            using (var ctx = new GameDbContext())
            {
                var e = ctx.Games.FirstOrDefault(g => g.Id == gameId);
                if (e == null) return;
                ctx.Games.Remove(e);
                ctx.SaveChanges();
            }
        }
    }
}
