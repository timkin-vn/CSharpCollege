using Nonogram.Common.BusinessModels;
using Nonogram.Common.Dtos;
using Nonogram.Common.Repositories;
using Nonogram.Common.Services;
using System.Collections.Generic;
using System.Linq;

namespace Nonogram.Business.Services
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