using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel
    {
        public bool IsOn { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}