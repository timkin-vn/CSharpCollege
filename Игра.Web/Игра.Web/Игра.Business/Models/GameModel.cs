namespace Игра.Business.Models
{
    public class GameModel
    {
        public const int Size = 5;

        public bool[,] Cells { get; set; }

        public GameModel()
        {
            Cells = new bool[Size, Size];
        }
    }
}