using System;
using System.Collections.Generic;
using System.Linq;
using FifteenGame.Core.BusinessLogic.Models;

namespace FifteenGame.Core.BusinessLogic.Services
{
    /// <summary>
    /// Правила игры: собрать все ключи и дойти до выхода, не попавшись монстрам.
    /// Монстры двигаются на одну клетку к игроку после каждого второго хода игрока.
    /// </summary>
    public class GameService
    {
        public const int Size = 12;
        public const int KeyCount = 3;
        public const int MonsterCount = 2;
        private const int WallCount = 22;

        private readonly Random _random = new Random();

        public CellType[,] Cells { get; private set; }
        public List<Monster> Monsters { get; private set; }
        public int PlayerRow { get; private set; }
        public int PlayerColumn { get; private set; }
        public int KeysLeft { get; private set; }
        public int Moves { get; private set; }
        public GameState State { get; private set; }

        public GameService()
        {
            NewGame();
        }

        public void NewGame()
        {
            do
            {
                Generate();
            }
            while (!IsSolvable());

            Moves = 0;
            KeysLeft = KeyCount;
            State = GameState.Playing;
        }

        public bool TryMove(int dRow, int dColumn)
        {
            if (State != GameState.Playing)
            {
                return false;
            }

            int row = PlayerRow + dRow;
            int column = PlayerColumn + dColumn;
            if (!IsWalkable(row, column))
            {
                return false;
            }

            PlayerRow = row;
            PlayerColumn = column;
            Moves++;

            if (Cells[row, column] == CellType.Key)
            {
                Cells[row, column] = CellType.Floor;
                KeysLeft--;
            }

            if (IsCaught())
            {
                State = GameState.Lost;
                return true;
            }

            if (Cells[row, column] == CellType.Exit && KeysLeft == 0)
            {
                State = GameState.Won;
                return true;
            }

            if (Moves % 2 == 0)
            {
                MoveMonsters();
                if (IsCaught())
                {
                    State = GameState.Lost;
                }
            }

            return true;
        }

        public GameSnapshot ToSnapshot()
        {
            var cells = new CellType[Size][];
            for (int row = 0; row < Size; row++)
            {
                cells[row] = new CellType[Size];
                for (int column = 0; column < Size; column++)
                {
                    cells[row][column] = Cells[row, column];
                }
            }

            return new GameSnapshot
            {
                Cells = cells,
                Monsters = Monsters.Select(m => new Monster { Row = m.Row, Column = m.Column }).ToArray(),
                PlayerRow = PlayerRow,
                PlayerColumn = PlayerColumn,
                KeysLeft = KeysLeft,
                Moves = Moves,
                State = State,
            };
        }

        /// <summary>Восстанавливает игру из снимка. Возвращает false, если снимок некорректен.</summary>
        public bool TryRestore(GameSnapshot snapshot)
        {
            if (snapshot?.Cells == null || snapshot.Monsters == null || snapshot.Cells.Length != Size ||
                snapshot.Cells.Any(r => r == null || r.Length != Size))
            {
                return false;
            }

            var cells = new CellType[Size, Size];
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    cells[row, column] = snapshot.Cells[row][column];
                }
            }

            Cells = cells;
            Monsters = snapshot.Monsters.Select(m => new Monster { Row = m.Row, Column = m.Column }).ToList();
            PlayerRow = snapshot.PlayerRow;
            PlayerColumn = snapshot.PlayerColumn;
            KeysLeft = snapshot.KeysLeft;
            Moves = snapshot.Moves;
            State = snapshot.State;
            return true;
        }

        private bool IsWalkable(int row, int column) =>
            row >= 0 && row < Size && column >= 0 && column < Size && Cells[row, column] != CellType.Wall;

        private bool IsCaught() => Monsters.Any(m => m.Row == PlayerRow && m.Column == PlayerColumn);

        private void MoveMonsters()
        {
            var directions = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

            foreach (var monster in Monsters)
            {
                int bestRow = monster.Row;
                int bestColumn = monster.Column;
                int bestDistance = Distance(monster.Row, monster.Column, PlayerRow, PlayerColumn);

                foreach (var (dr, dc) in directions)
                {
                    int row = monster.Row + dr;
                    int column = monster.Column + dc;
                    if (!IsWalkable(row, column) || Monsters.Any(o => o != monster && o.Row == row && o.Column == column))
                    {
                        continue;
                    }

                    int distance = Distance(row, column, PlayerRow, PlayerColumn);
                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
                        bestRow = row;
                        bestColumn = column;
                    }
                }

                monster.Row = bestRow;
                monster.Column = bestColumn;
            }
        }

        private static int Distance(int r1, int c1, int r2, int c2) => Math.Abs(r1 - r2) + Math.Abs(c1 - c2);

        private void Generate()
        {
            Cells = new CellType[Size, Size];
            Monsters = new List<Monster>();
            PlayerRow = 0;
            PlayerColumn = 0;

            Cells[Size - 1, Size - 1] = CellType.Exit;

            PlaceRandom(WallCount, CellType.Wall);
            PlaceRandom(KeyCount, CellType.Key);

            while (Monsters.Count < MonsterCount)
            {
                int row = _random.Next(Size);
                int column = _random.Next(Size);
                if (Cells[row, column] == CellType.Floor &&
                    Distance(row, column, PlayerRow, PlayerColumn) >= 6 &&
                    !Monsters.Any(m => m.Row == row && m.Column == column))
                {
                    Monsters.Add(new Monster { Row = row, Column = column });
                }
            }
        }

        private void PlaceRandom(int count, CellType type)
        {
            int placed = 0;
            while (placed < count)
            {
                int row = _random.Next(Size);
                int column = _random.Next(Size);
                bool isStart = row == PlayerRow && column == PlayerColumn;
                if (!isStart && Cells[row, column] == CellType.Floor)
                {
                    Cells[row, column] = type;
                    placed++;
                }
            }
        }

        /// <summary>Из стартовой клетки должны быть достижимы все ключи и выход.</summary>
        private bool IsSolvable()
        {
            var visited = new bool[Size, Size];
            var queue = new Queue<(int, int)>();
            queue.Enqueue((PlayerRow, PlayerColumn));
            visited[PlayerRow, PlayerColumn] = true;
            int keys = 0;
            bool exit = false;

            while (queue.Count > 0)
            {
                var (row, column) = queue.Dequeue();
                if (Cells[row, column] == CellType.Key) keys++;
                if (Cells[row, column] == CellType.Exit) exit = true;

                foreach (var (dr, dc) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
                {
                    int r = row + dr;
                    int c = column + dc;
                    if (IsWalkable(r, c) && !visited[r, c])
                    {
                        visited[r, c] = true;
                        queue.Enqueue((r, c));
                    }
                }
            }

            return exit && keys == KeyCount;
        }
    }
}
