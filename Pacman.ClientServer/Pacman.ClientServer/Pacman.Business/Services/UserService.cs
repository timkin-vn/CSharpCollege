using Pacman.Common.Interfaces.Repositories;
using Pacman.Common.Interfaces.Services;
using Pacman.Common.Models;

namespace Pacman.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDto GetOrCreateUser(string name)
        {
            var user = _userRepository.GetByName(name);
            if (user == null)
            {
                user = _userRepository.Create(name);
            }
            return user;
        }

        public UserDto GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }
    }
}