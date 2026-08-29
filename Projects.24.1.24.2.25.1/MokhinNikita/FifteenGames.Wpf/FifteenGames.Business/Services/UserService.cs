using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FifteenGames.Common.BusinessModels;
using FifteenGames.DataAccess.Repositories;

namespace FifteenGames.Business.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository = new UserRepository();
        public IEnumerable<UserModel> GetAllUsers()
        {
            return (from u in _userRepository.GetAll()
                    select new UserModel
                    {
                        Id = u.Id,
                        Name = u.Name
                    }).ToList();
        }

        public UserModel GetOrCreateUser(string userName)
        {
            var userDto = _userRepository.GetByName(userName);
            if(userDto == null)
            {
                userDto = _userRepository.Create(userName);
            }
            return new UserModel
            {
                Id = userDto.Id,
                Name = userDto.Name
            };
        }
        public UserModel GetUserByName(string name)
        {
            var userDto = _userRepository.GetByName(name);
            return userDto == null ? null : new UserModel
            {
                Id = userDto.Id,
                Name = userDto.Name
            };
        }
    }
}
