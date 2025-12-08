using _2048Game.Common.BusinessModels;
using _2048Game.Common.Definitions;
using _2048Game.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _2048Game.Common.BusinessModels
{
    public class GameModel
    {
        public int[,] Tiles { get; set; }

        private readonly Random _random = new Random();
        public int UserId {  get; set; }
        public int Id {  get; set; }
        public int MoveCount { get; set; }

        public GameModel()
        {
            Tiles = new int[4, 4];
        }
        public void Reset()
        {
            Tiles = new int[4, 4];
            AddRandomTile();
            AddRandomTile();
        }
        public void AddRandomTile()
        {
            var emptyCells = Enumerable
                .Range(0, 16)
                .Select(i => new { Row = i / 4, Col = i % 4 })
                .Where(c => Tiles[c.Row, c.Col] == 0)
                .ToList();

            if (!emptyCells.Any())
            {
                return;
            }

            var cell = emptyCells[_random.Next(emptyCells.Count)];
            Tiles[cell.Row, cell.Col] = _random.NextDouble() < 0.75 ? 2 : 4;
        }
        public bool Move(MoveDirections.MoveDirection direction)
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
                    if (Tiles[r, c] != oldTiles[r, c])
                    {
                        moved = true;
                        break;
                    }
                }
            }

            if (moved)
            {
                AddRandomTile();
            }
            return moved;
        }
        public bool CanMove()
        {
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    if (Tiles[r, c] == 0)
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
                    if ((r < 3 && Tiles[r + 1, c] == val) || (c < 3 && Tiles[r, c + 1] == val))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public bool HasWon()
        {
            foreach (var tile in Tiles)
            {
                if (tile == 2048)
                {
                    return true;
                }
            }
            return false;
        }

        private int[] GetLine(int index, MoveDirections.MoveDirection direction)
        {
            int[] line = new int[4];
            for (int i = 0; i < 4; i++)
            {
                switch (direction)
                {
                    case MoveDirections.MoveDirection.Left:
                        line[i] = Tiles[index, i];
                        break;
                    case MoveDirections.MoveDirection.Right:
                        line[i] = Tiles[index, 3 - i];
                        break;
                    case MoveDirections.MoveDirection.Up:
                        line[i] = Tiles[i, index];
                        break;
                    case MoveDirections.MoveDirection.Down:
                        line[i] = Tiles[3 - i, index];
                        break;
                }
            }
            return line;
        }

        private void SetLine(int index, int[] line, MoveDirections.MoveDirection direction)
        {
            for (int i = 0; i < 4; i++)
            {
                switch (direction)
                {
                    case MoveDirections.MoveDirection.Left:
                        Tiles[index, i] = line[i];
                        break;
                    case MoveDirections.MoveDirection.Right:
                        Tiles[index, 3 - i] = line[i];
                        break;
                    case MoveDirections.MoveDirection.Up:
                        Tiles[i, index] = line[i];
                        break;
                    case MoveDirections.MoveDirection.Down:
                        Tiles[3 - i, index] = line[i];
                        break;
                }
            }
        }
        private int[] MergeLine(int[] line)
        {
            int[] filtered = line.Where(v => v != 0).ToArray();
            int[] merged = new int[4];
            int target = 0;

            for (int i = 0; i < filtered.Length; i++)
            {
                if (i < filtered.Length - 1 && filtered[i] == filtered[i + 1])
                {
                    int newValue = filtered[i] * 2;
                    merged[target++] = newValue;
                    i++;
                }
                else
                {
                    merged[target++] = filtered[i];
                }
            }

            return merged;
        }
    }
}
