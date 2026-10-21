
namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public const int RowCount = 4;
        public const int ColumnCount = 4;
        public const int FreeCellValue = 0;

        private readonly int[,] _cells =
            new int[RowCount, ColumnCount];

        public int this[int row, int column]
        {
            get => _cells[row, column];
            internal set => _cells[row, column] = value;
        }

        public int Score { get; internal set; }

        public bool HasWon
        {
            get
            {
                for (int row = 0; row < RowCount; row++)
                {
                    for (int column = 0; column < ColumnCount; column++)
                    {
                        if (_cells[row, column] >= 2048)
                            return true;
                    }
                }

                return false;
            }
        }
        public void RestoreState(int[,] board, int score)
        {
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    this[row, column] = board[row, column];
                }
            }

            Score = score;
        }
    }
}
