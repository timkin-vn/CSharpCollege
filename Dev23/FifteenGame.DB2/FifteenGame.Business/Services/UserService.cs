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

        public UserService(IUserRepository userRepository)
        {
            _repository = userRepository;
        }

        public UserModel Create(string userName)
        {
            var uDto = _repository.Create(userName);
            return new UserModel
            {
                Id = uDto.Id,
                Name = uDto.Name,
            };
        }

        public IEnumerable<UserModel> GetAll()
        {
            var uDtos = _repository.GetAll();
            return uDtos
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Name = u.Name,
                })
                .ToList();
        }

        public UserModel GetByName(string userName)
        {
            var uDto = _repository.GetByName(userName);
            return uDto == null ?
                null :
                new UserModel
                {
                    Id = uDto.Id,
                    Name = uDto.Name,
                };
        }
    }
}
