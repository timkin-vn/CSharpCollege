using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minesweeper.DataAccess.EF.Entities
{
    [Table("users")]
    public class UserEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("username")]
        [StringLength(100)]
        public string Username { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("total_games_played")]
        public int TotalGamesPlayed { get; set; }

        [Column("games_won")]
        public int GamesWon { get; set; }

        [Column("last_played_at")]
        public DateTime? LastPlayedAt { get; set; }

        public virtual ICollection<GameStateEntity> Games { get; set; }

        public UserEntity()
        {
            CreatedAt = DateTime.Now;
            TotalGamesPlayed = 0;
            GamesWon = 0;
            Games = new HashSet<GameStateEntity>();
        }
    }
}