using Игра.Common.Definitions;

namespace Игра.Common.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int[,] Cells { get; } = new int[Constants.Size, Constants.Size];

        public int MoveCount { get; set; }
    }
}