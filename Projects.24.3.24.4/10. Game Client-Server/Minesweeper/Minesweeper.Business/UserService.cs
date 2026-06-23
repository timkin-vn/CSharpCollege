using Minesweeper.Common.Interfaces;
using Minesweeper.Common.Models;

namespace Minesweeper.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User Login(string name)
        {
            var user = _repository.GetByName(name);
            if (user == null)
                user = _repository.Create(name);
            return user;
        }

        public User GetById(int id)
        {
            return _repository.GetById(id);
        }
    }
}
