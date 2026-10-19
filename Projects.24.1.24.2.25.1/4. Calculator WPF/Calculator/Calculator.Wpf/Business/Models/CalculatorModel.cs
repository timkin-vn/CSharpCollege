using System;

namespace Calculator.Business.Models
{
    public class CalculatorModel
    {
        public double RegisterX { get; internal set; }
        public double RegisterY { get; internal set; }
        public string OperationCode { get; internal set; } = "";
        public string InputText { get; internal set; } = "0";
        internal bool IsLastDigitPressed { get; set; }
        internal bool StartFreshOnDigit { get; set; }
        internal bool HasError { get; set; }
        internal double LastOperand { get; set; }
        internal string LastOperation { get; set; } = "";
        internal bool AfterEquals { get; set; }
    }
}
