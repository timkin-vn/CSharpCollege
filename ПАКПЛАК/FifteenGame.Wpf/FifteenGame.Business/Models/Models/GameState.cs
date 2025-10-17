using System.Collections.Generic;

namespace Pacmen.Business.Models
{
    public class GameState
    {
        public int[,] Maze { get; private set; } 
        public (int X, int Y) PacmanPosition { get; set; }
        public List<(int X, int Y)> GhostPositions { get; set; } 
        public int Score { get; set; }
        public int CellSize { get; } = 30; 
        public bool IsGameOver { get; set; }
        public bool HasWon { get; set; }

        public GameState()
        {
            
            Maze = new int[,]
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 1, 2, 2, 2, 2, 2, 2, 1, 0 },
                { 0, 2, 0, 0, 2, 0, 0, 2, 2, 0 },
                { 0, 2, 2, 2, 2, 2, 2, 2, 2, 0 },
                { 0, 2, 0, 0, 2, 0, 2, 0, 2, 0 },
                { 0, 2, 2, 2, 2, 0, 2, 2, 2, 0 },
                { 0, 2, 0, 0, 2, 0, 0, 0, 2, 0 },
                { 0, 2, 2, 2, 2, 2, 2, 2, 2, 0 },
                { 0, 1, 2, 2, 2, 2, 2, 2, 1, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            PacmanPosition = (1, 1); // Начальная позиция Pac-Man
            GhostPositions = new List<(int X, int Y)> { (8, 8), (8, 1) }; // Два призрака
            Score = 0;
            IsGameOver = false;
            HasWon = false;
        }

        public int GetCoinCount()
        {
            int count = 0;
            for (int i = 0; i < Maze.GetLength(0); i++)
                for (int j = 0; j < Maze.GetLength(1); j++)
                    if (Maze[i, j] == 2) count++;
            return count;
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}