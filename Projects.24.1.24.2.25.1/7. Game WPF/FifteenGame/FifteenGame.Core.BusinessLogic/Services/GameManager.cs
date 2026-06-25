using System;
using System.Collections.Generic;
using System.Linq;
using FifteenGame.Business.Models;

namespace FifteenGame.Business.Services
{
    public class GameManager
    {
        private List<Level> _levels;
        private int _currentLevelIndex;
        public Level CurrentLevel { get; private set; }
        public Player Player { get; private set; }
        public bool IsGameOver { get; private set; }
        public bool IsLevelComplete { get; private set; }
        public int TotalLevels => _levels.Count;
        public int CurrentLevelIndex => _currentLevelIndex;

        public event Action LevelChanged;
        public event Action GameFinished;

        private Random _random = new Random();

        public GameManager()
        {
            _levels = new List<Level>();
            GenerateNewLevel();
            StartGame();
        }

        private void GenerateNewLevel()
        {
            int levelNumber = _levels.Count + 1;
            var level = GenerateLevel(levelNumber);
            _levels.Add(level);
        }

        private Level GenerateLevel(int levelNumber)
        {
            var level = new Level();

            // Заполняем стенами
            for (int r = 0; r < Level.Rows; r++)
                for (int c = 0; c < Level.Columns; c++)
                    level.SetCell(r, c, CellType.Wall);

            // Количество комнат растёт с уровнем
            int mainRoomCount = Math.Min(4 + levelNumber, 12);
            int extraRoomCount = Math.Min(levelNumber, 6); // Дополнительные тупиковые комнаты
            var rooms = GenerateRooms(level, mainRoomCount + extraRoomCount);

            if (rooms.Count == 0) return level; // Fallback

            var mainRooms = rooms.Take(mainRoomCount).ToList();
            var extraRooms = rooms.Skip(mainRoomCount).ToList();

            // Создаём коридоры для цепочки комнат (каждая комната соединена со следующей)
            var corridorCells = CreateCorridorsChain(level, mainRooms);

            // Подключаем дополнительные комнаты к случайным комнатам из основной цепи
            foreach (var extraRoom in extraRooms)
            {
                var targetRoom = mainRooms[_random.Next(mainRooms.Count)];
                corridorCells.AddRange(CreateCorridor(level, extraRoom, targetRoom));
            }

            // Выбираем позиции для дверей с проверкой расстояния
            var doorCells = SelectDoorsWithSpacing(level, corridorCells, mainRooms.Count - 1);

            // Ставим двери
            foreach (var pos in doorCells)
            {
                level.SetCell(pos.Item1, pos.Item2, CellType.Door);
                level.DoorStates[(pos.Item1, pos.Item2)] = false;
            }

            int doorCount = doorCells.Count;

            var startRoom = mainRooms.First();
            var exitRoom = mainRooms.Last();

            // Ключи: разбрасываем по всем комнатам, кроме комнаты с выходом
            PlaceKeys(level, rooms, exitRoom, doorCount);

            // Переключатели: doorCount + (1..3)
            int switchCount = doorCount + _random.Next(1, 4);
            PlaceSwitches(level, rooms, startRoom, switchCount);

            // Старт в первой комнате
            int sr, sc;
            do {
                sr = startRoom.Row + 1 + _random.Next(startRoom.Height - 2);
                sc = startRoom.Col + 1 + _random.Next(startRoom.Width - 2);
            } while (level.Grid[sr, sc].Type != CellType.Floor);
            level.StartPosition = (sr, sc);

            // Выход в последней комнате
            int er, ec;
            do {
                er = exitRoom.Row + 1 + _random.Next(exitRoom.Height - 2);
                ec = exitRoom.Col + 1 + _random.Next(exitRoom.Width - 2);
            } while (level.Grid[er, ec].Type != CellType.Floor);
            level.ExitPosition = (er, ec);
            level.SetCell(er, ec, CellType.Exit);

            return level;
        }

