using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pacman.DataAccess.EF.Entities
{
    [Table("users")]
    public class UserEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        // Добавлено навигационное свойство
        public virtual ICollection<GameEntity> Games { get; set; }

        public UserEntity()
        {
            Games = new List<GameEntity>();
        }
    }
}
