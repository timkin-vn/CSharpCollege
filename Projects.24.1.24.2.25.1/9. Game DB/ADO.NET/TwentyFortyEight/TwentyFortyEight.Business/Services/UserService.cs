using System.Collections.Generic;
using TwentyFortyEight.Common.BusinessModels;
using TwentyFortyEight.Common.Contracts.Repositories;
using TwentyFortyEight.Common.Contracts.Services;

namespace TwentyFortyEight.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userDataRepository;

        public UserService(IUserRepository repository)
        {
            _userDataRepository = repository;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var userList = new List<UserModel>();

            foreach (var dto in _userDataRepository.GetAll())
            {
                userList.Add(new UserModel
                {
                    Id = dto.Id,
                    Name = dto.Name
                });
            }

            return userList;
        }

        public UserModel GetOrCreateUser(string userName)
        {
            var existingUser = _userDataRepository.GetByName(userName);

            if (existingUser == null)
            {
                existingUser = _userDataRepository.Create(userName);
            }

            return new UserModel
            {
                Id = existingUser.Id,
                Name = existingUser.Name
            };
        }

        public UserModel GetUserByName(string userName)
        {
            var existingUser = _userDataRepository.GetByName(userName);

            if (existingUser == null)
            {
                return null;
            }

            return new UserModel
            {
                Id = existingUser.Id,
                Name = existingUser.Name
            };
        }
    }
}