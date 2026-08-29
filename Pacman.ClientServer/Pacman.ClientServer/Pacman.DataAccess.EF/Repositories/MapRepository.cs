using System.Data.Entity;
using System.Linq;
using Pacman.Common;
using Pacman.Common.Enums;
using Pacman.Common.Interfaces.Repositories;
using Pacman.Common.Models;

namespace Pacman.DataAccess.EF.Repositories
{
    public class MapRepository : IMapRepository
    {
        public MapDto GetDefaultMap()
        {
            using (var db = new PacmanDbContext())
            {
                var entity = db.Maps
                    .Include(m => m.Cells)
                    .FirstOrDefault(m => m.Name == Constants.DefaultMapName);

                return entity == null ? null : MapToDto(entity);
            }
        }

        public MapDto GetMapById(int id)
        {
            using (var db = new PacmanDbContext())
            {
                var entity = db.Maps
                    .Include(m => m.Cells)
                    .FirstOrDefault(m => m.Id == id);

                return entity == null ? null : MapToDto(entity);
            }
        }

        private MapDto MapToDto(Entities.MapEntity entity)
        {
            return new MapDto
            {
                Id = entity.Id,
                Name = entity.Name,
                RowCount = entity.RowCount,
                ColCount = entity.ColCount,
                Cells = entity.Cells.Select(c => new MapCellDto
                {
                    Row = c.Row,
                    Col = c.Col,
                    CellType = (CellType)c.CellType
                }).ToList()
            };
        }
    }
}