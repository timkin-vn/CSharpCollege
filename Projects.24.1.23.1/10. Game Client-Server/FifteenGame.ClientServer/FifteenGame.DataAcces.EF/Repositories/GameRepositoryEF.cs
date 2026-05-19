using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dto;
using FifteenGame.Common.Repositories;
using FifteenGame.DataAcces.EF.DataContext;
using FifteenGame.DataAcces.EF.Entites;
using FifteenGame.DataAcces.EF.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAcces.EF.Repositories
{
    public class GameRepositoryEF : IGameRepository
    {
        public GameDto GetByGameId(int gameId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games.Include("GameCells").FirstOrDefault(g => g.Id == gameId);
                if (game == null)
                {
                    return null;
                }

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

        public GameDto ToDto(Game game)
        {
            var result = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                MoveCount = game.MoveCount,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result.Cells[row, column] = Constants.FreeCellValue;
                }
            }

            foreach (var cell in game.GameCells)
            {
                result.Cells[cell.Row, cell.Column] = cell.Value;
            }

            return result;
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
                        if (gameDto.Cells[row, column] == Constants.FreeCellValue)
                        {
                            continue;
                        }

                        newGame.GameCells.Add(new GameCell
                        {
                            Row = row,
                            Column = column,
                            Value = gameDto.Cells[row, column],
                        });
                    }
                }

                context.Games.Add(newGame);
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
                context.SaveChanges();

                int row = -1;
                int column = -1;
                GameCell selectedCell = null;

                for (row = 0; row < Constants.RowCount; row++)
                {
                    for (column = 0; column < Constants.ColumnCount; column++)
                    {
                        if (gameDto.Cells[row, column] == Constants.FreeCellValue)
                        {
                            continue;
                        }

                        selectedCell = game.GameCells.First(c => c.Value == gameDto.Cells[row, column]);
                        if (selectedCell.Row != row || selectedCell.Column != column)
                        {
                            goto cellFound;
                        }
                    }
                }

            cellFound:
                selectedCell.Row = row;
                selectedCell.Column = column;
                context.SaveChanges();

                return gameDto.Id;
            }
        }
    }
}
