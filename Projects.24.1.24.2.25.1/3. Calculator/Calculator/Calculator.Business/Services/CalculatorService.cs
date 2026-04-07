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
        public void PressUnaryOperation(CalculatorModel calculatorModel, string operationCode)
        {
            if (calculatorModel == null || string.IsNullOrWhiteSpace(operationCode))
            {
                return;
            }

            switch (operationCode)
            {
                case "x²":
                case "x^2":
                case "^2":
                    calculatorModel.RegisterX = calculatorModel.RegisterX * calculatorModel.RegisterX;
                    calculatorModel.IsLastDigitPressed = false;
                    return;

                case "√":
                case "sqrt":
                    calculatorModel.RegisterX = Math.Sqrt(calculatorModel.RegisterX);
                    calculatorModel.IsLastDigitPressed = false;
                    return;

                case "!":
                    var x = calculatorModel.RegisterX;
                    if (x < 0 || x % 1 != 0)
                    {
                        calculatorModel.RegisterX = double.NaN;
                        calculatorModel.IsLastDigitPressed = false;
                        return;
                    }

                    var n = (int)x;
                    double f = 1;
                    for (int i = 2; i <= n; i++)
                    {
                        f *= i;
                        if (double.IsInfinity(f))
                        {
                            break;
                        }
                    }

                    calculatorModel.RegisterX = f;
                    calculatorModel.IsLastDigitPressed = false;
                    return;
            }
        }

        public void PressDigit(CalculatorModel calculatorModel, string digitString)
        {
            if (!int.TryParse(digitString, out var digit))
            {
                return;
            }

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
            calculatorModel.IsLastDigitPressed = false;
        }

        public void MoveXToY(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterY = calculatorModel.RegisterX;
        }

        private void CompleteOperation(CalculatorModel calculatorModel)
        {
            switch (calculatorModel.OperationCode)
            {
                case "+":
                    calculatorModel.RegisterX = calculatorModel.RegisterX + calculatorModel.RegisterY;
                    break;

                case "-":
                    calculatorModel.RegisterX = calculatorModel.RegisterY - calculatorModel.RegisterX;
                    break;

                case "*":
                    calculatorModel.RegisterX = calculatorModel.RegisterX * calculatorModel.RegisterY;
                    break;

                case "/":
                    calculatorModel.RegisterX = calculatorModel.RegisterY / calculatorModel.RegisterX;
                    break;
            }
        }

        public void PressOperation(CalculatorModel calculatorModel, string operationCode)
        {
            CompleteOperation(calculatorModel);

            MoveXToY(calculatorModel);
            calculatorModel.OperationCode = operationCode;
            calculatorModel.IsLastDigitPressed = false;
        }

        public void PressEqual(CalculatorModel calculatorModel)
        {
            CompleteOperation(calculatorModel);
            calculatorModel.IsLastDigitPressed = false;
        }
    }
}
