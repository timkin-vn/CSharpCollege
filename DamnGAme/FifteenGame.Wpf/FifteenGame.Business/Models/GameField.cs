using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship
{
    public enum Cell
    {
        Empty,
        Ship,
        Miss,
        Hit
    }

    public enum Orientation
    {
        Horizontal,
        Vertical
    }

    public enum ShotResult
    {
        Miss,
        Hit,
        Sunk,
        AlreadyShot
    }

    public class Ship
    {
        public int Length { get; }
        public List<(int X, int Y)> Cells { get; } = new List<(int X, int Y)>();
        private HashSet<(int X, int Y)> hits = new HashSet<(int X, int Y)>();

        public Ship(int length)
        {
            if (length <= 0) throw new ArgumentException("Length must be positive", nameof(length));
            Length = length;
        }

        public void SetPosition(int startX, int startY, Orientation orientation)
        {
            Cells.Clear();
            for (int i = 0; i < Length; i++)
            {
                int x = startX + (orientation == Orientation.Horizontal ? i : 0);
                int y = startY + (orientation == Orientation.Vertical ? i : 0);
                Cells.Add((x, y));
            }
        }

        public bool Occupies(int x, int y) => Cells.Contains((x, y));

        public void RegisterHit(int x, int y)
        {
            if (Occupies(x, y))
                hits.Add((x, y));
        }

        public bool IsSunk() => hits.Count >= Length;
    }

    public class GameField
    {
        public const int DefaultSize = 10;
        public int Width { get; }
        public int Height { get; }

        private Cell[,] grid;
        public IReadOnlyList<Ship> Ships => ships.AsReadOnly();
        private List<Ship> ships = new List<Ship>();
        private static readonly Random rng = new Random();

        // Default constructor 10x10
        public GameField(int width = DefaultSize, int height = DefaultSize)
        {
            if (width <= 0 || height <= 0) throw new ArgumentException("Invalid dimensions");
            Width = width;
            Height = height;
            grid = new Cell[Width, Height];
            Clear();
        }

        public void Clear()
        {
            ships.Clear();
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    grid[x, y] = Cell.Empty;
        }

        public bool InBounds(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;

        // Validate placement without committing
        public bool CanPlaceShip(int startX, int startY, Orientation orientation, int length)
        {
            for (int i = 0; i < length; i++)
            {
                int x = startX + (orientation == Orientation.Horizontal ? i : 0);
                int y = startY + (orientation == Orientation.Vertical ? i : 0);
                if (!InBounds(x, y)) return false;
                if (grid[x, y] != Cell.Empty) return false;
                // check adjacency (no-touch rule)
                for (int dx = -1; dx <= 1; dx++)
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int nx = x + dx, ny = y + dy;
                        if (InBounds(nx, ny) && grid[nx, ny] == Cell.Ship)
                            return false;
                    }
            }
            return true;
        }

        // Place and commit a ship (returns false if cannot)
        public bool PlaceShip(int startX, int startY, Orientation orientation, int length)
        {
            if (!CanPlaceShip(startX, startY, orientation, length))
                return false;

            var ship = new Ship(length);
            ship.SetPosition(startX, startY, orientation);
            ships.Add(ship);

            foreach (var (x, y) in ship.Cells)
                grid[x, y] = Cell.Ship;

            return true;
        }

        // Attempt to randomly place standard fleet: lengths provided
        public void AutoPlaceFleet(IEnumerable<int> lengths)
        {
            foreach (var l in lengths)
            {
                bool placed = false;
                int attempts = 0;
                while (!placed && attempts < 500)
                {
                    attempts++;
                    Orientation o = rng.Next(2) == 0 ? Orientation.Horizontal : Orientation.Vertical;
                    int maxX = o == Orientation.Horizontal ? Width - l : Width - 1;
                    int maxY = o == Orientation.Vertical ? Height - l : Height - 1;
                    int sx = rng.Next(0, maxX + 1);
                    int sy = rng.Next(0, maxY + 1);
                    placed = PlaceShip(sx, sy, o, l);
                }
                if (!placed)
                    throw new InvalidOperationException($"Не удалось автоматически разместить корабль длины {l}");
            }
        }

        // Receive a shot and return result
        public ShotResult Shoot(int x, int y)
        {
            if (!InBounds(x, y)) throw new ArgumentOutOfRangeException("Shot out of bounds");

            var current = grid[x, y];
            if (current == Cell.Miss || current == Cell.Hit) return ShotResult.AlreadyShot;

            if (current == Cell.Empty)
            {
                grid[x, y] = Cell.Miss;
                return ShotResult.Miss;
            }

            // Hit a ship
            if (current == Cell.Ship)
            {
                grid[x, y] = Cell.Hit;
                // mark hit on ship object
                var ship = ships.FirstOrDefault(s => s.Occupies(x, y));
                if (ship != null)
                {
                    ship.RegisterHit(x, y);
                    if (ship.IsSunk())
                    {
                        // optionally mark surrounding cells as Miss (classical rule) -- we will mark them as Miss to help UI/AI
                        MarkSurroundingAroundSunkShip(ship);
                        return ShotResult.Sunk;
                    }
                    return ShotResult.Hit;
                }
                return ShotResult.Hit;
            }

            return ShotResult.Miss;
        }

        private void MarkSurroundingAroundSunkShip(Ship ship)
        {
            foreach (var (sx, sy) in ship.Cells)
            {
                for (int dx = -1; dx <= 1; dx++)
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int nx = sx + dx, ny = sy + dy;
                        if (InBounds(nx, ny) && grid[nx, ny] == Cell.Empty)
                            grid[nx, ny] = Cell.Miss;
                    }
            }
        }

        public bool AllShipsSunk() => ships.All(s => s.IsSunk());

        // Return a copy of internal grid (for server logic). For view you might want masked version.
        public Cell[,] GetFullGridCopy()
        {
            var copy = new Cell[Width, Height];
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    copy[x, y] = grid[x, y];
            return copy;
        }

        // Get view for opponent: hide ships (only reveal hits/misses)
        public Cell[,] GetMaskedGridForOpponent()
        {
            var view = new Cell[Width, Height];
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    var c = grid[x, y];
                    if (c == Cell.Hit) view[x, y] = Cell.Hit;
                    else if (c == Cell.Miss) view[x, y] = Cell.Miss;
                    else view[x, y] = Cell.Empty; // hide ships and empty
                }
            return view;
        }

        // Utility: print grid to console (for debugging)
        // showShips = true -> reveal ships
        public string RenderToString(bool showShips = true)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append("  ");
            for (int x = 0; x < Width; x++) sb.Append($"{x} ");
            sb.AppendLine();
            for (int y = 0; y < Height; y++)
            {
                sb.Append($"{y:00} ");
                for (int x = 0; x < Width; x++)
                {
                    char ch = '.';
                    var c = grid[x, y];
                    if (c == Cell.Empty) ch = '.';
                    else if (c == Cell.Miss) ch = 'o';
                    else if (c == Cell.Hit) ch = 'X';
                    else if (c == Cell.Ship) ch = showShips ? 'S' : '.';
                    sb.Append($"{ch} ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
