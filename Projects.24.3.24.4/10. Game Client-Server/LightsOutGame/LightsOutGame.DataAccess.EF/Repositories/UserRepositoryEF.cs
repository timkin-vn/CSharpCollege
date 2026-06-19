using LightsOutGame.Common.Contracts.Repositories;
using LightsOutGame.Common.Dtos;
using LightsOutGame.DataAccess.EF.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LightsOutGame.DataAccess.EF.Repositories
{
    public class UserRepositoryEF : IUserRepository
    {
        public IEnumerable<UserDto> GetAll()
        {
            using (var context = new LightsOutGameContext())
            {
                return context.Users.ToList().Select(ToDto).ToList();
            }
        }

        public UserDto Create(string userName)
        {
            using (var context = new LightsOutGameContext())
            {
                var entity = new UserEntity { Name = userName };
                context.Users.Add(entity);
                context.SaveChanges();

                return ToDto(entity);
            }
        }

        public UserDto GetByName(string userName)
        {
            using (var context = new LightsOutGameContext())
            {
                var entity = context.Users.FirstOrDefault(u => u.Name == userName);
                return ToDto(entity);
            }
        }

        public UserDto GetById(int userId)
        {
            using (var context = new LightsOutGameContext())
            {
                var entity = context.Users.FirstOrDefault(u => u.Id == userId);
                return ToDto(entity);
            }
        }

        private UserDto ToDto(UserEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new UserDto { Id = entity.Id, Name = entity.Name, };
        }
    }
}
