using System;
using System.Collections.Generic;

namespace SeaBattle.Business
{
    
    public class GameService
    {
        
        public static readonly int[] Fleet = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };

        private readonly Random _rnd = new Random();

        
        public void PlaceFleet(Board board)
        {
            for (int r = 0; r < Board.Size; r++)
                for (int c = 0; c < Board.Size; c++)
                    board.Cells[r, c] = CellState.Empty;
            board.Ships.Clear();

            foreach (int len in Fleet)
            {
                bool placed = false;
                int tries = 0;
                while (!placed && tries++ < 2000)
                {
                    bool horiz = _rnd.Next(2) == 0;
                    int row = _rnd.Next(Board.Size);
                    int col = _rnd.Next(Board.Size);

                    var cells = new List<int[]>();
                    bool fits = true;
                    for (int i = 0; i < len; i++)
                    {
                        int rr = row + (horiz ? 0 : i);
                        int cc = col + (horiz ? i : 0);
                        if (rr >= Board.Size || cc >= Board.Size) { fits = false; break; }
                        cells.Add(new[] { rr, cc });
                    }
                    if (!fits) continue;
                    if (!IsFree(board, cells)) continue;

                    var ship = new Ship();
                    foreach (var c in cells)
                    {
                        ship.Cells.Add(c);
                        board.Cells[c[0], c[1]] = CellState.Ship;
                    }
                    board.Ships.Add(ship);
                    placed = true;
                }
            }
        }

        
        private bool IsFree(Board board, List<int[]> cells)
        {
            foreach (var c in cells)
            {
                for (int dr = -1; dr <= 1; dr++)
                    for (int dc = -1; dc <= 1; dc++)
                    {
                        int nr = c[0] + dr;
                        int nc = c[1] + dc;
                        if (nr < 0 || nr >= Board.Size || nc < 0 || nc >= Board.Size) continue;
                        if (board.Cells[nr, nc] == CellState.Ship) return false;
                    }
            }
            return true;
        }
    }
}
