using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.EF.Entities
{
    [Table("MazeGames")]
    public class MazeGame
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string SerializedGameManager { get; set; }
    }
}
