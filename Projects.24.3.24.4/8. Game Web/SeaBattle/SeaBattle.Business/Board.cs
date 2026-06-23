using System.Collections.Generic;

namespace SeaBattle.Business
{
    public class Board
    {
        public const int Size = 10;

        public CellState[,] Cells { get; private set; }
        public List<Ship> Ships { get; private set; }

        public Board()
        {
            Cells = new CellState[Size, Size];
            Ships = new List<Ship>();
        }

        public Ship ShipAt(int row, int col)
        {
            foreach (var s in Ships)
                if (s.Occupies(row, col)) return s;
            return null;
        }

        public bool AllSunk()
        {
            if (Ships.Count == 0) return false;
            foreach (var s in Ships)
                if (!s.IsSunk) return false;
            return true;
        }

        public ShotResult Shoot(int row, int col)
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size)
                return ShotResult.Invalid;

            CellState st = Cells[row, col];
            if (st == CellState.Miss || st == CellState.Hit || st == CellState.Sunk)
                return ShotResult.Invalid;

            Ship ship = ShipAt(row, col);
            if (ship == null)
            {
                Cells[row, col] = CellState.Miss;
                return ShotResult.Miss;
            }

            ship.Hits++;
            if (ship.IsSunk)
            {
                foreach (var c in ship.Cells)
                    Cells[c[0], c[1]] = CellState.Sunk;
                return ShotResult.Sunk;
            }

            Cells[row, col] = CellState.Hit;
            return ShotResult.Hit;
        }
    }
}
