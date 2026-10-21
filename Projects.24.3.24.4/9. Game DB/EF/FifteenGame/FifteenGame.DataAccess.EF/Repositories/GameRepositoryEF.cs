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
        private const int MetaRow = 10; 

        public GameDto GetByGameId(int gameId)
        {
            using (var context = new FifteenGameDataContext())
            {
                return ToDto(context.Games.Include("Cells").FirstOrDefault(g => g.Id == gameId));
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var games = context.Games.Include("Cells").Where(g => g.UserId == userId).ToList();

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
                    Cells = new List<Cell> (),
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

                        newGame.Cells.Add(new Cell
                        {
                            Row = row,
                            Column = column,
                            Value = gameDto.Cells[row, column],
                        });
                    }
                }

                newGame.Cells.Add(new Cell { Row = MetaRow, Column = 0, Value = gameDto.Difficulty });

                newGame.Cells.Add(new Cell { Row = MetaRow, Column = 1, Value = gameDto.Theme });

                newGame.Cells.Add(new Cell { Row = MetaRow, Column = 2, Value = gameDto.Score });

                newGame.Cells.Add(new Cell { Row = MetaRow, Column = 3, Value = gameDto.ComboStreak });

                newGame.Cells.Add(new Cell { Row = MetaRow, Column = 4, Value = gameDto.HintsLeft });

                newGame.Cells.Add(new Cell { Row = MetaRow, Column = 5, Value = gameDto.ElapsedSeconds });

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

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        int newVal = gameDto.Cells[row, column];

                        var existingCell = game.Cells.FirstOrDefault(c => c.Row == row && c.Column == column);

                        if (newVal == Constants.FreeCellValue)
                        {
                            if (existingCell != null)
                            {
                                context.Cells.Remove(existingCell);
                            }
                        }

                        else

                        {
                            if (existingCell != null)
                            {
                                existingCell.Value = newVal;
                            }

                            else

                            {
                                game.Cells.Add(new Cell
                                {
                                    Row = row,
                                    Column = column,
                                    Value = newVal
                                });
                            }
                        }
                    }
                }

                SetMetaCell(game, 0, gameDto.Difficulty);

                SetMetaCell(game, 1, gameDto.Theme);

                SetMetaCell(game, 2, gameDto.Score);

                SetMetaCell(game, 3, gameDto.ComboStreak);

                SetMetaCell(game, 4, gameDto.HintsLeft);

                SetMetaCell(game, 5, gameDto.ElapsedSeconds);

                context.SaveChanges();

                return gameDto.Id;
            }
        }

        private void SetMetaCell(Game game, int col, int val)
        {
            var cell = game.Cells.FirstOrDefault(c => c.Row == MetaRow && c.Column == col);

            if (cell != null)
            {
                cell.Value = val;
            }

            else

            {
                game.Cells.Add(new Cell { Row = MetaRow, Column = col, Value = val });
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
                Difficulty = 1,
                Theme = 1,
                HintsLeft = Constants.DefaultHintsCount
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result.Cells[row, column] = Constants.FreeCellValue;
                }
            }

            foreach (var cell in game.Cells)
            {
                if (cell.Row >= 0 && cell.Row < Constants.RowCount && cell.Column >= 0 && cell.Column < Constants.ColumnCount)
                {
                    result.Cells[cell.Row, cell.Column] = cell.Value;
                }

                else if (cell.Row == MetaRow)

                {
                    switch (cell.Column)
                    {
                        case 0: result.Difficulty = cell.Value > 0 ? cell.Value : 1; break;

                        case 1: result.Theme = cell.Value > 0 ? cell.Value : 1; break;

                        case 2: result.Score = cell.Value; break;

                        case 3: result.ComboStreak = cell.Value; break;

                        case 4: result.HintsLeft = cell.Value; break;

                        case 5: result.ElapsedSeconds = cell.Value; break;
                    }
                }
            }

            return result;
        }
    }
}