        private List<Room> GenerateRooms(Level level, int roomCount)
        {
            var rooms = new List<Room>();
            int maxAttempts = 2000;
            int attempts = 0;

            while (rooms.Count < roomCount && attempts < maxAttempts)
            {
                attempts++;
                int width = _random.Next(5, 12); // Более разнообразные размеры
                int height = _random.Next(5, 12);
                int row = _random.Next(1, Level.Rows - height - 1);
                int col = _random.Next(1, Level.Columns - width - 1);

                var newRoom = new Room(row, col, width, height);

                // Проверка пересечений с отступом в 1 клетку, чтобы комнаты не слипались
                bool intersects = rooms.Any(r => 
                    !(r.Row + r.Height + 1 <= row || r.Row >= row + height + 1 ||
                      r.Col + r.Width + 1 <= col || r.Col >= col + width + 1));

                if (!intersects)
                {
                    rooms.Add(newRoom);
                    for (int r = row; r < row + height; r++)
                    {
                        for (int c = col; c < col + width; c++)
                        {
                            // Делаем комнаты более органичными, скругляя углы
                            bool isCorner = (r == row && c == col) || 
                                            (r == row && c == col + width - 1) || 
                                            (r == row + height - 1 && c == col) || 
                                            (r == row + height - 1 && c == col + width - 1);
                            
                            if (isCorner && _random.NextDouble() < 0.6) continue;

                            level.SetCell(r, c, CellType.Floor);
                        }
                    }

                    // Добавляем случайные препятствия (колонны) внутри комнаты
                    int obstacleCount = _random.Next(0, (width * height) / 8);
                    for (int i = 0; i < obstacleCount; i++)
                    {
                        int or = _random.Next(row + 2, row + height - 2);
                        int oc = _random.Next(col + 2, col + width - 2);
                        level.SetCell(or, oc, CellType.Wall);
                    }
                }
            }

            return rooms;
        }

        private List<(int, int)> CreateCorridorsChain(Level level, List<Room> rooms)
        {
            var allCorridorCells = new List<(int, int)>();
            for (int i = 0; i < rooms.Count - 1; i++)
            {
                allCorridorCells.AddRange(CreateCorridor(level, rooms[i], rooms[i + 1]));
            }
            return allCorridorCells;
        }

        private List<(int, int)> CreateCorridor(Level level, Room roomA, Room roomB)
        {
            var corridorCells = new List<(int, int)>();
            int ax = roomA.Row + roomA.Height / 2;
            int ay = roomA.Col + roomA.Width / 2;
            int bx = roomB.Row + roomB.Height / 2;
            int by = roomB.Col + roomB.Width / 2;

            bool horizontalFirst = _random.NextDouble() > 0.5;

            if (horizontalFirst)
            {
                for (int c = Math.Min(ay, by); c <= Math.Max(ay, by); c++)
                {
                    if (level.Grid[ax, c]?.Type == CellType.Wall)
                    {
                        level.SetCell(ax, c, CellType.Floor);
                        corridorCells.Add((ax, c));
                    }
                }
                for (int r = Math.Min(ax, bx); r <= Math.Max(ax, bx); r++)
                {
                    if (level.Grid[r, by]?.Type == CellType.Wall)
                    {
                        level.SetCell(r, by, CellType.Floor);
                        corridorCells.Add((r, by));
                    }
                }
            }
            else
            {
                for (int r = Math.Min(ax, bx); r <= Math.Max(ax, bx); r++)
                {
                    if (level.Grid[r, ay]?.Type == CellType.Wall)
                    {
                        level.SetCell(r, ay, CellType.Floor);
                        corridorCells.Add((r, ay));
                    }
                }
                for (int c = Math.Min(ay, by); c <= Math.Max(ay, by); c++)
                {
                    if (level.Grid[bx, c]?.Type == CellType.Wall)
                    {
                        level.SetCell(bx, c, CellType.Floor);
                        corridorCells.Add((bx, c));
                    }
                }
            }
            return corridorCells;
        }

