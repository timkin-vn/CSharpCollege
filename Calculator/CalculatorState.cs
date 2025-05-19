namespace Calculator.Business.Models
{
    public class CalculatorState
    {
        public string Display { get; set; } = "0";
        public string Operation { get; set; } = "";
        public string FirstNumber { get; set; } = "";
        public bool NeedClear { get; set; } = false;
    }
}