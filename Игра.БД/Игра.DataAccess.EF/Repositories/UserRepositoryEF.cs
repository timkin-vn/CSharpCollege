using Игра.Common.Dtos;
using Игра.Common.Repositories;
using Игра.DataAccess.EF.DataContext;
using Игра.DataAccess.EF.Entites;
using System.Collections.Generic;
using System.Linq;

namespace Игра.DataAccess.EF.Repositories
{
    public class UserRepositoryEF : IUserRepository
    {
        public UserDto Create(string userName)
        {
            using (var context = new ИграDataContext())
            {
                var newUser = new User { Name = userName };
                context.Users.Add(newUser);
                context.SaveChanges();

                return ToDto(newUser);
            }
        }

        public IEnumerable<UserDto> GetAll()
        {
            using (var context = new ИграDataContext())
            {
                var users = context.Users;
                return users.Select(ToDto).ToList();
            }
        }

        public UserDto GetByName(string userName)
        {
            using (var context = new ИграDataContext())
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

            return new UserDto { Id = u.Id, Name = u.Name };
        }
    }
}