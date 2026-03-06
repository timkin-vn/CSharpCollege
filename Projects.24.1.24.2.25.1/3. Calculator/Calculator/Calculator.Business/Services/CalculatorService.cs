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
            if (!int.TryParse(digitString, out int digit))
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
                    calculatorModel.RegisterX =
                        Math.Pow(calculatorModel.RegisterY, calculatorModel.RegisterX);
                    break;

                case "%":
                    calculatorModel.RegisterX =
                        calculatorModel.RegisterY * calculatorModel.RegisterX / 100.0;
                    break;
                case "√":
                    calculatorModel.RegisterX = Math.Sqrt(calculatorModel.RegisterX);
                    calculatorModel.IsLastDigitPressed = false;
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
            calculatorModel.OperationCode = null;
            calculatorModel.IsLastDigitPressed = false;
        }
    }
}