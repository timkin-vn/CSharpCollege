using Blackjack.Common.Contracts.Repositories;
using Blackjack.Common.Models;
using Blackjack.DataAccess.DataContext;
using Blackjack.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Blackjack.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public UserModel GetByName(string name)
        {
            using (var db = new BlackjackDbContext(_connectionString))
            {
                var entity = db.Users.AsNoTracking().FirstOrDefault(u => u.Name == name);
                return entity == null ? null : ToModel(entity);
            }
        }

        public UserModel Create(string name)
        {
            using (var db = new BlackjackDbContext(_connectionString))
            {
                var entity = new User { Name = name };
                db.Users.Add(entity);
                db.SaveChanges();
                return ToModel(entity);
            }
        }

        private static UserModel ToModel(User entity)
        {
            return new UserModel
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}
