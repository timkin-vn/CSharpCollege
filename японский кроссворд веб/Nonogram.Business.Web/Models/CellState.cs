using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram.Business.Web.Models
{
    public enum CellState
    {
        Empty = 0,
        Filled = 1,
        Crossed = -1,
    }
}