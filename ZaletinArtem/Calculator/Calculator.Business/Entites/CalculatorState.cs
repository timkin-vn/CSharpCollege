using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Entites
{
    public class CalculatorState
    {
        public double XRegister { get; set; }
        public double YRegister { get; set; }
        public double Result { get; set; }

        public string OpCode { get; set; }

        public bool NeedClearX { get; set; }

        public bool IsDecimalMode { get; set; }
        public double DecimalFactor { get; set; } = 1;

        public string DisplayedOperation { get; set; }
    }
}
