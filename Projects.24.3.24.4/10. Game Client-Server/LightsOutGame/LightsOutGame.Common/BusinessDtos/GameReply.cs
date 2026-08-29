namespace LightsOutGame.Common.BusinessDtos
{
    public class GameReply
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MoveCount { get; set; }

        public int[] Cells { get; set; }
    }
}
