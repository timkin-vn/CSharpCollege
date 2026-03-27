using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Models
{
    public class CalculatorModel
    {
        public double RegisterX { get; set; } = 0;
        public double? RegisterY { get; set; } = null; 
        public string OperationCode { get; set; }

        public bool IsTyping { get; set; } = false;
        public bool IsLastDigitPressed { get; set; } = false;
    }

}
