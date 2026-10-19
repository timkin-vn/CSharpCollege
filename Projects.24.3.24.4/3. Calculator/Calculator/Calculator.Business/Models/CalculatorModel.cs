using System;

namespace Calculator.Business.Models
{
    public class CalculatorModel
    {
        public double RegisterX { get; internal set; }

        internal double RegisterY { get; set; }

        internal string OperationCode { get; set; }

        internal bool IsLastDigitPressed { get; set; }

        internal string CurrentInput { get; set; } = "0";

        public string DisplayText { get; internal set; } = "0";
    }
}
