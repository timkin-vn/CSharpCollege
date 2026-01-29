using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pacman.Common.Enums;
namespace Pacman.Common.Models
{
    public class MapCellDto
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public CellType CellType { get; set; }
    }
}