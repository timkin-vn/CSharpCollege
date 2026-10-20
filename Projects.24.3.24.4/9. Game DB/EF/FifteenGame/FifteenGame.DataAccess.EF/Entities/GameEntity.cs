using System.ComponentModel.DataAnnotations;

namespace FifteenGame.DataAccess.EF.Entities
{
    public class GameEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string BoardState { get; set; }
        public int Result { get; set; }
        public int PlayerScore { get; set; }
        public int ComputerScore { get; set; }

        public virtual UserEntity User { get; set; }
    }
}