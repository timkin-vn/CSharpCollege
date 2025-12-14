namespace FifteenGame.Data.Entities
{
    public class GameState
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }


        public string BoardData { get; set; }

        public int Score { get; set; } 
    }
}