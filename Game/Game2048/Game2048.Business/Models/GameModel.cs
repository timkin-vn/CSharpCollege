namespace Game2048.Business.Models
{
    public class GameModel
    {
        public const int RowCount = 4;
        public const int ColumnCount = 4;

        private int[,] _cells = new int[RowCount, ColumnCount];

        public int this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        public int Score { get; set; }
    }
}
