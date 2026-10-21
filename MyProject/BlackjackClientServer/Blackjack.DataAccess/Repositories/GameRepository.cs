using Blackjack.Common.Contracts.Repositories;
using Blackjack.Common.Models;
using Blackjack.DataAccess.DataContext;
using Blackjack.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly string _connectionString;

        public GameRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public GameModel GetById(int id)
        {
            using (var db = new BlackjackDbContext(_connectionString))
            {
                var entity = db.Games.AsNoTracking().FirstOrDefault(g => g.Id == id);
                return entity == null ? null : ToModel(entity);
            }
        }

        public GameModel GetUnfinished(int userId)
        {
            int playing = (int)GameStatus.Playing;
            using (var db = new BlackjackDbContext(_connectionString))
            {
                var entity = db.Games.AsNoTracking()
                    .Where(g => g.UserId == userId && g.Status == playing)
                    .OrderByDescending(g => g.Id)
                    .FirstOrDefault();
                return entity == null ? null : ToModel(entity);
            }
        }

        public List<GameModel> GetFinished(int userId)
        {
            int playing = (int)GameStatus.Playing;
            using (var db = new BlackjackDbContext(_connectionString))
            {
                return db.Games.AsNoTracking()
                    .Where(g => g.UserId == userId && g.Status != playing)
                    .OrderByDescending(g => g.Id)
                    .ToList()
                    .Select(ToModel)
                    .ToList();
            }
        }

        public int Save(GameModel game)
        {
            using (var db = new BlackjackDbContext(_connectionString))
            {
                Game entity;
                if (game.Id == 0)
                {
                    entity = new Game { UserId = game.UserId, CreatedAt = game.CreatedAt };
                    db.Games.Add(entity);
                }
                else
                {
                    entity = db.Games.Find(game.Id);
                    if (entity == null)
                    {
                        throw new KeyNotFoundException($"Игра с номером {game.Id} не найдена.");
                    }
                }

                entity.Status = (int)game.Status;
                entity.Message = game.Message ?? "";
                entity.PlayerCards = CardConverter.ToText(game.PlayerCards);
                entity.DealerCards = CardConverter.ToText(game.DealerCards);
                entity.DeckCards = CardConverter.ToText(game.Deck);

                db.SaveChanges();

                game.Id = entity.Id;
                return entity.Id;
            }
        }

        private static GameModel ToModel(Game entity)
        {
            return new GameModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                CreatedAt = entity.CreatedAt,
                Status = (GameStatus)entity.Status,
                Message = entity.Message,
                PlayerCards = CardConverter.FromText(entity.PlayerCards),
                DealerCards = CardConverter.FromText(entity.DealerCards),
                Deck = CardConverter.FromText(entity.DeckCards),
            };
        }
    }
}
