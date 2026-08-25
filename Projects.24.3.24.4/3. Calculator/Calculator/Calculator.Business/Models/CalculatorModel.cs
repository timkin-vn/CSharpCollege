using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Models
{
    public class CalculatorModel
    {
        public double RegisterX { get; internal set; }

        internal double RegisterY { get; set; }

        internal string OperationCode { get; set; }

        internal bool IsLastDigitPressed { get; set; }
    }
}
