using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LightsOutGame.DataAccess.EF.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<GameEntity> Games { get; set; } = new List<GameEntity>();
    }
}
