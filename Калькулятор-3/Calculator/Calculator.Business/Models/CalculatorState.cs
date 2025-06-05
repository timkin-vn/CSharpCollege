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

        public string OperationCode { get; set; }

        public bool IsClearNeeded { get; set; }

        public bool IsFloat { get; set; }

        public double bonus { get; set; }

    }
}
