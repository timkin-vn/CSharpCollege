using Calculator.Business.Models;
using System;
using System.Globalization;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        public void PressDigit(CalculatorModel calculatorModel, string digitString)
        {
            if (!int.TryParse(digitString, out _))
            {
                return;
            }

            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.CurrentInput = "0";
                calculatorModel.RegisterX = 0;
            }

            if (calculatorModel.CurrentInput == "0")
            {
                calculatorModel.CurrentInput = digitString;
            }
            else
            {
                calculatorModel.CurrentInput += digitString;
            }

            calculatorModel.RegisterX = Parse(calculatorModel.CurrentInput);
            calculatorModel.DisplayText = calculatorModel.CurrentInput;
            calculatorModel.IsLastDigitPressed = true;
        }

        public void PressDecimalSeparator(CalculatorModel calculatorModel)
        {
            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.CurrentInput = "0";
                calculatorModel.RegisterX = 0;
                calculatorModel.IsLastDigitPressed = true;
            }

            if (calculatorModel.CurrentInput.Contains("."))
            {
                return;
            }

            calculatorModel.CurrentInput += ".";
            calculatorModel.DisplayText = calculatorModel.CurrentInput;
        }

        public void PressClear(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = 0;
            calculatorModel.RegisterY = 0;
            calculatorModel.OperationCode = null;
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.CurrentInput = "0";
            calculatorModel.DisplayText = "0";
        }

        public void PressMoveXToY(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterY = calculatorModel.RegisterX;
            calculatorModel.IsLastDigitPressed = false;
        }

        public void PressOperation(CalculatorModel calculatorModel, string operationCode)
        {
            CompleteOperation(calculatorModel);

            PressMoveXToY(calculatorModel);
            calculatorModel.OperationCode = operationCode;
            calculatorModel.CurrentInput = Format(calculatorModel.RegisterX);
            calculatorModel.DisplayText = calculatorModel.CurrentInput;
        }

        public void PressEqual(CalculatorModel calculatorModel)
        {
            CompleteOperation(calculatorModel);
            calculatorModel.OperationCode = null;
            calculatorModel.CurrentInput = Format(calculatorModel.RegisterX);
            calculatorModel.DisplayText = calculatorModel.CurrentInput;
            calculatorModel.IsLastDigitPressed = false;
        }

        public void PressPercent(CalculatorModel calculatorModel)
        {
            if (!string.IsNullOrEmpty(calculatorModel.OperationCode))
            {
                calculatorModel.RegisterX = calculatorModel.RegisterY * calculatorModel.RegisterX / 100;
            }
            else
            {
                calculatorModel.RegisterX = calculatorModel.RegisterX / 100;
            }

            calculatorModel.CurrentInput = Format(calculatorModel.RegisterX);
            calculatorModel.DisplayText = calculatorModel.CurrentInput;
            calculatorModel.IsLastDigitPressed = false;
        }

        public void PressSquare(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = calculatorModel.RegisterX * calculatorModel.RegisterX;
            calculatorModel.CurrentInput = Format(calculatorModel.RegisterX);
            calculatorModel.DisplayText = calculatorModel.CurrentInput;
            calculatorModel.IsLastDigitPressed = false;
        }

        public void PressSquareRoot(CalculatorModel calculatorModel)
        {
            if (calculatorModel.RegisterX < 0)
            {
                return;
            }

            calculatorModel.RegisterX = Math.Sqrt(calculatorModel.RegisterX);
            calculatorModel.CurrentInput = Format(calculatorModel.RegisterX);
            calculatorModel.DisplayText = calculatorModel.CurrentInput;
            calculatorModel.IsLastDigitPressed = false;
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
                    calculatorModel.RegisterX = calculatorModel.RegisterX == 0 ? 0 : calculatorModel.RegisterY / calculatorModel.RegisterX;
                    break;
            }

            if (!string.IsNullOrEmpty(calculatorModel.OperationCode))
            {
                calculatorModel.DisplayText = Format(calculatorModel.RegisterX);
            }
        }

        private double Parse(string text)
        {
            if (text.EndsWith("."))
            {
                text = text.TrimEnd('.');
            }

            if (string.IsNullOrEmpty(text))
            {
                return 0;
            }

            return double.Parse(text, CultureInfo.InvariantCulture);
        }

        private string Format(double value)
        {
            return value.ToString("0.############", CultureInfo.InvariantCulture);
        }
    }
}
