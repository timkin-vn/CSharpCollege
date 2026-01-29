using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Pacman.Common;
using Pacman.Common.Enums;
using Pacman.DataAccess.EF.Entities;

namespace Pacman.DataAccess.EF.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<PacmanDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PacmanDbContext context)
        {
            // Check if default map already exists
            if (context.Maps.Any(m => m.Name == Constants.DefaultMapName))
                return;

            var map = new MapEntity
            {
                Name = Constants.DefaultMapName,
                RowCount = Constants.DefaultRowCount,
                ColCount = Constants.DefaultColCount
            };

            context.Maps.Add(map);
            context.SaveChanges();

            // Generate map cells
            var cells = GenerateDefaultMapCells(map.Id, Constants.DefaultRowCount, Constants.DefaultColCount);
            foreach (var cell in cells)
            {
                context.MapCells.Add(cell);
            }

            context.SaveChanges();
        }

        private MapCellEntity[] GenerateDefaultMapCells(int mapId, int rows, int cols)
        {
            var cells = new MapCellEntity[rows * cols];
            int index = 0;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    CellType cellType = CellType.Empty;

                    // Outer walls
                    if (r == 0 || r == rows - 1 || c == 0 || c == cols - 1)
                    {
                        cellType = CellType.Wall;
                    }
                    // Power pellets in corners
                    else if ((r == 1 && c == 1) || (r == 1 && c == cols - 2) ||
                             (r == rows - 2 && c == 1) || (r == rows - 2 && c == cols - 2))
                    {
                        cellType = CellType.PowerPellet;
                    }
                    // Internal walls pattern
                    else if ((r % 4 == 2 && c % 4 == 2) || (r % 6 == 3 && c % 6 == 3))
                    {
                        cellType = CellType.Wall;
                    }
                    // Empty corridors for ghosts and pacman start
                    else if ((r == 1 && c >= cols / 2 - 2 && c <= cols / 2 + 2) ||
                             (r == rows - 2 && c == cols / 2))
                    {
                        cellType = CellType.Empty;
                    }
                    // Pellets everywhere else
                    else
                    {
                        cellType = CellType.Pellet;
                    }

                    cells[index++] = new MapCellEntity
                    {
                        MapId = mapId,
                        Row = r,
                        Col = c,
                        CellType = (int)cellType
                    };
                }
            }

            return cells;
        }
    }
}
