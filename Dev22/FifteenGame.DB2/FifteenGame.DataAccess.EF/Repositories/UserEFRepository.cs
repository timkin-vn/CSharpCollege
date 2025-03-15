using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.EF.Repositories
{
    public class UserEFRepository : IUserRepository
    {
        public UserDto Create(string userName)
        {
            using (var context = new MineSweepDataContext())
            {
                var newUser = new User { Name = userName };
                context.Users.Add(newUser);
                context.SaveChanges();
            }

            return GetByName(userName);
        }

        public IEnumerable<UserDto> GetAll()
        {
            using (var context = new MineSweepDataContext())
            {
                var users = context.Users.ToList();
                return users.Select(ToDto).ToList();
            }
        }

        public UserDto GetByName(string userName)
        {
            using (var context = new MineSweepDataContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Name == userName);
                if (user == null)
                {
                    return null;
                }

                return ToDto(user);
            }
        }

        private UserDto ToDto(User u)
        {
            return new UserDto { Id = u.Id, Name = u.Name, };
        }
    }
}
