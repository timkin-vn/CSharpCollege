using Calculator.Business.Models;
using System;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        // Нажали на число от 0 до 9
        public void PressDigit(CalculatorModel calculatorModel, string digitString)
        {
            if (!int.TryParse(digitString, out var digit))
            {
                return;
            }

            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.RegisterX = 0;
                calculatorModel.FractionDivider = 10;
            }

            if (!calculatorModel.IsDrob)
            {
                calculatorModel.RegisterX = calculatorModel.RegisterX * 10 + digit;
            }
            else
            {
                double fraction = digit / calculatorModel.FractionDivider;

                if (calculatorModel.RegisterX < 0)
                {
                    calculatorModel.RegisterX -= fraction;
                }
                else
                {
                    calculatorModel.RegisterX += fraction;
                }

                calculatorModel.FractionDivider *= 10;
            }

            calculatorModel.IsLastDigitPressed = true;
        }

        // Нажали на запятую
        public void PressComma(CalculatorModel calculatorModel)
        {
            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.RegisterX = 0;
                calculatorModel.IsLastDigitPressed = true;
            }

            calculatorModel.IsDrob = true;
        }

        public void PressClear(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = 0;
            calculatorModel.RegisterY = 0;
            calculatorModel.OperationCode = null;
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.IsDrob = false;
            calculatorModel.FractionDivider = 10;
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
                    calculatorModel.RegisterX = calculatorModel.RegisterX == 0
                        ? double.NaN
                        : calculatorModel.RegisterY / calculatorModel.RegisterX;
                    break;
            }

            ResetInputMode(calculatorModel);
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

        public void PressSquare(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX *= calculatorModel.RegisterX;
            ResetInputMode(calculatorModel);
        }

        public void PressSquareRoot(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = calculatorModel.RegisterX < 0
                ? double.NaN
                : Math.Sqrt(calculatorModel.RegisterX);
            ResetInputMode(calculatorModel);
        }

        private void ResetInputMode(CalculatorModel calculatorModel)
        {
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.IsDrob = false;
            calculatorModel.FractionDivider = 10;
        }
    }
}
