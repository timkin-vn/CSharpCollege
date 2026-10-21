
namespace FifteenGame.Common.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MoveCount { get; set; }
        public int Score { get; set; }
        public int BestScore { get; set; }
        public int[,] Cells { get; } = new int[4, 4];
    }
}
