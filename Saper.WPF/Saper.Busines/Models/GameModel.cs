namespace Saper.Models
{
    public class GameModel
    {
        public const int Size = 5;

        public int[,] Board { get; set; } = new int[Size, Size]; // 0 = пусто, 1 = бомба
        public bool[,] OpenedCells { get; set; } = new bool[Size, Size];
        public bool[,] FlaggedCells { get; set; } = new bool[Size, Size];
        public int Moves { get; set; }
        public int BombsCount { get; set; } = 5;
        public GameState State { get; set; } = GameState.Playing;
    }

    public enum GameState
    {
        Playing,
        Won,
        Lost
    }
}