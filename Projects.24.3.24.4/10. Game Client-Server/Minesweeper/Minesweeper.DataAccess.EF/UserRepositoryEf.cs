using System.Linq;
using Minesweeper.Common.Interfaces;
using Minesweeper.Common.Models;
using Minesweeper.DataAccess.EF.Entities;

namespace Minesweeper.DataAccess.EF
{
    public class UserRepositoryEf : IUserRepository
    {
        public User GetById(int id)
        {
            using (var ctx = new GameDbContext())
            {
                var e = ctx.Users.FirstOrDefault(u => u.Id == id);
                return e == null ? null : new User { Id = e.Id, Name = e.Name };
            }
        }

        public User GetByName(string name)
        {
            using (var ctx = new GameDbContext())
            {
                var e = ctx.Users.FirstOrDefault(u => u.Name == name);
                return e == null ? null : new User { Id = e.Id, Name = e.Name };
            }
        }

        public User Create(string name)
        {
            using (var ctx = new GameDbContext())
            {
                var e = new UserEntity { Name = name };
                ctx.Users.Add(e);
                ctx.SaveChanges();
                return new User { Id = e.Id, Name = e.Name };
            }
        }
    }
}
