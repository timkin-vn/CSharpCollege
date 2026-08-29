using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FifteenGame.Data.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        public double? BestTimeSeconds { get; set; }

        
        public string SavedGameJson { get; set; }
    }
}