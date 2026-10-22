using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FifteenGame.DataAccess.EF.Entities
{
    [Table("CheckersMoves", Schema = "public")]
    public class CheckersMoveEntity
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int MoveNumber { get; set; }
        public int FromRow { get; set; }
        public int FromCol { get; set; }
        public int ToRow { get; set; }
        public int ToCol { get; set; }
        public bool IsCapture { get; set; }
        public int? CapturedRow { get; set; }
        public int? CapturedCol { get; set; }
        public bool PromotedToKing { get; set; }
        public DateTime MoveTime { get; set; } = DateTime.UtcNow;

        public virtual CheckersGameEntity Game { get; set; }
    }
}