using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minesweeper.DataAccess.EF.Entities
{
    [Table("game_states")]
    public class GameStateEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [Column("game_data", TypeName = "text")]
        public string GameData { get; set; }

        [Column("is_game_over")]
        public bool IsGameOver { get; set; }

        [Column("is_game_won")]
        public bool IsGameWon { get; set; }

        [Column("play_time")]
        public TimeSpan PlayTime { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("field_size")]
        public int FieldSize { get; set; }

        [Column("mine_count")]
        public int MineCount { get; set; }

        [Column("flags_placed")]
        public int FlagsPlaced { get; set; }

        [Column("cells_revealed")]
        public int CellsRevealed { get; set; }

        public virtual UserEntity User { get; set; }

        public GameStateEntity()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            IsGameOver = false;
            IsGameWon = false;
            PlayTime = TimeSpan.Zero;
            FlagsPlaced = 0;
            CellsRevealed = 0;
        }
    }
}