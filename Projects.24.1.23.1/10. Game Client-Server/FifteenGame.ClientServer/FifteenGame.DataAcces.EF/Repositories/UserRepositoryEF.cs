using FifteenGame.Common.Dto;
using FifteenGame.Common.Repositories;
using FifteenGame.DataAcces.EF.DataContext;
using FifteenGame.DataAcces.EF.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAcces.EF.Repositories
{
    public class UserRepositoryEF : IUserRepository
    {
        public UserDto Create(string userName)
        {
            using (var context = new FifteenGameDataContext())
            {
                var newUser = new User { Name = userName };
                context.Users.Add(newUser);
                context.SaveChanges();

                return ToDto(context.Users.First(u => u.Id == newUser.Id));
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

        public UserDto GetById(int userId)
        {
            using (var context = new FifteenGameDataContext())
            {
                return ToDto(context.Users.FirstOrDefault(u => u.Id == userId));
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

            return new UserDto { Id = u.Id, Name = u.Name };
        }
    }
}
