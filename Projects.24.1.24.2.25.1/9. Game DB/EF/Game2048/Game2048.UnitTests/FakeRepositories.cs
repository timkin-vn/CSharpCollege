using System.Collections.Generic;
using System.Linq;
using Game2048.Common.Interfaces;
using Game2048.Common.Models;

namespace Game2048.UnitTests
{

    public class FakeGameRepository : IGameRepository
    {
        private readonly List<GameModel> _games = new List<GameModel>();
        private int _nextId = 1;

        public GameModel GetByUserId(int userId)
        {
            return _games.Where(g => g.UserId == userId).OrderByDescending(g => g.Id).FirstOrDefault();
        }

        public void SaveGame(GameModel game)
        {
            if (game.Id == 0)
            {
                game.Id = _nextId++;
                _games.Add(game);
            }
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
