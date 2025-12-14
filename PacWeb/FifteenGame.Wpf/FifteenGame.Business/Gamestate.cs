
using System.Collections.Generic;
using System.Linq;

namespace StepByStepPacman.Business.Models
{
    public class GameState
    {
        
        public const int TILE_SIZE = 35;
        public const int GRID_WIDTH = 19;
        public const int GRID_HEIGHT = 21; 

       
        public int Score { get; set; }
        public int Lives { get; set; }
        public int CollectedDots { get; set; }
        public int TotalDots { get; private set; }
        public bool GameRunning { get; set; }
        public bool IsGameOver { get; set; }
        public bool Won { get; set; }

       
        public int[,] GameBoard { get; private set; }
        public PacmanPlayer Player { get; set; }
        public List<Ghost> Ghosts { get; set; }

        public GameState() => Initialize();

        private void Initialize()
        {
            Score = 0;
            Lives = 3;
            CollectedDots = 0;
            GameRunning = true;
            IsGameOver = false;
            Won = false;

            
            GameBoard = CreateInitialBoard();
            InitializePlayer();
            InitializeGhosts();
            TotalDots = CalculateTotalDots();
        }

        public void Reset() => Initialize();

        private int[,] CreateInitialBoard()
        {
            
            return new int[GRID_HEIGHT, GRID_WIDTH]
            {
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // 0
                { 0,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,0 }, // 1
                { 0,3,0,0,2,0,0,0,2,0,2,0,0,0,2,0,0,3,0 }, // 2
                { 0,2,0,0,2,0,0,0,2,0,2,0,0,0,2,0,0,2,0 }, // 3
                { 0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0 }, // 4
                { 0,2,0,0,2,0,2,0,0,0,0,0,2,0,2,0,0,2,0 }, // 5
                { 0,2,2,2,2,0,2,2,2,0,2,2,2,0,2,2,2,2,0 }, // 6
                { 0,0,0,0,2,0,0,0,1,1,1,0,0,0,2,0,0,0,0 }, // 7
                { 0,0,0,0,2,0,1,1,1,1,1,1,1,0,2,0,0,0,0 }, // 8
                { 0,0,0,0,2,0,1,1,1,1,1,1,1,0,2,0,0,0,0 }, // 9
                { 0,0,0,0,2,0,1,1,1,1,1,1,1,0,2,0,0,0,0 }, // 10
                { 0,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,0 }, // 11
                { 0,2,0,0,2,0,0,0,2,0,2,0,0,0,2,0,0,2,0 }, // 12
                { 0,3,2,0,2,2,2,2,2,1,2,2,2,2,2,0,2,3,0 }, // 13
                { 0,0,2,0,2,0,2,0,0,0,0,0,2,0,2,0,2,0,0 }, // 14
                { 0,2,2,2,2,0,2,2,2,0,2,2,2,0,2,2,2,2,0 }, // 15
                { 0,2,0,0,0,0,0,0,2,0,2,0,0,0,0,0,0,2,0 }, // 16
                { 0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0 }, // 17
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // 18
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // 19
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  // 20
            };
        }

        private void InitializePlayer()
        {
            Player = new PacmanPlayer(1, 1, TILE_SIZE - 4);
            if (IsWithinBounds(Player.X, Player.Y))
            {
                GameBoard[Player.Y, Player.X] = 5;
            }
        }

        
        private void InitializeGhosts()
        {
            Ghosts = new List<Ghost>
            {
                new Ghost(9, 8, TILE_SIZE - 4, "Red"),
                new Ghost(8, 9, TILE_SIZE - 4, "Pink"),
                new Ghost(9, 9, TILE_SIZE - 4, "Cyan"),
                new Ghost(10, 9, TILE_SIZE - 4, "Orange")
            };
        }
        private int CalculateTotalDots()
        {
            return GameBoard.Cast<int>().Count(c => c == 2 || c == 3);
        }
        public bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < GRID_WIDTH && y >= 0 && y < GRID_HEIGHT;
        }
    }
}