
namespace FifteenGame.Common.BusinessDtos
{
    public class GameReply
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MoveCount { get; set; }
        public int Score { get; set; }
        public int BestScore { get; set; }
        public int[] Cells { get; set; }
    }
}
