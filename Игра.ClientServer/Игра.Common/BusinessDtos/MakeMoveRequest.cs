using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Игра.Common.BusinessDtos
{
    public class MakeMoveRequest
    {
        public int GameId { get; set; }
        public int Row {  get; set; }
        public int Column { get; set; }
    }
}
