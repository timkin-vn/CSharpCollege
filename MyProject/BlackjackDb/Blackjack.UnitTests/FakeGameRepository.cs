using Blackjack.Common.Contracts.Repositories;
using Blackjack.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack.UnitTests
{
    // Хранит игры в памяти вместо базы данных
    internal class FakeGameRepository : IGameRepository
    {
        private readonly Dictionary<int, GameModel> _games = new Dictionary<int, GameModel>();
        private int _nextId = 1;

        public GameModel GetById(int id)
        {
            return _games.TryGetValue(id, out var game) ? game : null;
        }

        public GameModel GetUnfinished(int userId)
        {
            return _games.Values.LastOrDefault(g => g.UserId == userId && !g.IsFinished);
        }

        public List<GameModel> GetFinished(int userId)
        {
            return _games.Values.Where(g => g.UserId == userId && g.IsFinished).ToList();
        }

        public int Save(GameModel game)
        {
            if (game.Id == 0)
            {
                game.Id = _nextId++;
            }
            _games[game.Id] = game;
            return game.Id;
        }
    }
}
