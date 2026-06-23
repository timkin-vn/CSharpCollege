namespace SeaBattle.Common.Dto
{
    public class SaveGameRequest
    {
        public int UserId { get; set; }
        public int MoveCount { get; set; }
        public bool Won { get; set; }
    }
}