        // Выбор позиций для дверей с проверкой расстояния
        private List<(int, int)> SelectDoorsWithSpacing(Level level, List<(int, int)> corridorCells, int doorCount)
        {
            var candidates = new List<(int, int)>();
            foreach (var (r, c) in corridorCells)
            {
                int floorNeighbors = 0;
                bool hasWallUp = false, hasWallDown = false, hasWallLeft = false, hasWallRight = false;

                if (r > 0 && level.Grid[r - 1, c]?.Type == CellType.Floor) floorNeighbors++;
                else hasWallUp = true;
                if (r < Level.Rows - 1 && level.Grid[r + 1, c]?.Type == CellType.Floor) floorNeighbors++;
                else hasWallDown = true;
                if (c > 0 && level.Grid[r, c - 1]?.Type == CellType.Floor) floorNeighbors++;
                else hasWallLeft = true;
                if (c < Level.Columns - 1 && level.Grid[r, c + 1]?.Type == CellType.Floor) floorNeighbors++;
                else hasWallRight = true;

                if (floorNeighbors == 2)
                {
                    bool vertical = (r > 0 && level.Grid[r - 1, c]?.Type == CellType.Floor &&
                                     r < Level.Rows - 1 && level.Grid[r + 1, c]?.Type == CellType.Floor);
                    bool horizontal = (c > 0 && level.Grid[r, c - 1]?.Type == CellType.Floor &&
                                       c < Level.Columns - 1 && level.Grid[r, c + 1]?.Type == CellType.Floor);
                    if (vertical && hasWallLeft && hasWallRight)
                        candidates.Add((r, c));
                    else if (horizontal && hasWallUp && hasWallDown)
                        candidates.Add((r, c));
                }
            }

            if (candidates.Count == 0)
                return new List<(int, int)>();

            var shuffled = candidates.OrderBy(x => _random.Next()).ToList();
            var selected = new List<(int, int)>();
            foreach (var pos in shuffled)
            {
                if (selected.Count >= doorCount) break;
                bool tooClose = false;
                foreach (var sel in selected)
                {
                    int dist = Math.Abs(pos.Item1 - sel.Item1) + Math.Abs(pos.Item2 - sel.Item2);
                    if (dist < 5)
                    {
                        tooClose = true;
                        break;
                    }
                }
                if (!tooClose)
                    selected.Add(pos);
            }

            // Если выбрали меньше, чем надо, добираем любыми
            if (selected.Count < doorCount)
            {
                foreach (var pos in shuffled)
                {
                    if (selected.Count >= doorCount) break;
                    if (!selected.Contains(pos))
                        selected.Add(pos);
                }
            }

            return selected;
        }

        // Размещение ключей: разбрасываем по всем комнатам, кроме комнаты с выходом
        private void PlaceKeys(Level level, List<Room> rooms, Room exitRoom, int keyCount)
        {
            var available = new List<(int, int)>();
            foreach (var room in rooms)
            {
                if (room == exitRoom) continue;
                for (int r = room.Row + 1; r < room.Row + room.Height - 1; r++)
                    for (int c = room.Col + 1; c < room.Col + room.Width - 1; c++)
                    {
                        if (level.Grid[r, c].Type == CellType.Floor)
                            available.Add((r, c));
                    }
            }

            int placed = 0;
            while (placed < keyCount && available.Count > 0)
            {
                int idx = _random.Next(available.Count);
                var pos = available[idx];
                level.SetCell(pos.Item1, pos.Item2, CellType.Key);
                placed++;
                available.RemoveAt(idx);
            }
        }

        private void PlaceSwitches(Level level, List<Room> rooms, Room startRoom, int count)
        {
            var available = new List<(int, int)>();
            foreach (var room in rooms)
            {
                if (room == startRoom) continue; // пропускаем стартовую комнату
                for (int r = room.Row + 1; r < room.Row + room.Height - 1; r++)
                    for (int c = room.Col + 1; c < room.Col + room.Width - 1; c++)
                    {
                        if (level.Grid[r, c].Type == CellType.Floor)
                            available.Add((r, c));
                    }
            }

            int placed = 0;
            while (placed < count && available.Count > 0)
            {
                int idx = _random.Next(available.Count);
                var pos = available[idx];
                if (level.Grid[pos.Item1, pos.Item2].Type == CellType.Floor)
                {
                    level.AddSwitch(pos.Item1, pos.Item2);
                    placed++;
                }
                available.RemoveAt(idx);
            }
        }

