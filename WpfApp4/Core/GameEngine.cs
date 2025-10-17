using System;
using System.Collections.Generic;

namespace WpfApp4.Core
{
    public enum CellType
    {
        Wall,
        Path,
        Exit
    }

    public class MoveResult
    {
        public bool IsSuccess { get; }
        public bool IsGameCompleted { get; }
        public string Message { get; }

        private MoveResult(bool isSuccess, bool isGameCompleted, string message)
        {
            IsSuccess = isSuccess;
            IsGameCompleted = isGameCompleted;
            Message = message;
        }

        public static MoveResult CreateSuccess(bool isGameCompleted, string message)
        {
            return new MoveResult(true, isGameCompleted, message);
        }

        public static MoveResult CreateFailed(string message)
        {
            return new MoveResult(false, false, message);
        }
    }

    public class GameEngine
    {
        private const int MAZE_SIZE = 15;
        private CellType[,] maze;
        private int playerRow, playerCol;
        private int exitRow, exitCol;
        private int movesCount;
        private bool gameFinished;
        private Random random;

        public int MazeSize => MAZE_SIZE;
        public int PlayerRow => playerRow;
        public int PlayerCol => playerCol;
        public int ExitRow => exitRow;
        public int ExitCol => exitCol;
        public int MovesCount => movesCount;
        public bool IsGameFinished => gameFinished;
        public CellType[,] Maze => maze;

        public GameEngine()
        {
            random = new Random();
            InitializeGame();
        }

        public void InitializeGame()
        {
            maze = new CellType[MAZE_SIZE, MAZE_SIZE];
            movesCount = 0;
            gameFinished = false;

            GenerateMaze();
            PlacePlayerAndExit();
        }

        private void GenerateMaze()
        {

            for (int i = 0; i < MAZE_SIZE; i++)
            {
                for (int j = 0; j < MAZE_SIZE; j++)
                {
                    maze[i, j] = CellType.Wall;
                }
            }
            GenerateMazeDFS(1, 1);
            for (int i = 0; i < MAZE_SIZE; i++)
            {
                maze[i, 0] = CellType.Wall;
                maze[i, MAZE_SIZE - 1] = CellType.Wall;
                maze[0, i] = CellType.Wall;
                maze[MAZE_SIZE - 1, i] = CellType.Wall;
            }
            exitRow = MAZE_SIZE - 2;
            exitCol = MAZE_SIZE - 2;
            maze[exitRow, exitCol] = CellType.Exit;
        }

        private void GenerateMazeDFS(int row, int col)
        {
            maze[row, col] = CellType.Path;

            var directions = new List<(int, int)> { (0, 2), (2, 0), (0, -2), (-2, 0) };
            Shuffle(directions);

            foreach (var (dr, dc) in directions)
            {
                int newRow = row + dr;
                int newCol = col + dc;

                if (newRow > 0 && newRow < MAZE_SIZE - 1 &&
                    newCol > 0 && newCol < MAZE_SIZE - 1 &&
                    maze[newRow, newCol] == CellType.Wall)
                {
                    maze[row + dr / 2, col + dc / 2] = CellType.Path;
                    GenerateMazeDFS(newRow, newCol);
                }
            }
        }

        private void Shuffle<T>(IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        private void PlacePlayerAndExit()
        {
            playerRow = 1;
            playerCol = 1;
            exitRow = MAZE_SIZE - 2;
            exitCol = MAZE_SIZE - 2;
        }

        public MoveResult MovePlayer(int deltaRow, int deltaCol)
        {
            if (gameFinished)
                return MoveResult.CreateFailed("Игра уже завершена");

            int newRow = playerRow + deltaRow;
            int newCol = playerCol + deltaCol;

            if (newRow >= 0 && newRow < MAZE_SIZE &&
                newCol >= 0 && newCol < MAZE_SIZE &&
                maze[newRow, newCol] != CellType.Wall)
            {
                playerRow = newRow;
                playerCol = newCol;
                movesCount++;

                if (playerRow == exitRow && playerCol == exitCol)
                {
                    gameFinished = true;
                    return MoveResult.CreateSuccess(true, "Поздравляем! Вы достигли выхода!");
                }

                return MoveResult.CreateSuccess(false, "Ход выполнен успешно");
            }

            return MoveResult.CreateFailed("Невозможно переместиться в эту клетку");
        }

        public void Restart()
        {
            InitializeGame();
        }

        public CellType GetCellType(int row, int col)
        {
            if (row < 0 || row >= MAZE_SIZE || col < 0 || col >= MAZE_SIZE)
                return CellType.Wall;

            return maze[row, col];
        }

        public bool IsValidMove(int deltaRow, int deltaCol)
        {
            int newRow = playerRow + deltaRow;
            int newCol = playerCol + deltaCol;

            return newRow >= 0 && newRow < MAZE_SIZE &&
                   newCol >= 0 && newCol < MAZE_SIZE &&
                   maze[newRow, newCol] != CellType.Wall;
        }
    }
}