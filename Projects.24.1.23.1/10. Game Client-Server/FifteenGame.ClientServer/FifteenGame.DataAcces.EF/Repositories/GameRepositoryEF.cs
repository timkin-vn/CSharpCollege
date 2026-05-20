using FifteenGame.Common.Dto;
using FifteenGame.Common.Repositories;
using FifteenGame.DataAcces.EF.DataContext;
using FifteenGame.DataAcces.EF.Entites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.DataAcces.EF.Repositories
{
    public class GameRepositoryEF : IGameRepository
    {
        public GameDto GetByGameId(int gameId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games.Include("GameCells").FirstOrDefault(g => g.Id == gameId);
                return game == null ? null : ToDto(game);
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            using (var context = new FifteenGameDataContext())
            {
                return context.Games.Include("GameCells")
                    .Where(g => g.UserId == userId)
                    .Select(ToDto).ToList();
            }
        }

        public void Remove(int gameId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games.Include("GameCells").FirstOrDefault(g => g.Id == gameId);
                if (game == null) return;
                context.Games.Remove(game);
                context.SaveChanges();
            }
        }

        public int Save(GameDto gameDto)
        {
            return gameDto.Id == 0 ? Create(gameDto) : Update(gameDto);
        }

        private GameDto ToDto(Game game)
        {
            var result = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                MatchesCount = game.MatchesCount,
                IsFinished = game.IsFinished,
            };

            foreach (var cell in game.GameCells)
                result.Cells[cell.Row, cell.Column] = cell.Value;

            return result;
        }

        private int Create(GameDto gameDto)
        {
            using (var context = new FifteenGameDataContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == gameDto.UserId)
                    ?? throw new Exception("Нет такого пользователя");

                var newGame = new Game
                {
                    User = user,
                    MatchesCount = gameDto.MatchesCount,
                    IsFinished = gameDto.IsFinished,
                    GameCells = new List<GameCell>(),
                };

                for (int row = 0; row < 8; row++)
                    for (int col = 0; col < 8; col++)
                        newGame.GameCells.Add(new GameCell
                        {
                            Row = row,
                            Column = col,
                            Value = gameDto.Cells[row, col]
                        });

                context.Games.Add(newGame);
                context.SaveChanges();
                return newGame.Id;
            }
        }

        private int Update(GameDto gameDto)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games.Include("GameCells").FirstOrDefault(g => g.Id == gameDto.Id)
                    ?? throw new Exception("Нет такой игры");

                game.MatchesCount = gameDto.MatchesCount;
                game.IsFinished = gameDto.IsFinished;

                foreach (var cell in game.GameCells)
                    cell.Value = gameDto.Cells[cell.Row, cell.Column];

                context.SaveChanges();
                return gameDto.Id;
            }
        }
    }
}