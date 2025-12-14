namespace FifteenGame.Business.Models
{
    public enum CellType
    {
        Wall,
        Empty,
        Player,
        Exit
    }

    public class GameField
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public CellType[,] Grid { get; set; } // Двумерный массив
        public int PlayerX { get; set; }
        public int PlayerY { get; set; }
        public bool IsGameFinished { get; set; }

        public GameField(int width, int height)
        {
            Width = width;
            Height = height;
            Grid = new CellType[width, height];
        }
    }
}