using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Models
{
    public class CalculatorState
    {
        public double RegisterX { get; set; }

        public double RegisterY { get; set; }

        public string Operation { get; set; }

        public bool NeedClearX { get; set; }

        public string ErrorMessage { get; set; }
    }
}
