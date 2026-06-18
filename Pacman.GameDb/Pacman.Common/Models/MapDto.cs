using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Common.Models
{
    public class MapDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RowCount { get; set; }
        public int ColCount { get; set; }
        public List<MapCellDto> Cells { get; set; }
    }
}