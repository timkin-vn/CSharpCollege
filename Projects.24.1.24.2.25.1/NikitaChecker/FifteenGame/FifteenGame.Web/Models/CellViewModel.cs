using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Web.Models
{
    public class CellViewModel
    {
        public bool HasChecker { get; set; }
        public string CheckerColor { get; set; }
        public bool IsKing { get; set; }
        public bool IsValidFrom { get; set; }
        public bool IsValidTo { get; set; }
    }
}
