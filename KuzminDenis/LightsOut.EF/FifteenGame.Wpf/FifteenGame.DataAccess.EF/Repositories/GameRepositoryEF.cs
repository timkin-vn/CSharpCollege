using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.EF.Repositories
{
    public class GameRepositoryEF : IGameRepository
    {
        public GameDto GetByGameId(int gameId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games.Include("GameCells").FirstOrDefault(g => g.Id == gameId);
                return ToDto(game);
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var games = context.Games.Include("GameCells").Where(g => g.UserId == userId);
                return games.Select(ToDto).ToList();
            }
        }

        public void Remove(int gameId)
        {
            using (var context = new FifteenGameDataContext())
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
            using (var context = new FifteenGameDataContext())
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
                    GameCells = new List<GameCell>(),
                };
                context.Games.Add(newGame);

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        newGame.GameCells.Add(new GameCell
                        {
                            Row = row,
                            Column = column,
                            Value = gameDto.Cells[row, column],
                        });
                    }
                }
                context.SaveChanges();
                return newGame.Id;
            }
        }

        private int Update(GameDto gameDto)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games.Include("GameCells").FirstOrDefault(g => g.Id == gameDto.Id);
                if (game == null)
                {
                    throw new Exception("Нет такой игры");
                }

                game.MoveCount = gameDto.MoveCount;

                context.GameCells.RemoveRange(game.GameCells);

                var newGameCells = new List<GameCell>();
                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        newGameCells.Add(new GameCell
                        {
                            GameId = game.Id,
                            Row = row,
                            Column = column,
                            Value = gameDto.Cells[row, column],
                        });
                    }
                }
                context.GameCells.AddRange(newGameCells);

                context.SaveChanges();
                return gameDto.Id;
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
                MoveCount = game.MoveCount,
            };

            foreach (var cell in game.GameCells)
            {
                if (cell.Row >= 0 && cell.Row < Constants.RowCount && cell.Column >= 0 && cell.Column < Constants.ColumnCount)
                {
                    result.Cells[cell.Row, cell.Column] = cell.Value;
                }
            }
            return result;
        }
    }
}