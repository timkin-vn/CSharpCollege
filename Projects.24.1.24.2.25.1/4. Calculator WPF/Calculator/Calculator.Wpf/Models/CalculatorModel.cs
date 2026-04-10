using System;

namespace CalculatorWPF.Models
{
    public class CalculatorModel
    {
        public double RegisterX { get; set; }
        public double RegisterY { get; set; }
        public string OperationCode { get; set; }
        public bool IsLastDigitPressed { get; set; }
        public bool IsDrob { get; set; }

        public CalculatorModel()
        {
            RegisterX = 0;
            RegisterY = 0;
            OperationCode = "";
            IsLastDigitPressed = false;
            IsDrob = false;
        }
    }
}