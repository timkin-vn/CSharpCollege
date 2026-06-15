using LightsOutGame.Common.Contracts.Repositories;
using LightsOutGame.Common.Dtos;
using LightsOutGame.DataAccess.EF.DataContext;
using LightsOutGame.DataAccess.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutGame.DataAccess.EF.Repositories
{
    public class UserRepositoryEF : IUserRepository
    {
        public UserDto Create(string userName)
        {
            using (var context = new LightsOutGameDataContext())
            {
                var newUser = new User { Name = userName };
                context.Users.Add(newUser);
                context.SaveChanges();

                return ToDto(newUser);
            }
        }

        public IEnumerable<UserDto> GetAll()
        {
            using (var context = new LightsOutGameDataContext())
            {
                return context.Users.Select(ToDto).ToList();
            }
        }

        public UserDto GetById(int userId)
        {
            using (var context = new LightsOutGameDataContext())
            {
                return ToDto(context.Users.FirstOrDefault(u => u.Id == userId));
            }
        }

        public UserDto GetByName(string userName)
        {
            using (var context = new LightsOutGameDataContext())
            {
                return ToDto(context.Users.FirstOrDefault(u => u.Name == userName));
            }
        }

        public UserDto ToDto(User user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
            };
        }
    }
}
