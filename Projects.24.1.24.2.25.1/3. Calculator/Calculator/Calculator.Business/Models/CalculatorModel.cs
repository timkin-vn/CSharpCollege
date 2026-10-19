using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Models
{
    public class CalculatorModel
    {
        public double RegisterX { get; internal set; }

        public double RegisterY { get; internal set; }

        public string OperationCode { get; internal set; }

        public bool IsDecimalEntry { get; internal set; }

        public int DecimalDigits { get; internal set; }

        public bool HasError { get; internal set; }

        public List<string> History { get; } = new List<string>();

        internal bool IsLastDigitPressed { get; set; }
    }
}