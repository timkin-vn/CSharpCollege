namespace Game2048.Business
{

    public class GameModel
    {
        public const int Size = 4;

        public int[,] Field { get; set; }
        public int Score { get; set; }
        public int MoveCount { get; set; }

        public GameModel()
        {
            Field = new int[Size, Size];
            Score = 0;
            MoveCount = 0;
        }
    }
}
