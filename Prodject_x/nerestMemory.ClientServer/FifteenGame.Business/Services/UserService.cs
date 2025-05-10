using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using FifteenGame.DataAccess.Repositories;
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

        public UserService(IUserRepository userRepository)
        {
            _repository = userRepository;
        }

        

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _repository.GetAll().Select(uDto => new UserModel { Id = uDto.Id, Name = uDto.Name, }).ToList();
        }

        public UserModel GetUserByName(string userName)
        {
            var uDto = _repository.GetByName(userName);

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
            var uDto = _repository.GetByName(userName);

            if (uDto == null)
            {
                uDto = _repository.Create(userName);
            }

            return new UserModel
            {
                Id = uDto.Id,
                Name = uDto.Name,
            };
        }
    }
}
