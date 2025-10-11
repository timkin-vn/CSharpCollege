namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public const int RowCount = 10;
        public const int ColumnCount = 10;
        public int TotalMines { get; internal set; } = 15;

       
        private int[,] _cells = new int[RowCount, ColumnCount];
        private bool[,] _revealed = new bool[RowCount, ColumnCount];
        private bool[,] _flagged = new bool[RowCount, ColumnCount];
        private bool[,] _mines = new bool[RowCount, ColumnCount];

        public int this[int row, int column]
        {
            get => _cells[row, column];
            internal set => _cells[row, column] = value;
        }

        public GameState GameState { get; internal set; } = GameState.Playing;
        public int FlagsPlaced { get; internal set; }

        public bool IsRevealed(int row, int column) => _revealed[row, column];
        public void SetRevealed(int row, int column, bool value) => _revealed[row, column] = value;

        public bool IsFlagged(int row, int column) => _flagged[row, column];
        public void SetFlagged(int row, int column, bool value) => _flagged[row, column] = value;

        public bool HasMine(int row, int column) => _mines[row, column];
        public void SetMine(int row, int column, bool value) => _mines[row, column] = value;

        public int GetAdjacentMines(int row, int column) => _cells[row, column];
    }

    public enum GameState
    {
        Playing,
        Won,
        GameOver
    }
}