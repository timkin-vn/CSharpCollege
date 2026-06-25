using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FifteenGame.DataAccess.EF.Entities
{
    [Table("CheckersGames", Schema = "public")]
    public class CheckersGameEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public int CurrentPlayer { get; set; }
        public bool IsFinished { get; set; }
        public int? Winner { get; set; }
        public string GameStateJson { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<CheckersMoveEntity> Moves { get; set; } = new List<CheckersMoveEntity>();
    }
}