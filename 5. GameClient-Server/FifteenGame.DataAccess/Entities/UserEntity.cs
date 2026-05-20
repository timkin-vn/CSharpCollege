using System.ComponentModel.DataAnnotations;

namespace FifteenGame.DataAccess.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public int BestScore { get; set; }
    }
}