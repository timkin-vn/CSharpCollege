using System.ComponentModel.DataAnnotations;

namespace BlackjackMVC.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }
    }
}