        private class Room
        {
            public int Row { get; }
            public int Col { get; }
            public int Width { get; }
            public int Height { get; }

            public Room(int row, int col, int width, int height)
            {
                Row = row; Col = col; Width = width; Height = height;
            }

            public bool Intersects(Room other)
            {
                return !(other.Row + other.Height <= Row || other.Row >= Row + Height ||
                         other.Col + other.Width <= Col || other.Col >= Col + Width);
            }
        }

        // ----- Остальные методы (StartGame, LoadLevel, TryMove, NextLevel, Restart) -----
        public void StartGame()
        {
            _currentLevelIndex = 0;
            LoadLevel(_currentLevelIndex);
            IsGameOver = false;
            IsLevelComplete = false;
        }

        private void LoadLevel(int index)
        {
            CurrentLevel = _levels[index];
            var start = CurrentLevel.StartPosition;
            Player = new Player(start.Row, start.Col);

            foreach (var key in CurrentLevel.DoorStates.Keys.ToList())
                CurrentLevel.DoorStates[key] = false;
            foreach (var key in CurrentLevel.SwitchStates.Keys.ToList())
                CurrentLevel.SwitchStates[key] = false;

            IsLevelComplete = false;
            LevelChanged?.Invoke();
        }

        public bool TryMove(int deltaRow, int deltaCol)
        {
            if (IsGameOver || IsLevelComplete) return false;

            int newRow = Player.Row + deltaRow;
            int newCol = Player.Column + deltaCol;

            if (newRow < 0 || newRow >= Level.Rows || newCol < 0 || newCol >= Level.Columns)
                return false;

            var cell = CurrentLevel.GetCell(newRow, newCol);
            if (cell == null) return false;

            bool moved = false;

            switch (cell.Type)
            {
                case CellType.Wall:
                    return false;

                case CellType.Floor:
                    Player.Row = newRow;
                    Player.Column = newCol;
                    moved = true;
                    break;

                case CellType.Switch:
                    Player.Row = newRow;
                    Player.Column = newCol;
                    CurrentLevel.ToggleSwitch(newRow, newCol);
                    moved = true;
                    break;

                case CellType.Key:
                    Player.Keys++;
                    Player.Row = newRow;
                    Player.Column = newCol;
                    CurrentLevel.SetCell(newRow, newCol, CellType.Floor);
                    moved = true;
                    break;

                case CellType.Door:
                    if (CurrentLevel.DoorStates.ContainsKey((newRow, newCol)) &&
                        CurrentLevel.DoorStates[(newRow, newCol)])
                    {
                        Player.Row = newRow;
                        Player.Column = newCol;
                        moved = true;
                    }
                    else if (Player.Keys > 0)
                    {
                        Player.Keys--;
                        CurrentLevel.DoorStates[(newRow, newCol)] = true;
                        Player.Row = newRow;
                        Player.Column = newCol;
                        moved = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case CellType.Exit:
                    if (CurrentLevel.AreAllSwitchesActive())
                    {
                        Player.Row = newRow;
                        Player.Column = newCol;
                        IsLevelComplete = true;
                        LevelChanged?.Invoke();
                        moved = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;

                default:
                    return false;
            }

            if (moved)
            {
                Player.Moves++;
            }

            return moved;
        }

        public void NextLevel()
        {
            if (_currentLevelIndex == _levels.Count - 1)
                GenerateNewLevel();

            _currentLevelIndex++;
            LoadLevel(_currentLevelIndex);
        }

        public void RestartGame()
        {
            _levels.Clear();
            GenerateNewLevel();
            StartGame();
        }
    }
}