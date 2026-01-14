using System.ComponentModel.DataAnnotations;

namespace FifteenGame.DataAccess.Entities
{
    public class GameEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public int MovesLeft { get; set; }
        public int Mode { get; set; }
        public int State { get; set; }
        public string BoardJson { get; set; }
    }
}