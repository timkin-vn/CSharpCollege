using System.Collections.Generic;
using StepByStepPacman.Business.Models;

namespace StepByStepPacman.Business
{
    public class GameState
    {
        public const int TILE_SIZE = 35;
        public const int GRID_WIDTH = 19;
        public const int GRID_HEIGHT = 21;

        public PacmanPlayer Player { get; set; }
        public List<Ghost> Ghosts { get; set; }
        public int[,] GameBoard { get; set; }
        public int Score { get; set; }
        public int Lives { get; set; }
        public int TotalDots { get; set; }
        public int CollectedDots { get; set; }
        public bool IsGameOver { get; set; }
        public bool GameRunning { get; set; }

        public GameState()
        {
            Initialize();
        }

        private void Initialize()
        {
            Score = 0;
            Lives = 3;
            CollectedDots = 0;
            IsGameOver = false;
            GameRunning = true;

            InitializeBoard();
            InitializePlayer();
            InitializeGhosts();
            CountTotalDots();
        }

        private void InitializeBoard()
        {
            GameBoard = new int[GRID_HEIGHT, GRID_WIDTH];

            int[,] initialBoard = {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,0},
                {0,3,0,0,2,0,0,0,2,0,2,0,0,0,2,0,0,3,0},
                {0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0},
                {0,2,0,0,2,0,2,0,0,0,0,0,2,0,2,0,0,2,0},
                {0,2,2,2,2,0,2,2,2,0,2,2,2,0,2,2,2,2,0},
                {0,0,0,0,2,0,0,0,1,0,1,0,0,0,2,0,0,0,0},
                {0,0,0,0,2,0,1,1,1,1,1,1,1,0,2,0,0,0,0},
                {1,1,1,0,2,0,1,0,0,0,0,0,1,0,2,0,1,1,1},
                {0,0,0,0,2,0,1,0,0,0,0,0,1,0,2,0,0,0,0},
                {0,0,0,0,2,0,1,1,1,1,1,1,1,0,2,0,0,0,0},
                {0,0,0,0,2,0,1,0,0,0,0,0,1,0,2,0,0,0,0},
                {0,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,0},
                {0,2,0,0,2,0,0,0,2,0,2,0,0,0,2,0,0,2,0},
                {0,3,2,0,2,2,2,2,2,1,2,2,2,2,2,0,2,3,0},
                {0,0,2,0,2,0,2,0,0,0,0,0,2,0,2,0,2,0,0},
                {0,2,2,2,2,0,2,2,2,0,2,2,2,0,2,2,2,2,0},
                {0,2,0,0,0,0,0,0,2,0,2,0,0,0,0,0,0,2,0},
                {0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
            };

            for (int y = 0; y < GRID_HEIGHT; y++)
            {
                for (int x = 0; x < GRID_WIDTH; x++)
                {
                    if (y < initialBoard.GetLength(0) && x < initialBoard.GetLength(1))
                    {
                        GameBoard[y, x] = initialBoard[y, x];
                    }
                    else
                    {
                        GameBoard[y, x] = 0;
                    }
                }
            }
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
                new Ghost(9, 8, TILE_SIZE - 4, ColorType.Red, "Blinky"),
                new Ghost(8, 9, TILE_SIZE - 4, ColorType.Pink, "Pinky"),
                new Ghost(9, 9, TILE_SIZE - 4, ColorType.Cyan, "Inky"),
                new Ghost(10, 9, TILE_SIZE - 4, ColorType.Orange, "Clyde")
            };

            foreach (var ghost in Ghosts)
            {
                if (IsWithinBounds(ghost.X, ghost.Y))
                {
                    GameBoard[ghost.Y, ghost.X] = 4;
                }
            }
        }

        private void CountTotalDots()
        {
            TotalDots = 0;
            for (int y = 0; y < GRID_HEIGHT; y++)
            {
                for (int x = 0; x < GRID_WIDTH; x++)
                {
                    if (GameBoard[y, x] == 2 || GameBoard[y, x] == 3)
                    {
                        TotalDots++;
                    }
                }
            }
        }

        public bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < GRID_WIDTH && y >= 0 && y < GRID_HEIGHT;
        }

        public void Reset()
        {
            Initialize();
        }
    }
}