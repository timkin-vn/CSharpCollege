using System.Collections.Generic;
using System.Linq;
using SeaBattle.Common.Interfaces;
using SeaBattle.Common.Models;

namespace SeaBattle.UnitTests
{
    public class FakeGameRepository : IGameRepository
    {
        private readonly List<GameModel> _games = new List<GameModel>();
        private int _nextId = 1;

        public GameModel Save(GameModel game)
        {
            game.Id = _nextId++;
            _games.Add(game);
            return game;
        }

        public IList<GameModel> GetByUserId(int userId)
        {
            return _games.Where(g => g.UserId == userId).OrderByDescending(g => g.Id).ToList();
        }

        public void Remove(int gameId)
        {
            _games.RemoveAll(g => g.Id == gameId);
        }
    }

    public class FakeUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        private int _nextId = 1;

        public User GetById(int id) { return _users.FirstOrDefault(u => u.Id == id); }
        public User GetByName(string name) { return _users.FirstOrDefault(u => u.Name == name); }

        public User Create(string name)
        {
            var u = new User { Id = _nextId++, Name = name };
            _users.Add(u);
            return u;
        }
    }
}
