using System;

namespace Calculator.Business.Models
{
    public class CalculatorModel
    {
        public string DisplayText { get; internal set; } = "0";

        public double RegisterX
        {
            get
            {
                if (double.TryParse(DisplayText, out var value))
                {
                    return value;
                }

                return 0;
            }
        }

        internal double RegisterY { get; set; }

        internal string OperationCode { get; set; }

        internal bool IsNewInput { get; set; } = true;

        internal double LastOperationValue { get; set; }

        internal string LastOperationCode { get; set; }

        internal bool IsRepeatedEqualEnabled { get; set; }

        internal bool IsDivisionByZeroHandled { get; set; }
    }
}
