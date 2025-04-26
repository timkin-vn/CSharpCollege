namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public int[,] Field { get; private set; }
        public int Score { get; private set; }
        public const int Size = 4;

        public GameModel()
        {
            Field = new int[Size, Size];
            Score = 0;
        }

        public void SetCell(int row, int col, int value)
        {
            Field[row, col] = value;
        }

        public int GetCell(int row, int col)
        {
            return Field[row, col];
        }

        public void AddScore(int value)
        {
            Score += value;
        }

        public void Reset()
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    Field[i, j] = 0;

            Score = 0;
        }
    }
}
