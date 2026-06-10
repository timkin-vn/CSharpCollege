using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Web.Models
{
    public class MoveInfo
    {
        public int FromRow, FromCol, ToRow, ToCol;
        public MoveInfo(int fr, int fc, int tr, int tc) { FromRow = fr; FromCol = fc; ToRow = tr; ToCol = tc; }
    }
}
