namespace StepByStepPacman
{
    public class Pacman
    {
        public int X { get; set; }
        public int Y { get; set; }

        // Убрали tileSize из конструктора - он не нужен для логики движения
        public Pacman(int startX, int startY)
        {
            X = startX;
            Y = startY;
        }

        public bool TryMove(int dx, int dy, int[,] gameBoard)
        {
            int newX = X + dx;
            int newY = Y + dy;

            // Проверка выхода за границы
            if (newX < 0 || newX >= gameBoard.GetLength(1) ||
                newY < 0 || newY >= gameBoard.GetLength(0))
                return false;

            if (gameBoard[newY, newX] == 0) // Стена
                return false;

            // Двигаемся
            X = newX;
            Y = newY;
            return true;
        }
    }
}