using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048.Common.Dtos;
using Game2048.Common.Repositories;
using Game2048.DataAccess.EF.DataContext;
using Game2048.DataAccess.EF.Entities;

namespace Game2048.DataAccess.EF.Repositories
{
    public class UserEFRepository : IUserRepository
    {
        public UserDto Create(string userName)
        {
            using (var db = new Game2048DBContext())
            {
                var newUser = new User { Name = userName };
                db.Users.Add(newUser);

                db.SaveChanges();

                return ToDto(db.Users.First(u => u.Id == newUser.Id));
            }
        }

        public IEnumerable<UserDto> GetAll()
        {
            using (var db = new Game2048DBContext())
            {
                return db.Users.Select(ToDto).ToList();
            }
        }

        public UserDto GetByName(string userName)
        {
            using (var db = new Game2048DBContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Name == userName);
                if (user == null)
                {
                    return null;
                }

                return ToDto(user);
            }
        }

        private UserDto ToDto(User user)
        {
            return new UserDto { Id = user.Id, Name = user.Name };
        }
    }
}
