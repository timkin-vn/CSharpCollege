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
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _repository.GetAll()
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Name = u.Name,
                })
                .ToList();
        }

        public UserModel GetOrCreateUser(string userName)
        {
            var userDto = _repository.GetByName(userName);

            if (userDto == null)
            {
                userDto = _repository.Create(userName);
            }

            return new UserModel
            {
                Id = userDto.Id,
                Name = userDto.Name,
            };
        }

        public UserModel GetUserById(int userId)
        {
            var userDto = _repository.GetById(userId);

            if (userDto == null)
            {
                return null;
            }

            return new UserModel
            {
                Id = userDto.Id,
                Name = userDto.Name,
            };
        }

        public UserModel GetUserByName(string userName)
        {
            var userDto = _repository.GetByName(userName);

            if (userDto == null)
            {
                return null;
            }

            return new UserModel
            {
                Id = userDto.Id,
                Name = userDto.Name,
            };
        }
    }
}
