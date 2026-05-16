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

        public double RegisterY { get; internal set; }

        public string OperationCode { get; internal set; }

        internal bool IsLastDigitPressed { get; set; }
    }
}
