using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pacman.Common.Enums;

namespace Pacman.Common.Models
{
    public class GameDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MapId { get; set; }
        public GameStatus Status { get; set; }
        public int Score { get; set; }
        public int Lives { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}