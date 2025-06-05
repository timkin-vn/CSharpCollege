using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Models
{
    public class CalculatorState
    {
        public double XRegister { get; set; }

        public double YRegister { get; set; }

        public string OpCodeRegister { get; set; }

        public bool NeedClear { get; set; }
        public string OperationLog {  get; set; }  
    }
}
