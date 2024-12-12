using System;

namespace Calculator.Business.Models
{
    public class CalculatorState
    {
        public double XRegister { get; set; }
        public double YRegister { get; set; }
        public string OpCode { get; set; }
        public bool NeedClearX { get; set; }
        public string OperationLog { get; set; }
    }
}