using Игра.Common.Definitions;
using Игра.Common.Dtos;
using Игра.Common.Repositories;
using Игра.DataAccess.EF.DataContext;
using Игра.DataAccess.EF.Entites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Игра.DataAccess.EF.Repositories
{
    public class GameRepositoryEF : IGameRepository
    {
        public int Save(GameDto gameDto)
        {
            if (gameDto.Id == 0)
            {
                return Create(gameDto);
            }

            return Update(gameDto);
        }

        private int Create(GameDto gameDto)
        {
            using (var context = new ИграDataContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == gameDto.UserId);
                if (user == null)
                {
                    throw new Exception("Нет такого пользователя");
                }

                var newGame = new Game
                {
                    User = user,
                    MoveCount = gameDto.MoveCount,
                    GameCells = new List<GameCell>()
                };

                context.Games.Add(newGame);

                for (int r = 0; r < Constants.Size; r++)
                {
                    for (int c = 0; c < Constants.Size; c++)
                    {
                        if (gameDto.Cells[r, c] == 1)
                        {
                            newGame.GameCells.Add(new GameCell
                            {
                                Row = r,
                                Column = c,
                                Value = 1
                            });
                        }
                    }
                }

                context.SaveChanges();
                return newGame.Id;
            }
        }

        private int Update(GameDto gameDto)
        {
            using (var context = new ИграDataContext())
            {
                var game = context.Games.Include("GameCells").FirstOrDefault(g => g.Id == gameDto.Id);
                if (game == null)
                {
                    throw new Exception("Нет такой игры");
                }

                game.MoveCount = gameDto.MoveCount;
                var oldCells = game.GameCells.ToList();
                foreach (var oc in oldCells)
                {
                    context.GameCells.Remove(oc);
                }
                context.SaveChanges();

                game.GameCells = new List<GameCell>();
                for (int r = 0; r < Constants.Size; r++)
                {
                    for (int c = 0; c < Constants.Size; c++)
                    {
                        if (gameDto.Cells[r, c] == 1)
                        {
                            game.GameCells.Add(new GameCell
                            {
                                Row = r,
                                Column = c,
                                Value = 1
                            });
                        }
                    }
                }

                context.SaveChanges();
                return gameDto.Id;
            }
        }

        public GameDto GetByGameId(int gameId)
        {
            using (var context = new ИграDataContext())
            {
                var game = context.Games.Include("GameCells").FirstOrDefault(g => g.Id == gameId);
                return ToDto(game);
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            using (var context = new ИграDataContext())
            {
                var games = context.Games.Include("GameCells").Where(g => g.UserId == userId);
                return games.Select(ToDto).ToList();
            }
        }

        public void Remove(int gameId)
        {
            using (var context = new ИграDataContext())
            {
                var game = context.Games.Include("GameCells").FirstOrDefault(g => g.Id == gameId);
                if (game == null)
                {
                    return;
                }

                context.Games.Remove(game);
                context.SaveChanges();
            }
        }

        private GameDto ToDto(Game game)
        {
            if (game == null)
            {
                return null;
            }

            var result = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                MoveCount = game.MoveCount
            };

            for (int r = 0; r < Constants.Size; r++)
            {
                for (int c = 0; c < Constants.Size; c++)
                {
                    result.Cells[r, c] = 0;
                }
            }

            foreach (var cell in game.GameCells)
            {
                result.Cells[cell.Row, cell.Column] = cell.Value;
            }

            return result;
        }
    }
}