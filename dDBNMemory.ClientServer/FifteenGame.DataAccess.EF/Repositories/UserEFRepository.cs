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
    public class UserEFRepository : IUserRepository
    {
        public UserDto Create(string userName)
        {
            using (var db = new FifteenGameDbContext())
            {
                var newUser = new User { Name = userName };
                db.Users.Add(newUser);

                db.SaveChanges();

                return ToDto(db.Users.First(u => u.Id == newUser.Id));
            }
        }

        public IEnumerable<UserDto> GetAll()
        {
            using (var db = new FifteenGameDbContext())
            {
                return db.Users.Select(ToDto).ToList();
            }
        }

        public UserDto GetByName(string userName)
        {
            using (var db = new FifteenGameDbContext())
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
