using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace StepByStepPacman
{
    public partial class MainWindow : Window
    {
        private const int TILE_SIZE = 35;
        private const int GRID_WIDTH = 19;
        private const int GRID_HEIGHT = 21;

        private Pacman pacman;
        private List<Ghost> ghosts;
        private int score = 0;
        private int lives = 3;
        private int totalDots = 0;
        private int collectedDots = 0;

        private int[,] gameBoard;
        private bool gameRunning = true;

        // Кисти для отрисовки
        private SolidColorBrush wallBrush = new SolidColorBrush(Color.FromRgb(0, 0, 139));
        private SolidColorBrush dotBrush = new SolidColorBrush(Colors.White);
        private SolidColorBrush energizerBrush = new SolidColorBrush(Colors.White);
        private SolidColorBrush pacmanBrush = new SolidColorBrush(Colors.Yellow);

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Console.WriteLine("Инициализация игры...");
                InitializeGame();
                DrawGame();
                Console.WriteLine("Игра готова!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации: {ex.Message}");
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private void InitializeGame()
        {
            // Очистка канваса
            GameCanvas.Children.Clear();

            // Сброс переменных
            score = 0;
            lives = 3;
            collectedDots = 0;
            gameRunning = true;
            GameOverPanel.Visibility = Visibility.Collapsed;

            // Инициализация игрового поля
            InitializeBoard();

            // Создание Пакмана
            pacman = new Pacman(1, 1);

            // Проверяем границы перед установкой Пакмана
            if (IsWithinBounds(pacman.X, pacman.Y))
            {
                gameBoard[pacman.Y, pacman.X] = 5;
            }

            // Создание призраков
            ghosts = new List<Ghost>
            {
                new Ghost(9, 8, Colors.Red, "Blinky"),
                new Ghost(8, 9, Colors.Pink, "Pinky"),
                new Ghost(9, 9, Colors.Cyan, "Inky"),
                new Ghost(10, 9, Colors.Orange, "Clyde")
            };

            // Размещение призраков на карте с проверкой границ
            foreach (var ghost in ghosts)
            {
                if (IsWithinBounds(ghost.X, ghost.Y))
                {
                    gameBoard[ghost.Y, ghost.X] = 4;
                }
            }

            // Подсчет общего количества точек
            CountTotalDots();
            UpdateHUD();
        }

        // Метод для проверки границ массива
        private bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < GRID_WIDTH && y >= 0 && y < GRID_HEIGHT;
        }

        private void InitializeBoard()
        {
            // Создаем массив правильного размера
            gameBoard = new int[GRID_HEIGHT, GRID_WIDTH];

            
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
                        gameBoard[y, x] = initialBoard[y, x];
                    }
                    else
                    {
                        gameBoard[y, x] = 0; 
                    }
                }
            }

            Console.WriteLine("Карта инициализирована. Проверка точек...");
            int dotCount = 0;
            for (int y = 0; y < GRID_HEIGHT; y++)
            {
                for (int x = 0; x < GRID_WIDTH; x++)
                {
                    if (gameBoard[y, x] == 2 || gameBoard[y, x] == 3)
                        dotCount++;
                }
            }
            Console.WriteLine($"На карте найдено точек: {dotCount}");


        }

        private void CountTotalDots()
        {
            totalDots = 0;
            for (int y = 0; y < GRID_HEIGHT; y++)
            {
                for (int x = 0; x < GRID_WIDTH; x++)
                {
                    if (gameBoard[y, x] == 2 || gameBoard[y, x] == 3)
                    {
                        totalDots++;
                    }
                }
            }
            Console.WriteLine($"Всего точек на карте: {totalDots}");
        }

        private void DrawGame()
        {
            GameCanvas.Children.Clear();

            // Отрисовка игрового поля с проверкой границ
            for (int y = 0; y < GRID_HEIGHT; y++)
            {
                for (int x = 0; x < GRID_WIDTH; x++)
                {
                    if (y < gameBoard.GetLength(0) && x < gameBoard.GetLength(1))
                    {
                        DrawCell(x, y, gameBoard[y, x]);
                    }
                }
            }

            // Отрисовка Пакмана
            DrawPacman();

            // Отрисовка призраков
            foreach (var ghost in ghosts)
            {
                DrawGhost(ghost);
            }
        }

        private void DrawCell(int x, int y, int cellType)
        {
           
            if (cellType < 0)
            {
                int originalValue = -cellType;

                
                switch (originalValue)
                {
                    case 2: // Точка под призраком
                        var hiddenDot = new Ellipse
                        {
                            Width = 6,
                            Height = 6,
                            Fill = Brushes.LightGray 
                        };
                        Canvas.SetLeft(hiddenDot, x * TILE_SIZE + TILE_SIZE / 2 - 3);
                        Canvas.SetTop(hiddenDot, y * TILE_SIZE + TILE_SIZE / 2 - 3);
                        GameCanvas.Children.Add(hiddenDot);
                        break;

                    case 3: 
                        var hiddenEnergizer = new Ellipse
                        {
                            Width = 16,
                            Height = 16,
                            Fill = Brushes.LightGray 
                        };
                        Canvas.SetLeft(hiddenEnergizer, x * TILE_SIZE + TILE_SIZE / 2 - 8);
                        Canvas.SetTop(hiddenEnergizer, y * TILE_SIZE + TILE_SIZE / 2 - 8);
                        GameCanvas.Children.Add(hiddenEnergizer);
                        break;
                }
                return;
            }

            
            switch (cellType)
            {
                case 0: 
                    var wallRect = new Rectangle
                    {
                        Width = TILE_SIZE,
                        Height = TILE_SIZE,
                        Fill = wallBrush,
                        Stroke = Brushes.Blue,
                        StrokeThickness = 1
                    };
                    Canvas.SetLeft(wallRect, x * TILE_SIZE);
                    Canvas.SetTop(wallRect, y * TILE_SIZE);
                    GameCanvas.Children.Add(wallRect);
                    break;

                case 2: 
                    var dotEllipse = new Ellipse
                    {
                        Width = 6,
                        Height = 6,
                        Fill = dotBrush
                    };
                    Canvas.SetLeft(dotEllipse, x * TILE_SIZE + TILE_SIZE / 2 - 3);
                    Canvas.SetTop(dotEllipse, y * TILE_SIZE + TILE_SIZE / 2 - 3);
                    GameCanvas.Children.Add(dotEllipse);
                    break;

                case 3: 
                    var energizerEllipse = new Ellipse
                    {
                        Width = 16,
                        Height = 16,
                        Fill = energizerBrush
                    };
                    Canvas.SetLeft(energizerEllipse, x * TILE_SIZE + TILE_SIZE / 2 - 8);
                    Canvas.SetTop(energizerEllipse, y * TILE_SIZE + TILE_SIZE / 2 - 8);
                    GameCanvas.Children.Add(energizerEllipse);
                    break;
            }
        }

        private void DrawPacman()
        {
            if (IsWithinBounds(pacman.X, pacman.Y))
            {
                var pacmanEllipse = new Ellipse
                {
                    Width = TILE_SIZE - 4,
                    Height = TILE_SIZE - 4,
                    Fill = pacmanBrush
                };
                Canvas.SetLeft(pacmanEllipse, pacman.X * TILE_SIZE + 2);
                Canvas.SetTop(pacmanEllipse, pacman.Y * TILE_SIZE + 2);
                GameCanvas.Children.Add(pacmanEllipse);
            }
        }

        private void DrawGhost(Ghost ghost)
        {
            if (IsWithinBounds(ghost.X, ghost.Y))
            {
                var ghostEllipse = new Ellipse
                {
                    Width = TILE_SIZE - 4,
                    Height = TILE_SIZE - 4,
                    Fill = new SolidColorBrush(ghost.Color)
                };
                Canvas.SetLeft(ghostEllipse, ghost.X * TILE_SIZE + 2);
                Canvas.SetTop(ghostEllipse, ghost.Y * TILE_SIZE + 2);
                GameCanvas.Children.Add(ghostEllipse);
            }
        }

        private void UpdateHUD()
        {
            ScoreText.Text = score.ToString();
            LivesText.Text = lives.ToString();
            DotsText.Text = $"{collectedDots}/{totalDots}";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!gameRunning) return;

            
            int oldX = pacman.X;
            int oldY = pacman.Y;

            bool moved = false;

            switch (e.Key)
            {
                case Key.Left:
                    moved = pacman.TryMove(-1, 0, gameBoard);
                    break;
                case Key.Right:
                    moved = pacman.TryMove(1, 0, gameBoard);
                    break;
                case Key.Up:
                    moved = pacman.TryMove(0, -1, gameBoard);
                    break;
                case Key.Down:
                    moved = pacman.TryMove(0, 1, gameBoard);
                    break;
            }

            if (moved)
            {
                // Очищаем старую позицию Пакмана
                if (IsWithinBounds(oldX, oldY))
                {
                    
                    gameBoard[oldY, oldX] = 1;
                }

                
                CheckDotCollection();

                // Если Пакман не собрал точку, ставим его на новую позицию
                if (IsWithinBounds(pacman.X, pacman.Y) && gameBoard[pacman.Y, pacman.X] != 5)
                {
                    gameBoard[pacman.Y, pacman.X] = 5;
                }

                // Двигаем призраков
                MoveGhosts();

                // Проверяем столкновения
                CheckGhostCollisions();

                // Проверяем условия завершения игры
                CheckGameEnd();

                // Перерисовываем игру
                DrawGame();
                UpdateHUD();
            }
        }

        private void CheckDotCollection()
        {
            if (!IsWithinBounds(pacman.X, pacman.Y)) return;

            int cellValue = gameBoard[pacman.Y, pacman.X];

            // Если клетка содержит призрака, который скрывает точку
            if (cellValue < 0)
            {
                // Восстанавливаем исходное значение точки
                int originalValue = -cellValue;

                if (originalValue == 2) 
                {
                    score += 10;
                    collectedDots++;
                    gameBoard[pacman.Y, pacman.X] = 5; 
                    Console.WriteLine($"Собрана точка из-под призрака! Счет: {score}, Всего: {collectedDots}/{totalDots}");
                }
                else if (originalValue == 3) 
                {
                    score += 50;
                    collectedDots++;
                    gameBoard[pacman.Y, pacman.X] = 5; 
                    Console.WriteLine($"Собран энерджайзер из-под призрака! Счет: {score}, Всего: {collectedDots}/{totalDots}");
                }
            }
            else if (cellValue == 2) 
            {
                score += 10;
                collectedDots++;
                gameBoard[pacman.Y, pacman.X] = 5; 
                Console.WriteLine($"Собрана точка! Счет: {score}, Всего: {collectedDots}/{totalDots}");
            }
            else if (cellValue == 3) 
            {
                score += 50;
                collectedDots++;
                gameBoard[pacman.Y, pacman.X] = 5; 
                Console.WriteLine($"Собран энерджайзер! Счет: {score}, Всего: {collectedDots}/{totalDots}");
            }
        }

        private void MoveGhosts()
        {
            // Восстанавливаем точки под призраками перед их движением
            RestorePointsUnderGhosts();

            // Убираем старых призраков с карты
            foreach (var ghost in ghosts)
            {
                if (IsWithinBounds(ghost.X, ghost.Y) && gameBoard[ghost.Y, ghost.X] == 4)
                {
                    gameBoard[ghost.Y, ghost.X] = 1;
                }
            }

            // Двигаем призраков
            foreach (var ghost in ghosts)
            {
                ghost.Move(gameBoard, pacman);

                // Размещаем призрака на новой позиции
                if (IsWithinBounds(ghost.X, ghost.Y))
                {
                    int previousCellValue = gameBoard[ghost.Y, ghost.X];

                    // Если на клетке была точка или энерджайзер, временно сохраняем это
                    if (previousCellValue == 2 || previousCellValue == 3)
                    {
                        gameBoard[ghost.Y, ghost.X] = -previousCellValue;
                    }
                    else
                    {
                        gameBoard[ghost.Y, ghost.X] = 4;
                    }
                }
            }
        }

        private void CheckGhostCollisions()
        {
            foreach (var ghost in ghosts)
            {
                if (ghost.X == pacman.X && ghost.Y == pacman.Y)
                {
                    lives--;
                    if (lives <= 0)
                    {
                        gameRunning = false;
                        ShowGameOver(false);
                    }
                    else
                    {
                        // Респавн Пакмана
                        if (IsWithinBounds(pacman.X, pacman.Y))
                            gameBoard[pacman.Y, pacman.X] = 1;

                        // Восстанавливаем точки перед респавном
                        RestorePointsUnderGhosts();

                        pacman = new Pacman(1, 1);
                        if (IsWithinBounds(pacman.X, pacman.Y))
                            gameBoard[pacman.Y, pacman.X] = 5;
                    }
                    break;
                }
            }
        }

        private void CheckGameEnd()
        {
            if (collectedDots >= totalDots)
            {
                gameRunning = false;
                ShowGameOver(true);
                Console.WriteLine("ПОБЕДА! Все точки собраны!");
            }

            // Добавьте также отладочный вывод для отслеживания прогресса
            if (collectedDots > 0)
            {
                Console.WriteLine($"Прогресс: {collectedDots}/{totalDots} точек");
            }
        }
        private void RestorePointsUnderGhosts()
        {
            // Восстанавливаем точки под призраками
            foreach (var ghost in ghosts)
            {
                if (IsWithinBounds(ghost.X, ghost.Y) && gameBoard[ghost.Y, ghost.X] < 0)
                {
                    // Временно убираем призрака, чтобы восстановить точку
                    int cellValue = gameBoard[ghost.Y, ghost.X];
                    gameBoard[ghost.Y, ghost.X] = -cellValue; // Восстанавливаем исходное значение
                }
            }
        }
        private void ShowGameOver(bool win)
        {
            GameOverText.Text = win ? "ПОБЕДА! 🎉" : "ИГРА ОКОНЧЕНA";
            GameOverText.Foreground = win ? Brushes.Green : Brushes.Red;
            GameOverPanel.Visibility = Visibility.Visible;
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
            DrawGame();
        }

        


    }
}