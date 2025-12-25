using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Models
{
    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public TileType Type { get; set; }

        public Cell(int row, int column, TileType type)
        {
            Row = row;
            Column = column;
            Type = type;
        }
    }

    public enum TileType
    {
        Red,
        Green,
        Blue,
        Yellow,
        Purple
    }
}
