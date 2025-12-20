using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifteenGame.Web.Models
{
    public class CellViewModel
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public int GemType { get; set; }

        public bool IsEmpty => GemType == 0;
    }
}