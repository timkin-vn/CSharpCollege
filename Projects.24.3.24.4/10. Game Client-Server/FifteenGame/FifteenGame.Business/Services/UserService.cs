using FifteenGame.Common.Contracts;
using FifteenGame.Common.Models;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entities;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class UserService : IUserService
    {
        public UserModel GetOrCreateUser(string name)
        {
            using (var context = new GameContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Name == name);
                if (user == null)
                {
                    user = new UserEntity { Name = name };
                    context.Users.Add(user);
                    context.SaveChanges();
                }

                return new UserModel { Id = user.Id, Name = user.Name };
            }
        }
    }
}