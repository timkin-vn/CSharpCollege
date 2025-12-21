using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2048Game.Web.Models
{
    public enum TileType
    {
        Empty,
        Wall,
        Box,
        Target,
        Player,
        BoxOnTarget,
        PlayerOnTarget
    }

    public enum Direction { Up, Down, Left, Right }

    // Adapted business SokobanBoard for web project
    public class SokobanBoard
    {
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public int[,] Cells { get; private set; }

        public (int r, int c) PlayerPosition { get; set; }
        public List<(int r, int c)> LastPlacedPositions { get; private set; } = new List<(int r, int c)>();
        public int MoveCount { get; private set; }

        public SokobanBoard()
        {
            // default larger template (9x9) with outer walls, boxes and targets to match WPF layout size
            int[,] template = new int[,] {
                {1,1,1,1,1,1,1,1,1},
                {1,0,0,0,0,0,0,0,1},
                {1,0,2,0,0,0,2,0,1},
                {1,0,0,0,0,0,0,0,1},
                {1,0,0,3,0,3,0,0,1},
                {1,0,0,0,0,0,0,0,1},
                {1,0,0,0,4,0,0,0,1},
                {1,0,0,0,0,0,0,0,1},
                {1,1,1,1,1,1,1,1,1}
            };
            Rows = template.GetLength(0);
            Cols = template.GetLength(1);
            Cells = new int[Rows, Cols];
            LoadFromArray(template);
        }

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

        public TileType GetTile(int r, int c)
        {
            int v = Cells[r, c];
            switch (v)
            {
                case 1: return TileType.Wall;
                case 2: return TileType.Box;
                case 3: return TileType.Target;
                case 4: return TileType.Player;
                case 5: return TileType.BoxOnTarget;
                case 6: return TileType.PlayerOnTarget;
                default: return TileType.Empty;
            }
        }

        public bool IsSolved()
        {
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Cols; c++)
                    if (Cells[r, c] == 2) // any box not on target
                        return false;
            return true;
        }

        private (int dr, int dc) DirToDelta(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up: return (-1, 0);
                case Direction.Down: return (1, 0);
                case Direction.Left: return (0, -1);
                default: return (0, 1);
            }
        }

        private bool InBounds(int r, int c) => r >= 0 && r < Rows && c >= 0 && c < Cols;

        public bool CanMove()
        {
            return !IsSolved();
        }

        public bool Move(Direction dir)
        {
            LastPlacedPositions.Clear();

            var (dr, dc) = DirToDelta(dir);
            int pr = PlayerPosition.r;
            int pc = PlayerPosition.c;
            int nr = pr + dr;
            int nc = pc + dc;
            if (!InBounds(nr, nc)) return false;
            int dest = Cells[nr, nc];

            if (dest == 1) return false; // wall

            bool playerOnTarget = Cells[pr, pc] == 6;
            if (dest == 0 || dest == 3)
            {
                // move player
                Cells[nr, nc] = (dest == 3) ? 6 : 4;
                Cells[pr, pc] = playerOnTarget ? 3 : 0;
                PlayerPosition = (nr, nc);
                MoveCount++;
                return true;
            }
            else if (dest == 2 || dest == 5)
            {
                int br = nr + dr;
                int bc = nc + dc;
                if (!InBounds(br, bc)) return false;
                int beyond = Cells[br, bc];
                if (beyond == 0 || beyond == 3)
                {
                    bool willBeOnTarget = (beyond == 3);
                    Cells[br, bc] = willBeOnTarget ? 5 : 2;
                    if (willBeOnTarget) LastPlacedPositions.Add((br, bc));
                    Cells[nr, nc] = (dest == 5) ? 6 : 4;
                    Cells[pr, pc] = playerOnTarget ? 3 : 0;
                    PlayerPosition = (nr, nc);
                    MoveCount++;
                    return true;
                }
                return false;
            }

            return false;
        }

        public int[,] GetCopy() => (int[,])Cells.Clone();
    }
}
