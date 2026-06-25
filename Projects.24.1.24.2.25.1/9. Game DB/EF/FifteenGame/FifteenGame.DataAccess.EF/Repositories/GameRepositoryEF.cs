using System.Collections.Generic;
using System.Linq;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.DataAccess.EF.DataContext;

namespace FifteenGame.DataAccess.EF.Repositories
{
    public class GameRepositoryEF : IGameRepository
    {
        public GameDto GetByGameId(int gameId)
        {
            using (var ctx = new FifteenGameDataContext())
            {
                var game = ctx.Games.Include("Cells").FirstOrDefault(g => g.Id == gameId);
                if (game == null) return null;
                var dto = new GameDto
                {
                    Id = game.Id,
                    UserId = game.UserId,
                    MoveCount = game.MoveCount
                };
                for (int r = 0; r < Constants.RowCount; r++)
                    for (int c = 0; c < Constants.ColumnCount; c++)
                        dto.Cells[r, c] = Constants.FreeCellValue;

                foreach (var cell in game.Cells)
                    dto.Cells[cell.Row, cell.Column] = cell.Value;

                return dto;
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            using (var ctx = new FifteenGameDataContext())
            {
                return ctx.Games.Where(g => g.UserId == userId).ToList().Select(game =>
                {
                    var dto = new GameDto
                    {
                        Id = game.Id,
                        UserId = game.UserId,
                        MoveCount = game.MoveCount
                    };
                    for (int r = 0; r < Constants.RowCount; r++)
                        for (int c = 0; c < Constants.ColumnCount; c++)
                            dto.Cells[r, c] = Constants.FreeCellValue;

                    var cells = ctx.Cells.Where(c => c.GameId == game.Id).ToList();
                    foreach (var cell in cells)
                        dto.Cells[cell.Row, cell.Column] = cell.Value;
                    return dto;
                });
            }
        }

        public void Remove(int gameId)
        {
            using (var ctx = new FifteenGameDataContext())
            {
                var game = ctx.Games.Find(gameId);
                if (game != null)
                {
                    ctx.Cells.RemoveRange(ctx.Cells.Where(c => c.GameId == gameId));
                    ctx.Games.Remove(game);
                    ctx.SaveChanges();
                }
            }
        }

        public int Save(GameDto gameDto)
        {
            using (var ctx = new FifteenGameDataContext())
            {
                if (gameDto.Id == 0)
                {
                    var game = new EF.Entities.Game
                    {
                        UserId = gameDto.UserId,
                        MoveCount = gameDto.MoveCount
                    };
                    ctx.Games.Add(game);
                    ctx.SaveChanges();
                    gameDto.Id = game.Id;

                    for (int r = 0; r < Constants.RowCount; r++)
                        for (int c = 0; c < Constants.ColumnCount; c++)
                            if (gameDto.Cells[r, c] != Constants.FreeCellValue)
                                ctx.Cells.Add(new EF.Entities.Cell
                                {
                                    GameId = game.Id,
                                    Row = r,
                                    Column = c,
                                    Value = gameDto.Cells[r, c]
                                });
                    ctx.SaveChanges();
                }
                else
                {
                    var game = ctx.Games.Find(gameDto.Id);
                    if (game != null)
                    {
                        game.MoveCount = gameDto.MoveCount;
                        var cells = ctx.Cells.Where(c => c.GameId == gameDto.Id).ToList();
                        foreach (var cell in cells)
                        {
                            int val = gameDto.Cells[cell.Row, cell.Column];
                            if (val == Constants.FreeCellValue)
                                ctx.Cells.Remove(cell);
                            else
                                cell.Value = val;
                        }
                        ctx.SaveChanges();
                    }
                }
                return gameDto.Id;
            }
        }
    }
}