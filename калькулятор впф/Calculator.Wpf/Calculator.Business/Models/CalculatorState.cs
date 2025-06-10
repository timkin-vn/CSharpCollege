namespace Calculator.Business.Models
{
    public class CalculatorState
    {
        public double RegisterX { get; set; }
        public double RegisterY { get; set; }
        public string OperationCode { get; set; }
        public bool IsFloat { get; set; }
        public double bonus { get; set; } = 10.0;
    }
}