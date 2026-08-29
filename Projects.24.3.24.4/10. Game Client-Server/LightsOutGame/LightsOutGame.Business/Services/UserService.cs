using LightsOutGame.Common.BusinessModels;
using LightsOutGame.Common.Contracts.Repositories;
using LightsOutGame.Common.Contracts.Services;
using LightsOutGame.Common.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace LightsOutGame.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _userRepository.GetAll().Select(FromDto);
        }

        public UserModel GetUserByName(string userName)
        {
            return FromDto(_userRepository.GetByName(userName));
        }

        public UserModel GetOrCreateUser(string userName)
        {
            var userDto = _userRepository.GetByName(userName) ?? _userRepository.Create(userName);
            return FromDto(userDto);
        }

        private UserModel FromDto(UserDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new UserModel { Id = dto.Id, Name = dto.Name, };
        }
    }
}
