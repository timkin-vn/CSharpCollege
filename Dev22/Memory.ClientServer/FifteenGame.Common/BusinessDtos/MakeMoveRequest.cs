using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.BusinessDtos
{
    public class MakeMoveRequest
    {
        public int GameId { get; set; }

        public int[] ButtonOneRowCol { get; set; }     
        public int[] ButtonTwoRowCol { get; set; }
        
    }
}
