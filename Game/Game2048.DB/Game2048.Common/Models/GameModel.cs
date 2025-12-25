namespace Game2048.Common.Models
{
    public class GameModel
    {
        public int[,] Board { get; set; }
        public int Score { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsWon { get; set; }

        public GameModel()
        {
            Board = new int[4, 4];
            Score = 0;
            IsGameOver = false;
            IsWon = false;
        }
    }
}
