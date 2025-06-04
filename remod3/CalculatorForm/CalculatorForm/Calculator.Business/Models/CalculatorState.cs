namespace CalculatorForm.Calculator.Business.Models {
    public class CalculatorState {
        public string CurrentInput { get; set; } = "";
        public double RegisterX { get; set; }
        public double RegisterY { get; set; }
        public string? Operation { get; set; }
        public bool NeedClearX { get; set; }
        public double Memory { get; set; }
        public string Expression { get; set; } = "";
        public string SelectedFunction { get; set; } = "sin";
    }
}