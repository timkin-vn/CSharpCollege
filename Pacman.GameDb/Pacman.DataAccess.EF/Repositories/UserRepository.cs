using System;
using System.Linq;
using Pacman.Common.Interfaces.Repositories;
using Pacman.Common.Models;
using Pacman.DataAccess.EF.Entities;

namespace Pacman.DataAccess.EF.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserDto GetByName(string name)
        {
            using (var db = new PacmanDbContext())
            {
                var entity = db.Users.FirstOrDefault(u => u.Name == name);
                return entity == null ? null : MapToDto(entity);
            }
        }

        public UserDto GetById(int id)
        {
            using (var db = new PacmanDbContext())
            {
                var entity = db.Users.Find(id);
                return entity == null ? null : MapToDto(entity);
            }
        }

        public UserDto Create(string name)
        {
            using (var db = new PacmanDbContext())
            {
                var entity = new UserEntity
                {
                    Name = name,
                    CreatedAt = DateTime.UtcNow
                };
                db.Users.Add(entity);
                db.SaveChanges();
                return MapToDto(entity);
            }
        }

        private UserDto MapToDto(UserEntity entity)
        {
            return new UserDto
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}