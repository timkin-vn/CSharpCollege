namespace Calculator.Business.Models
{
    public class CalculatorModel
    {
        public double RegisterX { get; internal set; }
        public double RegisterY { get; internal set; }
        public string OperationCode { get; internal set; }
        internal bool IsLastDigitPressed { get; set; }
        public bool IsDrob { get; internal set; }
        internal double FractionDivider { get; set; } = 10;
    }
}
