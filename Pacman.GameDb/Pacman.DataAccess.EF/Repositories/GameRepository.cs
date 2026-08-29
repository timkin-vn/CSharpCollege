using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Pacman.Common;
using Pacman.Common.Enums;
using Pacman.Common.Interfaces.Repositories;
using Pacman.Common.Models;
using Pacman.DataAccess.EF.Entities;

namespace Pacman.DataAccess.EF.Repositories
{
    public class GameRepository : IGameRepository
    {
        public GameStateDto CreateGame(int userId, int mapId)
        {
            using (var db = new PacmanDbContext())
            {
                var map = db.Maps.Include(m => m.Cells).FirstOrDefault(m => m.Id == mapId);
                if (map == null) throw new Exception("Map not found");

                var gameEntity = new GameEntity
                {
                    UserId = userId,
                    MapId = mapId,
                    Status = (int)GameStatus.InProgress,
                    Score = 0,
                    Lives = Constants.InitialLives,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                db.Games.Add(gameEntity);
                db.SaveChanges();

                // Initialize actors
                InitializeActors(db, gameEntity.Id, map.RowCount, map.ColCount);

                // Initialize collectible states
                InitializeCollectibles(db, gameEntity.Id, map.Cells);

                db.SaveChanges();

                return GetGameState(gameEntity.Id);
            }
        }

        private void InitializeActors(PacmanDbContext db, int gameId, int rows, int cols)
        {
            // Pacman start position
            db.GameActors.Add(new GameActorEntity
            {
                GameId = gameId,
                ActorType = (int)ActorType.Pacman,
                Row = rows - 2,
                Col = cols / 2,
                Direction = (int)Direction.None,
                FrightenedTicksLeft = 0
            });

            // Ghosts start positions
            var ghostStartRow = 1;
            db.GameActors.Add(new GameActorEntity
            {
                GameId = gameId,
                ActorType = (int)ActorType.GhostBlinky,
                Row = ghostStartRow,
                Col = cols / 2 - 1,
                Direction = (int)Direction.Down,
                FrightenedTicksLeft = 0
            });

            db.GameActors.Add(new GameActorEntity
            {
                GameId = gameId,
                ActorType = (int)ActorType.GhostPinky,
                Row = ghostStartRow,
                Col = cols / 2,
                Direction = (int)Direction.Down,
                FrightenedTicksLeft = 0
            });

            db.GameActors.Add(new GameActorEntity
            {
                GameId = gameId,
                ActorType = (int)ActorType.GhostInky,
                Row = ghostStartRow,
                Col = cols / 2 + 1,
                Direction = (int)Direction.Down,
                FrightenedTicksLeft = 0
            });

            db.GameActors.Add(new GameActorEntity
            {
                GameId = gameId,
                ActorType = (int)ActorType.GhostClyde,
                Row = ghostStartRow,
                Col = cols / 2 + 2,
                Direction = (int)Direction.Down,
                FrightenedTicksLeft = 0
            });
        }

        private void InitializeCollectibles(PacmanDbContext db, int gameId, ICollection<MapCellEntity> cells)
        {
            var collectibles = cells.Where(c => c.CellType == (int)CellType.Pellet || c.CellType == (int)CellType.PowerPellet);

            foreach (var cell in collectibles)
            {
                db.GameCollectibleStates.Add(new GameCollectibleStateEntity
                {
                    GameId = gameId,
                    Row = cell.Row,
                    Col = cell.Col,
                    CollectibleType = cell.CellType,
                    IsEaten = false
                });
            }
        }

        public GameStateDto GetGameState(int gameId)
        {
            using (var db = new PacmanDbContext())
            {
                var game = db.Games
                    .Include(g => g.Map.Cells)
                    .Include(g => g.Actors)
                    .Include(g => g.CollectibleStates)
                    .FirstOrDefault(g => g.Id == gameId);

                if (game == null) return null;

                return new GameStateDto
                {
                    Game = new GameDto
                    {
                        Id = game.Id,
                        UserId = game.UserId,
                        MapId = game.MapId,
                        Status = (GameStatus)game.Status,
                        Score = game.Score,
                        Lives = game.Lives,
                        CreatedAt = game.CreatedAt,
                        UpdatedAt = game.UpdatedAt
                    },
                    Map = new MapDto
                    {
                        Id = game.Map.Id,
                        Name = game.Map.Name,
                        RowCount = game.Map.RowCount,
                        ColCount = game.Map.ColCount,
                        Cells = game.Map.Cells.Select(c => new MapCellDto
                        {
                            Row = c.Row,
                            Col = c.Col,
                            CellType = (CellType)c.CellType
                        }).ToList()
                    },
                    Actors = game.Actors.Select(a => new GameActorDto
                    {
                        ActorType = (ActorType)a.ActorType,
                        Row = a.Row,
                        Col = a.Col,
                        Direction = (Direction)a.Direction,
                        FrightenedTicksLeft = a.FrightenedTicksLeft
                    }).ToList(),
                    CollectibleStates = game.CollectibleStates.Select(cs => new GameCollectibleStateDto
                    {
                        Row = cs.Row,
                        Col = cs.Col,
                        CollectibleType = (CellType)cs.CollectibleType,
                        IsEaten = cs.IsEaten
                    }).ToList()
                };
            }
        }

        public void UpdateGameState(GameStateDto gameState)
        {
            using (var db = new PacmanDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var game = db.Games.Find(gameState.Game.Id);
                        if (game == null) throw new Exception("Game not found");

                        game.Status = (int)gameState.Game.Status;
                        game.Score = gameState.Game.Score;
                        game.Lives = gameState.Game.Lives;
                        game.UpdatedAt = DateTime.UtcNow;

                        // Update actors
                        var actors = db.GameActors.Where(a => a.GameId == game.Id).ToList();
                        foreach (var actorDto in gameState.Actors)
                        {
                            var actor = actors.FirstOrDefault(a => a.ActorType == (int)actorDto.ActorType);
                            if (actor != null)
                            {
                                actor.Row = actorDto.Row;
                                actor.Col = actorDto.Col;
                                actor.Direction = (int)actorDto.Direction;
                                actor.FrightenedTicksLeft = actorDto.FrightenedTicksLeft;
                            }
                        }

                        // Update collectible states
                        var collectibles = db.GameCollectibleStates.Where(cs => cs.GameId == game.Id).ToList();
                        foreach (var csDto in gameState.CollectibleStates)
                        {
                            var cs = collectibles.FirstOrDefault(c => c.Row == csDto.Row && c.Col == csDto.Col);
                            if (cs != null)
                            {
                                cs.IsEaten = csDto.IsEaten;
                            }
                        }

                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void RemoveGame(int gameId)
        {
            using (var db = new PacmanDbContext())
            {
                var game = db.Games.Find(gameId);
                if (game != null)
                {
                    db.Games.Remove(game);
                    db.SaveChanges();
                }
            }
        }

        public IReadOnlyList<GameDto> GetGamesByUserId(int userId)
        {
            using (var db = new PacmanDbContext())
            {
                var games = db.Games
                    .Where(g => g.UserId == userId)
                    .OrderByDescending(g => g.UpdatedAt)
                    .ToList();

                return games.Select(g => new GameDto
                {
                    Id = g.Id,
                    UserId = g.UserId,
                    MapId = g.MapId,
                    Status = (GameStatus)g.Status,
                    Score = g.Score,
                    Lives = g.Lives,
                    CreatedAt = g.CreatedAt,
                    UpdatedAt = g.UpdatedAt
                }).ToList();
            }
        }
    }
}