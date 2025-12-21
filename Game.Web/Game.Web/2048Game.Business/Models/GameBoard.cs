using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _2048Game.Business.Models
{
    public class GameBoard
    {
        public int[,] Tiles { get; set; }
        private readonly Random _random = new Random();

        public GameBoard()
        {
            Tiles = new int[4, 4];
        }

        public void Reset()
        {
            Tiles = new int[4, 4];
            AddRandomTile();
        }

        public void AddRandomTile()
        {
            var emptyCells = Enumerable.Range(0, 16).Select(i => new { Row = i / 4, Col = i % 4 }).Where(c => Tiles[c.Row, c.Col] == 0).ToList();
            
            if (!emptyCells.Any())
            {
                return;
            }

            var cell = emptyCells[_random.Next(emptyCells.Count)];
            Tiles[cell.Row, cell.Col] = _random.NextDouble() < 0.9 ? 2 : 4;
        }

        public bool Move(MoveDirection.Direction direction)
        {
            bool moved = false;
            int[,] oldTiles = (int[,])Tiles.Clone();

            for (int i = 0; i < 4; i++)
            {
                int[] line = GetLine(i, direction);
                int[] merged = MergeLine(line);
                SetLine(i, merged, direction);
            }

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    if (Tiles[r,c] != oldTiles[r,c])
                    {
                        moved = true;
                    }
                }
            }

            if (moved)
            {
                AddRandomTile();
            }
            
            return moved;
        }

        private int[] GetLine(int index, MoveDirection.Direction direction)
        {
            int[] line = new int[4];
            for (int i = 0; i < 4; i++)
            {
                switch (direction)
                {
                    case MoveDirection.Direction.Left:
                        line[i] = Tiles[index, i];
                        break;
                    case MoveDirection.Direction.Right:
                        line[i] = Tiles[index, 3 - i];
                        break;
                    case MoveDirection.Direction.Up:
                        line[i] = Tiles[i, index];
                        break;
                    case MoveDirection.Direction.Down:
                        line[i] = Tiles[3 - i, index];
                        break;
                }
            }
            return line;
        }

        private void SetLine(int index, int[] line, MoveDirection.Direction direction)
        {
            for (int i = 0; i < 4; i++)
            {
                switch(direction)
                {
                    case MoveDirection.Direction.Left:
                        Tiles[index, i] = line[i];
                        break;
                    case MoveDirection.Direction.Right:
                        Tiles[index, 3 - i] = line[i];
                        break;
                    case MoveDirection.Direction.Up:
                        Tiles[i, index] = line[i];
                        break;
                    case MoveDirection.Direction.Down:
                        Tiles[3 - i, index] = line[i];
                        break;
                }
            }
        }

        private int[] MergeLine(int[] line)
        {
            int[] filtered = line.Where(v => v != 0).ToArray();
            var merged = new int[4];
            int target = 0;
            for (int i = 0; i < filtered.Length; i++)
            {
                if (i < filtered.Length - 1 && filtered[i] == filtered[i+1])
                {
                    merged[target++] = filtered[i] * 2;
                    i++;
                }
                else
                {
                    merged[target++] = filtered[i];
                }
            }
            return merged;
        }

        public bool CanMove()
        {
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    if (Tiles[r,c] == 0)
                    {
                        return true;
                    }
                }
            }

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    int val = Tiles[r, c];
                    if ((r < 3 && Tiles[r + 1, c] == val) || (c < 3 && Tiles[r,c + 1] == val))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
