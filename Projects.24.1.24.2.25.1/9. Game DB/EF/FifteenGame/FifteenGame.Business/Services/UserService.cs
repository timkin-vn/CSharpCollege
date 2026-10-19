
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _repository = repository;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _repository.GetAll()
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Name = u.Name
                })
                .ToList();
        }

        public UserModel GetOrCreateUser(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException(
                    "Имя пользователя не может быть пустым.",
                    "userName");
            }

            var userDto = _repository.GetByName(userName.Trim());

            if (userDto == null)
            {
                userDto = _repository.Create(userName.Trim());
            }

            return new UserModel
            {
                Id = userDto.Id,
                Name = userDto.Name
            };
        }

        public UserModel GetUserByName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return null;
            }

            var userDto = _repository.GetByName(userName.Trim());

            return userDto == null
                ? null
                : new UserModel
                {
                    Id = userDto.Id,
                    Name = userDto.Name
                };
        }

        public void DeleteUser(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException("userId");
            }

            _repository.Delete(userId);
        }
    }
}
