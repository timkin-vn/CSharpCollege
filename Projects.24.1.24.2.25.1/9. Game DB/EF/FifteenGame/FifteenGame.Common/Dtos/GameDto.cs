using FifteenGame.Common.Definitions;

namespace FifteenGame.Common.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MoveCount { get; set; }
        public int Winner { get; set; } 

        
        public int[,] Cells { get; set; } = new int[3, 3];
    }
}