using Игра.Common.BusinessModels;
using Игра.Common.Dtos;
using Игра.Common.Repositories;
using Игра.Common.Services;
using System.Collections.Generic;
using System.Linq;

namespace Игра.Business.Services
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
            return _repository.GetAll().Select(u => new UserModel
                {
                    Id = u.Id,
                    Name = u.Name
                }).ToList();
        }

        public UserModel GetOrCreateUser(string userName)
        {
            var dto = _repository.GetByName(userName);

            if (dto == null)
            {
                dto = _repository.Create(userName);
            }

            return new UserModel { Id = dto.Id, Name = dto.Name };
        }

        public UserModel GetUserByName(string userName)
        {
            var dto = _repository.GetByName(userName);

            if (dto == null)
            {
                return null;
            }

            return new UserModel { Id = dto.Id, Name = dto.Name };
        }
    }
}