using System;
using System.Collections.Generic;
using System.Linq;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Dtos;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entities;

namespace FifteenGame.DataAccess.EF.Repositories
{
    public class CheckersGameRepositoryEF : ICheckersGameRepository
    {
        public CheckersGameDto GetById(int gameId)
        {
            using (var ctx = new FifteenGameDataContext())
            {
                var game = ctx.CheckersGames.Include("Moves").FirstOrDefault(g => g.Id == gameId);
                if (game == null) return null;
                return MapGame(game);
            }
        }

        public IEnumerable<CheckersGameDto> GetByUserId(int userId)
        {
            using (var ctx = new FifteenGameDataContext())
            {
                var games = ctx.CheckersGames.Where(g => g.UserId == userId).ToList();
                return games.Select(g => MapGame(g)).ToList();   // <-- Явно указан тип
            }
        }

        public int Save(CheckersGameDto dto)
        {
            using (var ctx = new FifteenGameDataContext())
            {
                if (dto.Id == 0)
                {
                    var game = new CheckersGameEntity
                    {
                        UserId = dto.UserId,
                        CurrentPlayer = dto.CurrentPlayer,
                        IsFinished = dto.IsFinished,
                        Winner = dto.Winner,
                        GameStateJson = dto.GameStateJson,
                        StartDate = DateTime.UtcNow
                    };
                    ctx.CheckersGames.Add(game);
                    ctx.SaveChanges();
                    return game.Id;
                }
                else
                {
                    var game = ctx.CheckersGames.Find(dto.Id);
                    if (game != null)
                    {
                        game.CurrentPlayer = dto.CurrentPlayer;
                        game.IsFinished = dto.IsFinished;
                        game.Winner = dto.Winner;
                        game.GameStateJson = dto.GameStateJson;
                        ctx.SaveChanges();
                    }
                    return dto.Id;
                }
            }
        }

        public void Delete(int gameId)
        {
            using (var ctx = new FifteenGameDataContext())
            {
                var game = ctx.CheckersGames.Find(gameId);
                if (game != null)
                {
                    ctx.CheckersMoves.RemoveRange(ctx.CheckersMoves.Where(m => m.GameId == gameId));
                    ctx.CheckersGames.Remove(game);
                    ctx.SaveChanges();
                }
            }
        }

        public void AddMove(CheckersMoveDto move)
        {
            using (var ctx = new FifteenGameDataContext())
            {
                ctx.CheckersMoves.Add(new CheckersMoveEntity
                {
                    GameId = move.GameId,
                    MoveNumber = move.MoveNumber,
                    FromRow = move.FromRow,
                    FromCol = move.FromCol,
                    ToRow = move.ToRow,
                    ToCol = move.ToCol,
                    IsCapture = move.IsCapture,
                    CapturedRow = move.CapturedRow,
                    CapturedCol = move.CapturedCol,
                    PromotedToKing = move.PromotedToKing,
                    MoveTime = move.MoveTime
                });
                ctx.SaveChanges();
            }
        }

        private CheckersGameDto MapGame(CheckersGameEntity game)
        {
            return new CheckersGameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                StartDate = game.StartDate,
                CurrentPlayer = game.CurrentPlayer,
                IsFinished = game.IsFinished,
                Winner = game.Winner,
                GameStateJson = game.GameStateJson,
                Moves = game.Moves.Select(m => new CheckersMoveDto
                {
                    Id = m.Id,
                    GameId = m.GameId,
                    MoveNumber = m.MoveNumber,
                    FromRow = m.FromRow,
                    FromCol = m.FromCol,
                    ToRow = m.ToRow,
                    ToCol = m.ToCol,
                    IsCapture = m.IsCapture,
                    CapturedRow = m.CapturedRow,
                    CapturedCol = m.CapturedCol,
                    PromotedToKing = m.PromotedToKing,
                    MoveTime = m.MoveTime
                }).ToList()
            };
        }
    }
}