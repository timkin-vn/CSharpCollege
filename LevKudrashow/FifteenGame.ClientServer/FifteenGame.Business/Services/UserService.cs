using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
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
            return _userRepository.GetAll().Select(uDto => new UserModel { Id = uDto.Id, Name = uDto.Name, }).ToList();
        }

        public UserModel GetUserByName(string userName)
        {
            var uDto = _userRepository.GetByName(userName);

            if (uDto != null)
            {
                return new UserModel
                {
                    Id = uDto.Id,
                    Name = uDto.Name,
                };
            }

            return null;
        }

        public UserModel GetOrCreateUser(string userName)
        {
            var uDto = _userRepository.GetByName(userName);

            if (uDto == null)
            {
                uDto = _userRepository.Create(userName);
            }

            return new UserModel
            {
                Id = uDto.Id,
                Name = uDto.Name,
            };
        }
    }
}
