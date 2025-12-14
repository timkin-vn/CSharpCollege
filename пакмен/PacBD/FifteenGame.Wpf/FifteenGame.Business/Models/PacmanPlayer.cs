namespace StepByStepPacman.Business.Models
{
    public class PacmanPlayer : GameObject
    {
        public Direction Direction { get; set; } = Direction.None;

        public PacmanPlayer(int x, int y, int size) : base(x, y, size) { }

        public bool TryMove(int dx, int dy, int[,] gameBoard)
        {
            int newX = X + dx;
            int newY = Y + dy;

            if (newX < 0 || newX >= gameBoard.GetLength(1) ||
                newY < 0 || newY >= gameBoard.GetLength(0))
                return false;

            if (gameBoard[newY, newX] == 0)
                return false;

            X = newX;
            Y = newY;
            return true;
        }
    }
}