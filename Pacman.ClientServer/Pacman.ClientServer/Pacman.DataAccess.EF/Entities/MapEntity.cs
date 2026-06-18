using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pacman.DataAccess.EF.Entities
{
    [Table("maps")]
    public class MapEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("row_count")]
        public int RowCount { get; set; }

        [Column("col_count")]
        public int ColCount { get; set; }

        public virtual ICollection<MapCellEntity> Cells { get; set; }

        // Добавлено навигационное свойство
        public virtual ICollection<GameEntity> Games { get; set; }

        public MapEntity()
        {
            Cells = new List<MapCellEntity>();
            Games = new List<GameEntity>();
        }
    }
}
