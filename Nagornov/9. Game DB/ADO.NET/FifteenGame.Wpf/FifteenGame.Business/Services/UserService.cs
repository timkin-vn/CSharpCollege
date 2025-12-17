using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserModel GetUserByName(string userName)
        {
            var userDto = _userRepository.GetByName(userName);
            if (userDto == null) return null;

            return new UserModel
            {
                Id = userDto.Id,
                Name = userDto.Name
            };
        }

        public UserModel GetOrCreateUser(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                userName = "Гость";

            var userDto = _userRepository.GetByName(userName);
            if (userDto != null)
            {
                return new UserModel
                {
                    Id = userDto.Id,
                    Name = userDto.Name
                };
            }

            try
            {
                userDto = _userRepository.Create(userName);
                if (userDto != null)
                {
                    return new UserModel
                    {
                        Id = userDto.Id,
                        Name = userDto.Name
                    };
                }
            }
            catch { }

            return new UserModel
            {
                Id = -1,
                Name = userName
            };
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _userRepository.GetAll()
                .Select(dto => new UserModel
                {
                    Id = dto.Id,
                    Name = dto.Name
                });
        }
    }
}