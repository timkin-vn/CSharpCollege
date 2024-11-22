using System;

namespace Calculator.Business.Models
{
    public class CalculatorState
    {
        public double XRegister { get; set; }

        public double YRegister { get; set; }

        public string OpCodeRegister { get; set; }

        public bool NeedClear { get; set; }

        public string OperationLog { get; set; }
    }
}