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
            if (!int.TryParse(digitString, out var digit))
            {
                return;
            }

            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.RegisterX = 0;
            }

            calculatorModel.RegisterX *= 10;
            calculatorModel.RegisterX += digit;
            calculatorModel.IsLastDigitPressed = true;
        }

        public void PressClear(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = 0;
            calculatorModel.IsLastDigitPressed = false;
        }

        public void PressMoveXToY(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterY = calculatorModel.RegisterX;
            calculatorModel.IsLastDigitPressed = false;
        }

        public void PressOperation(CalculatorModel calculatorModel, string operationCode)
        {
            if (IsUnaryOperation(operationCode))
            {
                CompleteUnaryOperation(calculatorModel, operationCode);
                calculatorModel.IsLastDigitPressed = false;
                return;
            }

            CompleteOperation(calculatorModel);

            PressMoveXToY(calculatorModel);
            calculatorModel.OperationCode = operationCode;
            calculatorModel.IsLastDigitPressed = false;
        }

        public void PressEqual(CalculatorModel calculatorModel)
        {
            CompleteOperation(calculatorModel);
            calculatorModel.OperationCode = null;
            calculatorModel.IsLastDigitPressed = false;
        }

        private bool IsUnaryOperation(string operationCode)
        {
            switch (operationCode)
            {
                case "x^2":
                case "sqrt":
                case "1/x":
                    return true;

                default:
                    return false;
            }
        }

        private void CompleteUnaryOperation(CalculatorModel calculatorModel, string operationCode)
        {
            switch (operationCode)
            {
                case "x^2":
                    calculatorModel.RegisterX = calculatorModel.RegisterX * calculatorModel.RegisterX;
                    break;

                case "sqrt":
                    calculatorModel.RegisterX = Math.Sqrt(calculatorModel.RegisterX);
                    break;

                case "1/x":
                    calculatorModel.RegisterX = 1 / calculatorModel.RegisterX;
                    break;
            }
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
    }
}
