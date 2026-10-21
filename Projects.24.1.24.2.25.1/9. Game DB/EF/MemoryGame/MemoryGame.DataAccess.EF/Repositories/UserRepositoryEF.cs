using MemoryGame.Common.Contracts.Repositories;
using MemoryGame.Common.Dtos;
using MemoryGame.DataAccess.EF.DataContext;
using MemoryGame.DataAccess.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.DataAccess.EF.Repositories
{
    public class UserRepositoryEF : IUserRepository
    {
        public UserDto Create(string userName)
        {
            using (var context = new MemoryGameDataContext())
            {
                var newUser = new User { Name = userName, };
                context.Users.Add(newUser);
                context.SaveChanges();

                return ToDto(newUser);
            }
        }

        public IEnumerable<UserDto> GetAll()
        {
            using (var context = new MemoryGameDataContext())
            {
                var users = context.Users;
                return users.Select(ToDto).ToList();
            }
        }

        public UserDto GetById(int userId)
        {
            using (var context = new MemoryGameDataContext())
            {
                return ToDto(context.Users.FirstOrDefault(u => u.Id == userId));
            }
        }

        public UserDto GetByName(string userName)
        {
            using (var context = new MemoryGameDataContext())
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

            return new UserDto
            {
                Id = u.Id,
                Name = u.Name,
            };
        }
    }
}
