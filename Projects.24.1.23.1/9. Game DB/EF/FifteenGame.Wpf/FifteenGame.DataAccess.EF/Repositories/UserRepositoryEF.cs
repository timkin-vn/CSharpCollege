using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.EF.Repositories
{
    public class UserRepositoryEF : IUserRepository
    {
        public UserDto Create(string userName)
        {
            using (var context = new FifteenGameDataContext())
            {
                var newUser = new User { Name = userName, };
                context.Users.Add(newUser);
                context.SaveChanges();

                return ToDto(newUser);
            }
        }

        public IEnumerable<UserDto> GetAll()
        {
            using (var context = new FifteenGameDataContext())
            {
                var users = context.Users;
                return users.Select(ToDto).ToList();
            }
        }

        public UserDto GetByName(string userName)
        {
            using (var context = new FifteenGameDataContext())
            {
                return ToDto(context.Users.FirstOrDefault(u => u.Name == userName));
            }
        }

        private UserDto ToDto(User u)
        {
            if (u == null)
            {
                return null;
            }

            return new UserDto { Id = u.Id, Name = u.Name, };
        }
    }
}
