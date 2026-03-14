using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pacman.DataAccess.EF.Entities
{
    [Table("game_actors")]
    public class GameActorEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("game_id")]
        public int GameId { get; set; }

        [Column("actor_type")]
        public int ActorType { get; set; }

        [Column("row")]
        public int Row { get; set; }

        [Column("col")]
        public int Col { get; set; }

        [Column("direction")]
        public int Direction { get; set; }

        [Column("frightened_ticks_left")]
        public int FrightenedTicksLeft { get; set; }

        [ForeignKey("GameId")]
        public virtual GameEntity Game { get; set; }
    }
}
