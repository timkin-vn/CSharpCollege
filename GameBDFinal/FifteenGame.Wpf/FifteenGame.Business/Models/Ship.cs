using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business.Models
{
    public class Ship
    {
        public int Size { get; }
        public List<Cell> Cells { get; } = new List<Cell>();

        public Ship(int size)
        {
            Size = size;
        }

        public bool IsSunk
        {
            get { return Cells.All(c => c.State == CellState.Hit || c.State == CellState.Sunk); }
        }
    }
}