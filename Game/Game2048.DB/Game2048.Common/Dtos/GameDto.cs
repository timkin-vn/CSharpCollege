namespace Game2048.Common.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsWon { get; set; }
        public string BoardState { get; set; }
    }
}
