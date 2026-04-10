using Calculator.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        public void PressDigit(CalculatorModel calculatorModel, string digitString)
        {
            if (!int.TryParse(digitString, out var digit)) return;

            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.RegisterX = 0;
            }

            calculatorModel.RegisterX = calculatorModel.RegisterX * 10 + digit;
            calculatorModel.IsLastDigitPressed = true;
        }

        public void PressClear(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = 0;
            calculatorModel.RegisterY = 0;
            calculatorModel.OperationCode = null;
            calculatorModel.IsLastDigitPressed = false;
        }

        public void MoveXToY(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterY = calculatorModel.RegisterX;
        }

        private void CompleteOperation(CalculatorModel calculatorModel)
        {
            if (string.IsNullOrEmpty(calculatorModel.OperationCode)) return;

            switch (calculatorModel.OperationCode)
            {
                case "+":
                    calculatorModel.RegisterX = calculatorModel.RegisterY + calculatorModel.RegisterX;
                    break;
                case "-":
                    calculatorModel.RegisterX = calculatorModel.RegisterY - calculatorModel.RegisterX;
                    break;
                case "*":
                    calculatorModel.RegisterX = calculatorModel.RegisterY * calculatorModel.RegisterX;
                    break;
                case "/":
                    if (calculatorModel.RegisterX != 0)
                        calculatorModel.RegisterX = calculatorModel.RegisterY / calculatorModel.RegisterX;
                    break;
                case "^":
                    calculatorModel.RegisterX = Math.Pow(calculatorModel.RegisterY, calculatorModel.RegisterX);
                    break;
                case "sqrt":
                    double rootPower = calculatorModel.RegisterX == 0 ? 2 : calculatorModel.RegisterX;
                    calculatorModel.RegisterX = Math.Pow(calculatorModel.RegisterY, 1.0 / rootPower);
                    break;

                
                case "x²":
                    calculatorModel.RegisterX = Math.Pow(calculatorModel.RegisterX, 2);
                    break;
                case "π":
                    calculatorModel.RegisterX = Math.PI;
                    break;
                case "!":
                    calculatorModel.RegisterX = CalculateFactorial((int)calculatorModel.RegisterX);
                    break;
            }

            calculatorModel.OperationCode = null;
        }

        
        private double CalculateFactorial(int n)
        {
            if (n < 0) return 0;
            if (n == 0) return 1;
            double result = 1;
            for (int i = 1; i <= n; i++) result *= i;
            return result;
        }

        public void PressOperation(CalculatorModel calculatorModel, string operationCode)
        {
            
            if (operationCode == "x²" || operationCode == "π" || operationCode == "!")
            {
                calculatorModel.OperationCode = operationCode;
                CompleteOperation(calculatorModel);
            }
            else
            {
                CompleteOperation(calculatorModel);
                MoveXToY(calculatorModel);
                calculatorModel.OperationCode = operationCode;
            }
            calculatorModel.IsLastDigitPressed = false;
        }

        public void PressEqual(CalculatorModel calculatorModel)
        {
            CompleteOperation(calculatorModel);
            calculatorModel.IsLastDigitPressed = false;
        }
    }
}