using System.Linq;
using Game2048.Common.Interfaces;
using Game2048.Common.Models;
using Game2048.DataAccess.EF.Entities;

namespace Game2048.DataAccess.EF
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
