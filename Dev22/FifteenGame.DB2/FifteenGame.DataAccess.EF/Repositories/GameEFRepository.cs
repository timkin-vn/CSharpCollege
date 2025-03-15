using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.EF.Repositories
{
    public class GameEFRepository : IGameRepository
    {
        public GameDto GetByGameId(int gameId)
        {
            using (var context = new MineSweepDataContext())
            {
                var game = context.Games.Include("Cells").FirstOrDefault(g => g.Id == gameId);
                if (game == null)
                {
                    return null;
                }

                return ToDto(game);
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            using (var context = new MineSweepDataContext())
            {
                var games = context.Games.Include("Cells").Where(g => g.UserId == userId);
                return games.Select(ToDto).ToList();
            }
        }

        public void Remove(int gameId)
        {
            using (var context = new MineSweepDataContext())
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
            using (var context = new MineSweepDataContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == gameDto.UserId);
                if (user == null)
                {
                    throw new Exception("Нет такого пользователя");
                }

                var newGame = new Game
                {
                    User = user,
                    GameStart = gameDto.GameBegin,
                    MoveCount = gameDto.MoveCount,
                    Cells = new List<Cell>(),
                };

                context.Games.Add(newGame);

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        var cellModel = gameDto.Cells[row, column]; // Предполагается, что gameDto.Cells - это массив CellsModel

                        var cell = new Cell
                        {
                            Row = row,
                            Column = column,
                            Value = new CellsModel
                            {
                                IsMine = cellModel.IsMine,
                                NeightborMines = cellModel.NeightborMines,
                                IsRevealed = cellModel.IsRevealed,
                                Isflag = cellModel.Isflag
                            }
                        };

                        newGame.Cells.Add(cell);
                    }
                }

                context.SaveChanges();

                return newGame.Id;
            }
        }

        private int Update(GameDto gameDto)
        {
            using (var context = new MineSweepDataContext())
            {
                var game = context.Games.Include("Cells").FirstOrDefault(g => g.Id == gameDto.Id);
                if (game == null)
                {
                    throw new Exception("Нет такой игры");
                }

                game.MoveCount = gameDto.MoveCount;
                var gameCells = game.Cells.ToArray();
                foreach (var cell in gameCells)
                {
                    context.Cells.Remove(cell);
                }

                context.SaveChanges();

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        var cellModel = gameDto.Cells[row, column]; // Предполагается, что gameDto.Cells - это массив CellsModel

                        var cell = new Cell
                        {
                            Row = row,
                            Column = column,
                            Value = new CellsModel
                            {
                                IsMine = cellModel.IsMine,
                                NeightborMines = cellModel.NeightborMines,
                                IsRevealed = cellModel.IsRevealed,
                                Isflag = cellModel.Isflag
                            }
                        };

                        game.Cells.Add(cell);
                    }
                }

                context.SaveChanges();

                return game.Id;
            }
        }

        public GameDto ToDto(Game game)
        {
            var result = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                GameBegin = game.GameStart,
                MoveCount = game.MoveCount,
                Cells = new CellsModel[Constants.RowCount, Constants.ColumnCount] // Предполагается, что Cells - это двумерный массив CellsModel
            };

            // Инициализируем все ячейки как свободные
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result.Cells[row, column] = new CellsModel(); // Создаем новую ячейку
                }
            }

            foreach (var cell in game.Cells)
            {
                result.Cells[cell.Row, cell.Column] = new CellsModel
                {
                    IsMine = cell.Value.IsMine,
                    NeightborMines = cell.Value.NeightborMines,
                    IsRevealed = cell.Value.IsRevealed,
                    Isflag = cell.Value.Isflag
                };
            }

            return result;
        }
    }
}
