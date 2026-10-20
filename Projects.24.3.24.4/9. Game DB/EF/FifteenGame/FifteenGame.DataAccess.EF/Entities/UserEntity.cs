using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FifteenGame.DataAccess.EF.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<GameEntity> Games { get; set; }
    }
}