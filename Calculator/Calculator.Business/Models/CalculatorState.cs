using Calculator.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Models
{
    public class CalculatorState
    {
        public decimal RegisterX { get; set; }

        public decimal RegisterY { get; set; }

        public string OperationCode { get; set; }

        public bool IsClearNeeded { get; set; }
    }
}