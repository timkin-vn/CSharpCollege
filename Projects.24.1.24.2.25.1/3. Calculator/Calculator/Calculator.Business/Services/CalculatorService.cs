using Calculator.Business.Models;
using System;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        public void PressDigit(CalculatorModel calculatorModel, string digitString)
        {
            if (!int.TryParse(digitString, out var digit))
                return;

            if (!calculatorModel.IsLastDigitPressed)
                calculatorModel.RegisterX = 0;

            calculatorModel.RegisterX *= 10;
            calculatorModel.RegisterX += digit;
            calculatorModel.IsLastDigitPressed = true;
        }

        public void PressClear(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = 0;
            calculatorModel.IsLastDigitPressed = false;
        }

        private void PressMoveXToY(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterY = calculatorModel.RegisterX;
            calculatorModel.IsLastDigitPressed = false;
        }

        public void PressOperation(CalculatorModel calculatorModel, string operationCode)
        {
            CompleteOperation(calculatorModel);
            PressMoveXToY(calculatorModel);
            calculatorModel.OperationCode = operationCode;
            calculatorModel.IsLastDigitPressed = false;
        }

        public void PressEqual(CalculatorModel calculatorModel)
        {
            CompleteOperation(calculatorModel);
            calculatorModel.IsLastDigitPressed = false;
        }

        private void CompleteOperation(CalculatorModel calculatorModel)
        {
            if (string.IsNullOrEmpty(calculatorModel.OperationCode))
                return;

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
                    else
                        calculatorModel.RegisterX = double.NaN;
                    break;
                case "^":
                    calculatorModel.RegisterX = Math.Pow(calculatorModel.RegisterY, calculatorModel.RegisterX);
                    break;
            }
            calculatorModel.OperationCode = null;
        }

        public void PressSquare(CalculatorModel model)
        {
            model.RegisterX = Math.Pow(model.RegisterX, 2);
            model.IsLastDigitPressed = false;
        }

        public void PressSqrt(CalculatorModel model)
        {
            if (model.RegisterX < 0)
                model.RegisterX = double.NaN;
            else
                model.RegisterX = Math.Sqrt(model.RegisterX);
            model.IsLastDigitPressed = false;
        }

        public void PressPercent(CalculatorModel model)
        {
            model.RegisterX /= 100.0;
            model.IsLastDigitPressed = false;
        }

        public void PressAbs(CalculatorModel model)
        {
            model.RegisterX = Math.Abs(model.RegisterX);
            model.IsLastDigitPressed = false;
        }

        public void PressPower(CalculatorModel model)
        {
            model.RegisterY = model.RegisterX;
            model.OperationCode = "^";
            model.IsLastDigitPressed = false;
        }
    }
}