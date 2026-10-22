namespace FifteenGame.Common.BusinessDtos
{
    public class CheckersMakeMoveRequest
    {
        public int GameId { get; set; }

        public int FromRow { get; set; }

        public int FromCol { get; set; }

        public int ToRow { get; set; }

        public int ToCol { get; set; }
    }
}
