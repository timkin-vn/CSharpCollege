using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pacman.DataAccess.EF.Entities
{
    [Table("game_collectible_state")]
    public class GameCollectibleStateEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("game_id")]
        public int GameId { get; set; }

        [Column("row")]
        public int Row { get; set; }

        [Column("col")]
        public int Col { get; set; }

        [Column("collectible_type")]
        public int CollectibleType { get; set; }

        [Column("is_eaten")]
        public bool IsEaten { get; set; }

        [ForeignKey("GameId")]
        public virtual GameEntity Game { get; set; }
    }
}