namespace FifteenGame.Business.Models
{
    public enum Player
    {
        None,
        X,
        O
    }

    public enum GameState
    {
        Playing,
        Won,
        Draw
    }

    public class TicTacToeGame
    {
        public const int RowCount = 3;
        public const int ColumnCount = 3;

        private Player[,] _board = new Player[RowCount, ColumnCount];

        public Player CurrentPlayer { get; set; } = Player.X;
        public GameState GameState { get; set; } = GameState.Playing;
        public Player Winner { get; set; } = Player.None;

        public Player this[int row, int column]
        {
            get => _board[row, column];
            set => _board[row, column] = value;
        }

        public bool IsCellEmpty(int row, int column) => _board[row, column] == Player.None;
    }
}
