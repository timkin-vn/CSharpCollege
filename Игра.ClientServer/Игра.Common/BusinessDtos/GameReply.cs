namespace Игра.Common.BusinessDtos
{
    public class GameReply
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int[] Cells { get; set; }
    }
}
