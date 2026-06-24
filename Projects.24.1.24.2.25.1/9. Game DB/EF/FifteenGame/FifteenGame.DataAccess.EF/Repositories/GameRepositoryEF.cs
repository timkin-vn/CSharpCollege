using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entities;
using System;
using System.Collections.Generic;
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
                var game = context.Games.Include("Cells").FirstOrDefault(g => g.Id == gameId);
                return ToDto(game);
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var games = context.Games.Include("Cells").Where(g => g.UserId == userId);
                return games.Select(ToDto).ToList();
            }
        }

        public void Remove(int gameId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games.Include("Cells").FirstOrDefault(g => g.Id == gameId);
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
                    Cells = new List<Cell>(),
                };

                context.Games.Add(newGame);

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int col = 0; col < Constants.ColumnCount; col++)
                    {
                        if (gameDto.Cells[row, col] == 0) continue;

                        newGame.Cells.Add(new Cell
                        {
                            Row = row,
                            Column = col,
                            Value = gameDto.Cells[row, col],
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
                var game = context.Games.Include("Cells").FirstOrDefault(g => g.Id == gameDto.Id);
                if (game == null)
                {
                    throw new Exception("Нет такой игры");
                }

                game.MoveCount = gameDto.MoveCount;
                game.Score = gameDto.Score;
                game.IsWin = gameDto.IsWin;
                context.SaveChanges();

                var cellsToRemove = context.Cells.Where(c => c.GameId == gameDto.Id);
                context.Cells.RemoveRange(cellsToRemove);
                context.SaveChanges();

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int col = 0; col < Constants.ColumnCount; col++)
                    {
                        if (gameDto.Cells[row, col] == 0) continue;
  

                        context.Cells.Add(new Cell
                        {
                            GameId = gameDto.Id,
                            Row = row,
                            Column = col,
                            Value = gameDto.Cells[row, col],
                        });
                    }
                }

                context.SaveChanges();
                return gameDto.Id;
            }
        }

        private GameDto ToDto(Game game)
        {
            if (game == null) return null;

            var result = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                Score = game.Score,
                MoveCount = game.MoveCount,
                IsWin = game.IsWin,
            };

            // Инициализируем клетки нулями (а не -1)
            for (int row = 0; row < Constants.RowCount; row++)
                for (int col = 0; col < Constants.ColumnCount; col++)
                    result.Cells[row, col] = 0;

            foreach (var cell in game.Cells)
                result.Cells[cell.Row, cell.Column] = cell.Value;

            return result;
        }
    }
}
