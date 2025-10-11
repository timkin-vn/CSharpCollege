using System;
using System.Windows.Media;

namespace StepByStepPacman
{
    public class Ghost
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; }
        private string name;
        private Random random;

        public Ghost(int startX, int startY, Color color, string name)
        {
            X = startX;
            Y = startY;
            Color = color;
            this.name = name;
            this.random = new Random();
        }

        public void Move(int[,] gameBoard, Pacman pacman)
        {
            // 70% случайное движение, 30% движение к Пакману
            if (random.Next(100) < 70)
            {
                RandomMove(gameBoard);
            }
            else
            {
                MoveTowardsPacman(gameBoard, pacman);
            }
        }

        private void RandomMove(int[,] gameBoard)
        {
            int[][] directions = new int[][]
            {
                new int[] { 0, -1 },  // вверх
                new int[] { 1, 0 },   // вправо
                new int[] { 0, 1 },   // вниз
                new int[] { -1, 0 }   // влево
            };

            var shuffledDirections = ShuffleDirections(directions);

            foreach (var dir in shuffledDirections)
            {
                int newX = X + dir[0];
                int newY = Y + dir[1];

                if (IsValidMove(newX, newY, gameBoard))
                {
                    X = newX;
                    Y = newY;
                    return;
                }
            }
        }

        private void MoveTowardsPacman(int[,] gameBoard, Pacman pacman)
        {
            int[][] directions = new int[][]
            {
                new int[] { 0, -1 },  // вверх
                new int[] { 1, 0 },   // вправо
                new int[] { 0, 1 },   // вниз
                new int[] { -1, 0 }   // влево
            };

            int[] bestDir = directions[0];
            double bestDistance = double.MaxValue;

            foreach (var dir in directions)
            {
                int newX = X + dir[0];
                int newY = Y + dir[1];

                if (IsValidMove(newX, newY, gameBoard))
                {
                    double distance = Math.Sqrt(Math.Pow(newX - pacman.X, 2) + Math.Pow(newY - pacman.Y, 2));
                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
                        bestDir = dir;
                    }
                }
            }

            // Проверяем, что можем сделать ход
            if (IsValidMove(X + bestDir[0], Y + bestDir[1], gameBoard))
            {
                X += bestDir[0];
                Y += bestDir[1];
            }
        }

        private bool IsValidMove(int x, int y, int[,] gameBoard)
        {
            // Проверка выхода за границы
            if (x < 0 || x >= gameBoard.GetLength(1) ||
                y < 0 || y >= gameBoard.GetLength(0))
                return false;

            // Проверка стены
            if (gameBoard[y, x] == 0)
                return false;

            // Проверка, что клетка не занята другим призраком
            if (gameBoard[y, x] == 4 || gameBoard[y, x] < 0)
                return false;

            return true;
        }

        private int[][] ShuffleDirections(int[][] directions)
        {
            int[][] result = new int[directions.Length][];
            Array.Copy(directions, result, directions.Length);

            // Перемешивание
            for (int i = 0; i < result.Length; i++)
            {
                int j = random.Next(i, result.Length);
                var temp = result[i];
                result[i] = result[j];
                result[j] = temp;
            }

            return result;
        }
    }
}