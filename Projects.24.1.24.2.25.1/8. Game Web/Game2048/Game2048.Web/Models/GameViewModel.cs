using Game2048.Business;

namespace Game2048.Web.Models
{

    public class GameViewModel
    {
        public int[,] Field { get; set; }
        public int Score { get; set; }
        public int MoveCount { get; set; }
        public string Status { get; set; }
        public int Size { get { return GameModel.Size; } }
    }
}
