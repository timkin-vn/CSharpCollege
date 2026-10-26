namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public int[,] Board { get; set; } = new int[3, 3];
        public int CurrentPlayer { get; set; } = 1;
        public int Winner { get; set; } = 0; 

        public void Reset()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Board[i, j] = 0;

            CurrentPlayer = 1;
            Winner = 0;
        }
    }
}