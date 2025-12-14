using System;

namespace StepByStepPacman.Business.Models
{
    public class Ghost : GameObject
    {
        
        public ColorType Color { get; set; }

        private string name;
        private Random random;

        public Ghost(int x, int y, int size, ColorType color, string name)
            : base(x, y, size)
        {
            
            Color = color;
            this.name = name;
            this.random = new Random();
        }

        public void Move(int[,] gameBoard, PacmanPlayer pacman)
        {
            // 70% шанс случайного движения, 30% шанс преследования
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
                new int[] { 0, -1 }, // Up
                new int[] { 1, 0 },  // Right
                new int[] { 0, 1 },  // Down
                new int[] { -1, 0 }  // Left
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

        private void MoveTowardsPacman(int[,] gameBoard, PacmanPlayer pacman)
        {
            int[][] directions = new int[][]
            {
                new int[] { 0, -1 }, // Up
                new int[] { 1, 0 },  // Right
                new int[] { 0, 1 },  // Down
                new int[] { -1, 0 }  // Left
            };

            int[] bestDir = directions[0];
            double bestDistance = double.MaxValue;

            foreach (var dir in directions)
            {
                int newX = X + dir[0];
                int newY = Y + dir[1];

                if (IsValidMove(newX, newY, gameBoard))
                {
                    // Вычисляем евклидово расстояние
                    double distance = Math.Sqrt(Math.Pow(newX - pacman.X, 2) + Math.Pow(newY - pacman.Y, 2));

                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
                        bestDir = dir;
                    }
                }
            }

            // Выполняем движение в лучшем направлении, если оно валидно
            if (IsValidMove(X + bestDir[0], Y + bestDir[1], gameBoard))
            {
                X += bestDir[0];
                Y += bestDir[1];
            }
        }

        private bool IsValidMove(int x, int y, int[,] gameBoard)
        {
            // Проверка границ
            if (x < 0 || x >= gameBoard.GetLength(1) ||
                y < 0 || y >= gameBoard.GetLength(0))
                return false;

            // 0 - стена
            if (gameBoard[y, x] == 0)
                return false;

            // 4 - призрачный дом/ворота, < 0 - временный код (например, скрытая точка)
            // Призраки могут ходить по 2 (точка), 3 (энерджайзер), и 1 (пустота).
            // Предполагаем, что 4 - это ворота призрачного дома, и мы не хотим, чтобы они могли случайно выйти/войти.
            if (gameBoard[y, x] == 4 || gameBoard[y, x] < 0)
                return false;

            return true;
        }

        private int[][] ShuffleDirections(int[][] directions)
        {
            int[][] result = new int[directions.Length][];
            // Создаем копию массива, чтобы не менять исходный массив 'directions'
            Array.Copy(directions, result, directions.Length);

            // Алгоритм Фишера-Йейтса
            for (int i = 0; i < result.Length; i++)
            {
                // Выбираем случайный элемент от i до конца
                int j = random.Next(i, result.Length);

                // Меняем местами
                var temp = result[i];
                result[i] = result[j];
                result[j] = temp;
            }

            return result;
        }
    }
}