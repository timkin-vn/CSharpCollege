using _2048Game.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048Game.Common.BusinessDtos
{
    public class MoveRequest
    {
        public MoveDirections.MoveDirection direction { get; set; }
        public int UserId { get; set; }
    }
}
