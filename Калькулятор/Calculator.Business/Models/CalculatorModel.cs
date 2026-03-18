using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Models
{
    public class CalculatorModel
    {
        public double RegisterX { get; set; }
        public double RegisterY { get; set; }
        public string OperationCode { get; set; }
        public bool IsLastDigitPressed { get; set; }
        public bool IsDecimalMode { get; set; }
        public double DecimalMultiplier { get; set; }
    }
}