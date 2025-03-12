namespace TicTacToe.Business.Models
{
    public class GameModel
    {
        public const int RowCount = 3;
        public const int ColumnCount = 3;

        private string[,] _cells = new string[RowCount, ColumnCount];

        public string this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        
    }
}
