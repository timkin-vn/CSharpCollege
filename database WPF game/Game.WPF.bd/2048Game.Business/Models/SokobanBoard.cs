using System;
using System.Collections.Generic;
using System.Linq;

namespace _2048Game.Business.Models
{
    // Simple Sokoban board representation
    // 0 - empty
    // 1 - wall
    // 2 - box
    // 3 - target
    // 4 - player
    // 5 - box on target
    // 6 - player on target
    public class SokobanBoard
    {
        public int Rows { get; }
        public int Cols { get; }
        public int[,] Cells { get; private set; }

        public (int r, int c) PlayerPosition { get; set; }

        // Positions where boxes were placed onto targets during the last move
        public List<(int r, int c)> LastPlacedPositions { get; private set; } = new List<(int r, int c)>();

        public SokobanBoard(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Cells = new int[rows, cols];
        }

        public void LoadFromArray(int[,] template)
        {
            if (template.GetLength(0) != Rows || template.GetLength(1) != Cols)
                throw new ArgumentException("Template size mismatch");

            Cells = (int[,])template.Clone();

            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Cols; c++)
                {
                    if (Cells[r, c] == 4 || Cells[r, c] == 6)
                        PlayerPosition = (r, c);
                }
        }

        public bool IsWin()
        {
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Cols; c++)
                    if (Cells[r, c] == 2)
                        return false;
            return true;
        }

        public bool CanMove(SokobanDirection dir)
        {
            var (dr, dc) = DirToDelta(dir);
            int pr = PlayerPosition.r;
            int pc = PlayerPosition.c;
            int nr = pr + dr;
            int nc = pc + dc;
            if (!InBounds(nr, nc)) return false;
            int dest = Cells[nr, nc];
            if (dest == 1) return false; // wall
            if (dest == 2 || dest == 5)
            {
                int br = nr + dr;
                int bc = nc + dc;
                if (!InBounds(br, bc)) return false;
                int beyond = Cells[br, bc];
                if (beyond == 0 || beyond == 3) return true;
                return false;
            }
            return true;
        }

        public bool Move(SokobanDirection dir)
        {
            LastPlacedPositions.Clear();

            if (!CanMove(dir)) return false;

            var (dr, dc) = DirToDelta(dir);
            int pr = PlayerPosition.r;
            int pc = PlayerPosition.c;
            int nr = pr + dr;
            int nc = pc + dc;

            int dest = Cells[nr, nc];

            bool playerOnTarget = Cells[pr, pc] == 6;

            if (dest == 0 || dest == 3)
            {
                // move player
                Cells[nr, nc] = (dest == 3) ? 6 : 4;
                Cells[pr, pc] = playerOnTarget ? 3 : 0;
            }
            else if (dest == 2 || dest == 5)
            {
                int br = nr + dr;
                int bc = nc + dc;
                int beyond = Cells[br, bc];

                // move box
                bool willBeOnTarget = (beyond == 3);
                Cells[br, bc] = willBeOnTarget ? 5 : 2;

                if (willBeOnTarget)
                {
                    LastPlacedPositions.Add((br, bc));
                }

                // move player into box's spot
                Cells[nr, nc] = (dest == 5) ? 6 : 4;

                Cells[pr, pc] = playerOnTarget ? 3 : 0;
            }

            PlayerPosition = (nr, nc);
            return true;
        }

        private (int dr, int dc) DirToDelta(SokobanDirection dir)
        {
            switch (dir)
            {
                case SokobanDirection.Up: return (-1, 0);
                case SokobanDirection.Down: return (1, 0);
                case SokobanDirection.Left: return (0, -1);
                default: return (0, 1);
            }
        }

        private bool InBounds(int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Cols;
        }

        public int[,] GetCopy()
        {
            return (int[,])Cells.Clone();
        }
    }
}
