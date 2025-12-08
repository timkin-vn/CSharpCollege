using Игра.Common.Definitions;

namespace Игра.Common.BusinessModels
{
    public class GameModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public bool[,] Cells { get; set; }

        public int MoveCount { get; set; }

        public GameModel()
        {
            Cells = new bool[Constants.Size, Constants.Size];
        }
    }
}