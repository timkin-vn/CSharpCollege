using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightsOutGame.DataAccess.EF.Entities
{
    public class GameEntity
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MoveCount { get; set; }

        // Состояние поля 5x5 хранится строкой из 25 значений через запятую.
        [Required]
        public string CellsData { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual UserEntity User { get; set; }
    }
}
