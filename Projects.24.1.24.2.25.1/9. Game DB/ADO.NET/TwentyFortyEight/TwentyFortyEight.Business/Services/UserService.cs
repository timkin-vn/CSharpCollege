using System.Collections.Generic;
using System.Linq;
using TwentyFortyEight.Common.BusinessModels;
using TwentyFortyEight.Common.Contracts.Repositories;
using TwentyFortyEight.Common.Contracts.Services;

namespace TwentyFortyEight.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            
            return _userRepository.GetAll()
                .Select(dto => new UserModel
                {
                    Id = dto.Id,
                    Name = dto.Name
                })
                .ToList();
        }

        public UserModel GetOrCreateUser(string userName)
        {
            var foundUser = _userRepository.GetByName(userName);

            
            if (foundUser == null)
            {
                foundUser = _userRepository.Create(userName);
            }

            return new UserModel
            {
                Id = foundUser.Id,
                Name = foundUser.Name
            };
        }

        public UserModel GetUserByName(string userName)
        {
            var foundUser = _userRepository.GetByName(userName);

            if (foundUser == null) return null;

            return new UserModel
            {
                Id = foundUser.Id,
                Name = foundUser.Name
            };
        }
    }
}