using System.Collections.Generic;
using System.Linq;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Dtos;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entities;

namespace FifteenGame.DataAccess.EF.Repositories
{
    public class UserRepositoryEF : IUserRepository
    {
        public IEnumerable<UserDto> GetAll()
        {
            using (var ctx = new FifteenGameDataContext())
            {
                return ctx.Users.Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name
                }).ToList();
            }
        }

        public UserDto Create(string userName)
        {
            using (var ctx = new FifteenGameDataContext())
            {
                var user = new User { Name = userName };
                ctx.Users.Add(user);
                ctx.SaveChanges();
                return new UserDto { Id = user.Id, Name = user.Name };
            }
        }

        public UserDto GetById(int userId)
        {
            using (var ctx = new FifteenGameDataContext())
            {
                var user = ctx.Users.Find(userId);
                return user == null ? null : new UserDto { Id = user.Id, Name = user.Name };
            }
        }

        public UserDto GetByName(string userName)
        {
            using (var ctx = new FifteenGameDataContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.Name == userName);
                return user == null ? null : new UserDto { Id = user.Id, Name = user.Name };
            }
        }
    }
}