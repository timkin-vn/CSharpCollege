namespace FifteenGame.Web.Models
{
    public class GameModel
    {
        public const int RowCount = 10;
        public const int ColumnCount = 10;
        public int TotalMines { get; set; } = 15;

        public int[,] Cells { get; set; } = new int[RowCount, ColumnCount];
        public bool[,] Revealed { get; set; } = new bool[RowCount, ColumnCount];
        public bool[,] Flagged { get; set; } = new bool[RowCount, ColumnCount];
        public bool[,] Mines { get; set; } = new bool[RowCount, ColumnCount];

        public GameState GameState { get; set; } = GameState.Playing;
        public int FlagsPlaced { get; set; }

        public int GetAdjacentMines(int row, int column) => Cells[row, column];
    }

    public enum GameState
    {
        Playing,
        Won,
        GameOver
    }
}