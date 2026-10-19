
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Dtos;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.DataAccess.EF.Repositories
{
    public class UserRepositoryEF : IUserRepository
    {
        public UserDto Create(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException(
                    "Введите имя пользователя.", "userName");
            }

            userName = userName.Trim();

            using (var context = new FifteenGameDataContext())
            {
                var existingUser = context.Users
                    .FirstOrDefault(u => u.Name == userName);

                if (existingUser != null)
                {
                    return ToDto(existingUser);
                }

                var newUser = new User { Name = userName };
                context.Users.Add(newUser);
                context.SaveChanges();

                return ToDto(newUser);
            }
        }

        public IEnumerable<UserDto> GetAll()
        {
            using (var context = new FifteenGameDataContext())
            {
                return context.Users
                    .AsNoTracking()
                    .OrderBy(u => u.Name)
                    .ToList()
                    .Select(ToDto)
                    .ToList();
            }
        }

        public UserDto GetById(int userId)
        {
            using (var context = new FifteenGameDataContext())
            {
                return ToDto(context.Users
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Id == userId));
            }
        }

        public UserDto GetByName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return null;
            }

            userName = userName.Trim();

            using (var context = new FifteenGameDataContext())
            {
                return ToDto(context.Users
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Name == userName));
            }
        }

        public void Delete(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException("userId");
            }

            using (var context = new FifteenGameDataContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                var user = context.Users
                    .FirstOrDefault(u => u.Id == userId);

                if (user == null)
                {
                    transaction.Commit();
                    return;
                }

                var games = context.Games
                    .Where(g => g.UserId == userId)
                    .ToList();

                var gameIds = games.Select(g => g.Id).ToList();

                var cells = context.Cells
                    .Where(c => gameIds.Contains(c.GameId))
                    .ToList();

                context.Cells.RemoveRange(cells);
                context.SaveChanges();

                context.Games.RemoveRange(games);
                context.SaveChanges();

                context.Users.Remove(user);
                context.SaveChanges();

                transaction.Commit();
            }
        }

        private UserDto ToDto(User user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name
            };
        }
    }
}
