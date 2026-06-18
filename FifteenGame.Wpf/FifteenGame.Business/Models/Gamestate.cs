using System;
using System.Collections.Generic;
using System.Linq;
using Pacman.Business.Models;

namespace Pacman.Business
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

        public bool IsGameOver
        {
            get
            {
                // ПОБЕДА: Собраны все точки.
                if (CollectedDots >= TotalDots && TotalDots > 0)
                {
                    return true;
                }
                // ПРОИГРЫШ: Закончились жизни
                if (Lives <= 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool GameRunning { get; set; }
        public int Level { get; set; } = 1;

        public GameState()
        {
            Initialize();
        }

        private void Initialize()
        {
            Score = 0;
            Lives = 3;
            CollectedDots = 0;

            // 1. Инициализируем доску
            InitializeBoard();

            // 2. Считаем общее количество точек (теперь публичный метод)
            CountTotalDots();

            // 3. Инициализируем персонажей
            InitializePlayer();
            InitializeGhosts();

            GameRunning = true;
        }

        public void Restart()
        {
            Initialize();
        }

        public void SetInitialState(GameStateData data)
        {
            Score = data.Score;
            Lives = data.Lives;
            Level = data.Level;
            CollectedDots = data.CollectedDots;

            // При загрузке TotalDots устанавливается в LoadState в ViewModel
            

            GameRunning = true;
        }

        public void EatDot(int x, int y)
        {
            if (!IsWithinBounds(x, y)) return;

            int cell = GameBoard[y, x];

            if (cell == 2) // Обычная точка
            {
                Score += 10;
                CollectedDots++;
                GameBoard[y, x] = 1;
            }
            else if (cell == 3) // Энерджайзер
            {
                Score += 50;
                CollectedDots++;
                GameBoard[y, x] = 1;
                
            }
        }

        // -----------------------------------------------------------------
        
        
        
        public void CountTotalDots()
        {
            // Сбрасываем счетчик перед пересчетом
            TotalDots = 0;

            // Перебираем игровое поле
            for (int y = 0; y < GRID_HEIGHT; y++)
            {
                for (int x = 0; x < GRID_WIDTH; x++)
                {
                    int cell = GameBoard[y, x];

                    // Проверяем, является ли ячейка точкой (2) или энерджайзером (3)
                    // ИЛИ
                    // Проверяем, является ли ячейка скрытой точкой/энерджайзером (<0)
                    if (cell == 2 || cell == 3 || cell < 0) // <-- Проблема, вероятно, здесь
                    {
                        TotalDots++;
                    }
                }
            }
        }
        

        private void InitializeBoard()
        {
            GameBoard = new int[GRID_HEIGHT, GRID_WIDTH];

            // 0=Стена, 1=Путь (Пусто), 2=Точка, 3=Энерджайзер
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
        }

        public bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < GRID_WIDTH && y >= 0 && y < GRID_HEIGHT;
        }
    }
}