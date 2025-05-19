using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Models
{
    public class CalculatorState
    {
        public double RegisterX { get; set; } = 0;
        public double RegisterY { get; set; } = 0;
        public string OperationCode { get; set; } = "";
        public bool IsClearNeeded { get; set; } = false;

        // Для поддержки ввода как строки
        public string InputBuffer { get; set; } = "";
    }
}
