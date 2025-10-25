using System;

namespace StepByStepPacman.Business.Models
{
    public class Ghost : GameObject
    {
        public ColorType Color { get; }
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
                new int[] { 0, -1 },
                new int[] { 1, 0 },
                new int[] { 0, 1 },
                new int[] { -1, 0 }
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
                new int[] { 0, -1 },
                new int[] { 1, 0 },
                new int[] { 0, 1 },
                new int[] { -1, 0 }
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

            if (IsValidMove(X + bestDir[0], Y + bestDir[1], gameBoard))
            {
                X += bestDir[0];
                Y += bestDir[1];
            }
        }

        private bool IsValidMove(int x, int y, int[,] gameBoard)
        {
            if (x < 0 || x >= gameBoard.GetLength(1) ||
                y < 0 || y >= gameBoard.GetLength(0))
                return false;

            if (gameBoard[y, x] == 0)
                return false;

            if (gameBoard[y, x] == 4 || gameBoard[y, x] < 0)
                return false;

            return true;
        }

        private int[][] ShuffleDirections(int[][] directions)
        {
            int[][] result = new int[directions.Length][];
            Array.Copy(directions, result, directions.Length);

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