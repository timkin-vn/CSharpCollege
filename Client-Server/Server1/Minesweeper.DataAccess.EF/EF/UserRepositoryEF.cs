using Minesweeper.Common.Dto;
using Minesweeper.Common.Repositories;
using Minesweeper.DataAccess.EF.Data;
using Minesweeper.DataAccess.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper.DataAccess.EF.Repositories
{
    public class UserRepositoryEF : IUserRepository
    {
        public UserDto GetById(int userId)
        {
            using (var context = new MinesweeperDbContext())
            {
                var entity = context.Users.Find(userId);
                return MapEntityToDto(entity);
            }
        }

        public UserDto GetByUsername(string username)
        {
            using (var context = new MinesweeperDbContext())
            {
                var entity = context.Users
                    .FirstOrDefault(u => u.Username.ToLower() == username.ToLower());
                return MapEntityToDto(entity);
            }
        }

        public UserDto Create(UserDto userDto)
        {
            using (var context = new MinesweeperDbContext())
            {
                var entity = new UserEntity
                {
                    Username = userDto.Username,
                    CreatedAt = userDto.CreatedAt,
                    TotalGamesPlayed = userDto.TotalGamesPlayed,
                    GamesWon = userDto.GamesWon,
                    LastPlayedAt = userDto.LastPlayedAt
                };

                context.Users.Add(entity);
                context.SaveChanges();

                return MapEntityToDto(entity);
            }
        }

        public UserDto Update(UserDto userDto)
        {
            using (var context = new MinesweeperDbContext())
            {
                var entity = context.Users.Find(userDto.Id);
                if (entity == null)
                    return null;

                entity.Username = userDto.Username;
                entity.TotalGamesPlayed = userDto.TotalGamesPlayed;
                entity.GamesWon = userDto.GamesWon;
                entity.LastPlayedAt = userDto.LastPlayedAt;

                context.SaveChanges();
                return MapEntityToDto(entity);
            }
        }

        public IEnumerable<UserDto> GetAll()
        {
            using (var context = new MinesweeperDbContext())
            {
                return context.Users
                    .OrderBy(u => u.Username)
                    .ToList()  
                    .Select(MapEntityToDto)  
                    .ToList();
            }
        }

        public IEnumerable<UserDto> GetTopPlayers(int limit = 10)
        {
            using (var context = new MinesweeperDbContext())
            {
                return context.Users
                    .Where(u => u.TotalGamesPlayed > 0)
                    .OrderByDescending(u => u.GamesWon * 100.0 / u.TotalGamesPlayed)
                    .ThenByDescending(u => u.GamesWon)
                    .Take(limit)
                    .ToList()  
                    .Select(MapEntityToDto)
                    .ToList();
            }
        }

        public void UpdateStats(int userId, bool gameWon)
        {
            using (var context = new MinesweeperDbContext())
            {
                var entity = context.Users.Find(userId);
                if (entity == null)
                    return;

                entity.TotalGamesPlayed++;
                if (gameWon) entity.GamesWon++;
                entity.LastPlayedAt = DateTime.Now;

                context.SaveChanges();
            }
        }

        public void UpdateLastPlayed(int userId)
        {
            using (var context = new MinesweeperDbContext())
            {
                var entity = context.Users.Find(userId);
                if (entity == null)
                    return;

                entity.LastPlayedAt = DateTime.Now;
                context.SaveChanges();
            }
        }
        private UserDto MapEntityToDto(UserEntity entity)
        {
            if (entity == null) return null;

            return new UserDto
            {
                Id = entity.Id,
                Username = entity.Username,
                CreatedAt = entity.CreatedAt,
                TotalGamesPlayed = entity.TotalGamesPlayed,
                GamesWon = entity.GamesWon,
                LastPlayedAt = entity.LastPlayedAt
            };
        }
    }
}