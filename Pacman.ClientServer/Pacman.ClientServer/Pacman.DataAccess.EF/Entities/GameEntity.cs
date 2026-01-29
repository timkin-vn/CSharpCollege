using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pacman.DataAccess.EF.Entities
{
    [Table("games")]
    public class GameEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("map_id")]
        public int MapId { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("score")]
        public int Score { get; set; }

        [Column("lives")]
        public int Lives { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }

        [ForeignKey("MapId")]
        public virtual MapEntity Map { get; set; }

        public virtual ICollection<GameActorEntity> Actors { get; set; }
        public virtual ICollection<GameCollectibleStateEntity> CollectibleStates { get; set; }

        // Добавлен конструктор
        public GameEntity()
        {
            Actors = new List<GameActorEntity>();
            CollectibleStates = new List<GameCollectibleStateEntity>();
        }
    }
}
