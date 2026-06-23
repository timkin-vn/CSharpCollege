using System.Collections.Generic;

namespace SeaBattle.Common
{
    public class Ship
    {
        public List<int[]> Cells { get; private set; }
        public int Hits { get; set; }

        public Ship()
        {
            Cells = new List<int[]>();
            Hits = 0;
        }

        public int Size { get { return Cells.Count; } }
        public bool IsSunk { get { return Hits >= Cells.Count; } }

        public bool Occupies(int row, int col)
        {
            foreach (var c in Cells)
                if (c[0] == row && c[1] == col) return true;
            return false;
        }
    }
}
