using LightsOutGame.Common.Contracts.Repositories;
using LightsOutGame.Common.Definitions;
using LightsOutGame.Common.Dtos;
using LightsOutGame.DataAccess.EF.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LightsOutGame.DataAccess.EF.Repositories
{
    public class GameRepositoryEF : IGameRepository
    {
        public int Save(GameDto game)
        {
            using (var context = new LightsOutGameContext())
            {
                GameEntity entity;

                if (game.Id == 0)
                {
                    entity = new GameEntity();
                    context.Games.Add(entity);
                }
                else
                {
                    entity = context.Games.First(g => g.Id == game.Id);
                }

                entity.UserId = game.UserId;
                entity.MoveCount = game.MoveCount;
                entity.CellsData = Pack(game.Cells);

                context.SaveChanges();
                return entity.Id;
            }
        }

        public GameDto GetByGameId(int gameId)
        {
            using (var context = new LightsOutGameContext())
            {
                var entity = context.Games.FirstOrDefault(g => g.Id == gameId);
                return ToDto(entity);
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            using (var context = new LightsOutGameContext())
            {
                return context.Games
                    .Where(g => g.UserId == userId)
                    .ToList()
                    .Select(ToDto)
                    .ToList();
            }
        }

        public void Remove(int gameId)
        {
            using (var context = new LightsOutGameContext())
            {
                var entity = context.Games.FirstOrDefault(g => g.Id == gameId);
                if (entity == null)
                {
                    return;
                }

                context.Games.Remove(entity);
                context.SaveChanges();
            }
        }

        private GameDto ToDto(GameEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new GameDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                MoveCount = entity.MoveCount,
                Cells = Unpack(entity.CellsData),
            };
        }

        private string Pack(int[,] cells)
        {
            var values = new List<string>();
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    values.Add(cells[row, column].ToString());
                }
            }

            return string.Join(",", values);
        }

        private int[,] Unpack(string data)
        {
            var cells = new int[Constants.RowCount, Constants.ColumnCount];
            if (string.IsNullOrEmpty(data))
            {
                return cells;
            }

            var values = data.Split(',');
            int index = 0;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    cells[row, column] = int.Parse(values[index++]);
                }
            }

            return cells;
        }
    }
}
