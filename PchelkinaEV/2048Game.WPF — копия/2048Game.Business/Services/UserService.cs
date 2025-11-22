using _2048Game.Common.BusinessModels;
using _2048Game.Common.Repositories;
using _2048Game.Common.Services;
using _2048Game.DataAccess.Repositories;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048Game.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService() 
        {
            _repository = new UserRepository();
        }
        public IEnumerable<UserModel> GetAllUsers()
        {
            return _repository.GetAll().Select(u => new UserModel
            {
                Id = u.Id,
                Name = u.Name,
            }).ToList();
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
                Name = userDto.Name
            };
        }
    }
}
