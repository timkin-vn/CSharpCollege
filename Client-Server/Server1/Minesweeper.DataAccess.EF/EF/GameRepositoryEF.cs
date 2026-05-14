using Minesweeper.Common.Dto;
using Minesweeper.Common.Repositories;
using Minesweeper.DataAccess.EF.Data;
using Minesweeper.DataAccess.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper.DataAccess.EF.Repositories
{
    public class GameRepositoryEF : IGameRepository
    {
        public GameStateDto GetById(int gameId)
        {
            using (var context = new MinesweeperDbContext())
            {
                var entity = context.GameStates.Find(gameId);
                return MapEntityToDto(entity);
            }
        }

        public IEnumerable<GameStateDto> GetByUserId(int userId)
        {
            using (var context = new MinesweeperDbContext())
            {
                return context.GameStates
                    .Where(g => g.UserId == userId)
                    .OrderByDescending(g => g.CreatedAt)
                    .ToList()
                    .Select(MapEntityToDto)
                    .ToList();
            }
        }

        public GameStateDto Create(GameStateDto gameDto)
        {
            using (var context = new MinesweeperDbContext())
            {
                var entity = new GameStateEntity
                {
                    UserId = gameDto.UserId,
                    GameData = gameDto.GameData,
                    IsGameOver = gameDto.IsGameOver,
                    IsGameWon = gameDto.IsGameWon,
                    PlayTime = gameDto.PlayTime,
                    CreatedAt = gameDto.CreatedAt,
                    UpdatedAt = gameDto.UpdatedAt ?? DateTime.Now,
                    FieldSize = 10,
                    MineCount = 15,
                    FlagsPlaced = 0,
                    CellsRevealed = 0
                };

                context.GameStates.Add(entity);
                context.SaveChanges();

                return MapEntityToDto(entity);
            }
        }

        public GameStateDto Update(GameStateDto gameDto)
        {
            using (var context = new MinesweeperDbContext())
            {
                var entity = context.GameStates.Find(gameDto.Id);
                if (entity == null)
                    return null;

                entity.GameData = gameDto.GameData;
                entity.IsGameOver = gameDto.IsGameOver;
                entity.IsGameWon = gameDto.IsGameWon;
                entity.PlayTime = gameDto.PlayTime;
                entity.UpdatedAt = DateTime.Now;

                context.SaveChanges();
                return MapEntityToDto(entity);
            }
        }

        public void Delete(int gameId)
        {
            using (var context = new MinesweeperDbContext())
            {
                var entity = context.GameStates.Find(gameId);
                if (entity == null)
                    return;

                context.GameStates.Remove(entity);
                context.SaveChanges();
            }
        }

        public GameStateDto GetLastActiveGame(int userId)
        {
            using (var context = new MinesweeperDbContext())
            {
                var entity = context.GameStates
                    .Where(g => g.UserId == userId && !g.IsGameOver && !g.IsGameWon)
                    .OrderByDescending(g => g.CreatedAt)
                    .FirstOrDefault();

                return MapEntityToDto(entity);
            }
        }

        public IEnumerable<GameStateDto> GetCompletedGames(int userId, int limit = 10)
        {
            using (var context = new MinesweeperDbContext())
            {
                return context.GameStates
                    .Where(g => g.UserId == userId && (g.IsGameOver || g.IsGameWon))
                    .OrderByDescending(g => g.CreatedAt)
                    .Take(limit)
                    .ToList()
                    .Select(MapEntityToDto)
                    .ToList();
            }
        }

        public int GetActiveGamesCount(int userId)
        {
            using (var context = new MinesweeperDbContext())
            {
                return context.GameStates
                    .Count(g => g.UserId == userId && !g.IsGameOver && !g.IsGameWon);
            }
        }

        public void UpdateGameStats(int gameId, TimeSpan playTime, bool isGameOver, bool isGameWon)
        {
            using (var context = new MinesweeperDbContext())
            {
                var entity = context.GameStates.Find(gameId);
                if (entity == null)
                    return;

                entity.PlayTime = playTime;
                entity.IsGameOver = isGameOver;
                entity.IsGameWon = isGameWon;
                entity.UpdatedAt = DateTime.Now;

                context.SaveChanges();
            }
        }

        private GameStateDto MapEntityToDto(GameStateEntity entity)
        {
            if (entity == null) return null;

            return new GameStateDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                GameData = entity.GameData,
                IsGameOver = entity.IsGameOver,
                IsGameWon = entity.IsGameWon,
                PlayTime = entity.PlayTime,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}