namespace Игра.Common.BusinessModels
{
    public class GameModel
    {
        public const int RowCount = 5;
        public const int ColumnCount = 5;

        private readonly bool[,] _cells = new bool[RowCount, ColumnCount];

        public bool this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
