using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game2048.Web.Models
{
    public class CellViewModel
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Value { get; set; }
    }
}
