using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
using System; 

namespace PacmanGame.DataAccess.Entities
{
    public class GameUserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //  Ключ теперь называется Id

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } 

        // Навигационное свойство
        public virtual ICollection<GameStateEntity> GameStates { get; set; }
    }
}