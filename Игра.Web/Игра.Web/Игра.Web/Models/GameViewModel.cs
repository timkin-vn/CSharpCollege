using Игра.Business.Models;

namespace Игра.Web.Models
{
    public class GameViewModel
    {
        public int Size => GameModel.Size;
        public CellViewModel[,] Cells { get; set; } = new CellViewModel[GameModel.Size, GameModel.Size];
    }
}