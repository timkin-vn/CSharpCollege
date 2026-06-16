using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Business.Models;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        public void PressDigit(CalculatorModel calculatorModel, string digitString)
        {
            if (!int.TryParse(digitString, out var digit))
            {
                return;
            }
            if (calculatorModel.Trigged)
            {
                calculatorModel.RegisterX = 0;
                calculatorModel.PresentTrigged = false;
                calculatorModel.Trigged = false;
            }
            calculatorModel.RegisterX = calculatorModel.RegisterX * 10 + digit;
        }
        public void PressClear(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = 0;
        }
        public void MoveXtoY(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterY = calculatorModel.RegisterX;
        }
        private void CompleteOperation(CalculatorModel calculatorModel)
        {
            switch (calculatorModel.OperationCode)
            {
                case "+":
                    calculatorModel.RegisterX += calculatorModel.RegisterY; break;
                case "-":
                    calculatorModel.RegisterX -= calculatorModel.RegisterY; break;
                case "*":
                    calculatorModel.RegisterX *= calculatorModel.RegisterY; break;
                case "/":
                    calculatorModel.RegisterX /= calculatorModel.RegisterY; break;
                case "^":
                    calculatorModel.RegisterX = Math.Pow(calculatorModel.RegisterY, calculatorModel.RegisterX); break;
                case "%":
                    if (!calculatorModel.PresentTrigged)
                    {
                        calculatorModel.RegisterX /= (double)100.0;
                        calculatorModel.PresentTrigged = true;
                    }
                    break;
                case "n^√":
                    calculatorModel.RegisterX = Math.Pow(calculatorModel.RegisterY, 1 / (double)calculatorModel.RegisterX); break;
                case "sin":
                    calculatorModel.RegisterX = Math.Sin(calculatorModel.RegisterY); break;
                case "√":
                    calculatorModel.RegisterX = Math.Sqrt(calculatorModel.RegisterY); break;
            }
        }
        
        public void PressOperation(CalculatorModel calculatorModel, string operationCode)
        {
            CompleteOperation(calculatorModel);
            MoveXtoY(calculatorModel);
            calculatorModel.OperationCode = operationCode;
            calculatorModel.Trigged = true;
        }

        public void PressEqual(CalculatorModel calculatorModel)
        {
            CompleteOperation(calculatorModel);
            calculatorModel.Trigged = true;
        }
        public void PreePrecent(CalculatorModel calculatorModel)
        {
            calculatorModel.OperationCode = "%";
            CompleteOperation(calculatorModel);
            calculatorModel.Trigged = true;
            
        }
    }
}
