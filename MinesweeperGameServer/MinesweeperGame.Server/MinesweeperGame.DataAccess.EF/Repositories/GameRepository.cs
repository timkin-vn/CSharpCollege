using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MinesweeperGame.Common.BusinessModels;
using MinesweeperGame.Common.Contracts;
using MinesweeperGame.DataAccess.EF.DataContext;
using MinesweeperGame.DataAccess.EF.Entities;

namespace MinesweeperGame.DataAccess.EF.Repositories
{
    public class GameRepository : IGameRepository
    {
        public async Task<GameBoard> CreateAsync(GameBoard gameBoard)
        {
            using (var context = new GameDbContext())
            {
                var entity = MapToEntity(gameBoard);
                context.Games.Add(entity);
                await context.SaveChangesAsync();
                return MapToModel(entity);
            }
        }

        public async Task<GameBoard> GetByIdAsync(Guid id)
        {
            using (var context = new GameDbContext())
            {
                var entity = await context.Games
                    .Include(g => g.Cells)
                    .FirstOrDefaultAsync(g => g.Id == id);
                return entity != null ? MapToModel(entity) : null;
            }
        }

        public async Task UpdateAsync(GameBoard gameBoard)
        {
            using (var context = new GameDbContext())
            {
                var entity = await context.Games
                    .Include(g => g.Cells)
                    .FirstOrDefaultAsync(g => g.Id == gameBoard.Id);

                if (entity == null) return;

                entity.Status = gameBoard.Status;
                entity.FlagsPlaced = gameBoard.FlagsPlaced;
                entity.FinishedAt = gameBoard.FinishedAt;
                entity.ElapsedTime = gameBoard.ElapsedTime;

                foreach (var cell in gameBoard.Cells)
                {
                    var cellEntity = entity.Cells
                        .FirstOrDefault(c => c.Row == cell.Row && c.Column == cell.Column);
                    if (cellEntity != null)
                    {
                        cellEntity.State = cell.State;
                        cellEntity.IsMine = cell.IsMine;
                        cellEntity.AdjacentMinesCount = cell.AdjacentMinesCount;
                    }
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var context = new GameDbContext())
            {
                var entity = await context.Games.FindAsync(id);
                if (entity != null)
                {
                    context.Games.Remove(entity);
                    await context.SaveChangesAsync();
                }
            }
        }

        private GameEntity MapToEntity(GameBoard model)
        {
            var entity = new GameEntity
            {
                Id = model.Id,
                Difficulty = model.Difficulty,
                Status = model.Status,
                Rows = model.Rows,
                Columns = model.Columns,
                TotalMines = model.TotalMines,
                FlagsPlaced = model.FlagsPlaced,
                CreatedAt = model.CreatedAt,
                FinishedAt = model.FinishedAt,
                ElapsedTime = model.ElapsedTime
            };
            foreach (var cell in model.Cells)
            {
                entity.Cells.Add(new CellEntity
                {
                    GameId = model.Id,
                    Row = cell.Row,
                    Column = cell.Column,
                    IsMine = cell.IsMine,
                    AdjacentMinesCount = cell.AdjacentMinesCount,
                    State = cell.State
                });
            }
            return entity;
        }

        private GameBoard MapToModel(GameEntity entity)
        {
            var board = new GameBoard
            {
                Id = entity.Id,
                Difficulty = entity.Difficulty,
                Status = entity.Status,
                Rows = entity.Rows,
                Columns = entity.Columns,
                TotalMines = entity.TotalMines,
                FlagsPlaced = entity.FlagsPlaced,
                CreatedAt = entity.CreatedAt,
                FinishedAt = entity.FinishedAt,
                ElapsedTime = entity.ElapsedTime
            };
            foreach (var cell in entity.Cells)
            {
                board.Cells.Add(new Cell
                {
                    Row = cell.Row,
                    Column = cell.Column,
                    IsMine = cell.IsMine,
                    AdjacentMinesCount = cell.AdjacentMinesCount,
                    State = cell.State
                });
            }
            return board;
        }
    }
}
