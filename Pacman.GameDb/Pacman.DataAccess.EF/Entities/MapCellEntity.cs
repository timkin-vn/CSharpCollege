using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pacman.DataAccess.EF.Entities
{
    [Table("map_cells")]
    public class MapCellEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("map_id")]
        public int MapId { get; set; }

        [Column("row")]
        public int Row { get; set; }

        [Column("col")]
        public int Col { get; set; }

        [Column("cell_type")]
        public int CellType { get; set; }

        [ForeignKey("MapId")]
        public virtual MapEntity Map { get; set; }
    }
}