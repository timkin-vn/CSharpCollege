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
        private const int MaxDecimalDigits = 8;

        public void PressDigit(CalculatorModel calculatorModel, string digitString)
        {
            if (calculatorModel.HasError || !int.TryParse(digitString, out var digit))
            {
                return;
            }

            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.RegisterX = 0;
                ResetEntry(calculatorModel);
            }

            if (calculatorModel.IsDecimalEntry)
            {
                if (calculatorModel.DecimalDigits >= MaxDecimalDigits)
                {
                    return;
                }

                calculatorModel.DecimalDigits++;
                var step = digit * Math.Pow(10, -calculatorModel.DecimalDigits);
                var value = calculatorModel.RegisterX < 0
                    ? calculatorModel.RegisterX - step
                    : calculatorModel.RegisterX + step;
                calculatorModel.RegisterX = Math.Round(value, calculatorModel.DecimalDigits);
            }
            else
            {
                calculatorModel.RegisterX = calculatorModel.RegisterX < 0
                    ? calculatorModel.RegisterX * 10 - digit
                    : calculatorModel.RegisterX * 10 + digit;
            }

            calculatorModel.IsLastDigitPressed = true;
        }

        public void PressDecimalPoint(CalculatorModel calculatorModel)
        {
            if (calculatorModel.HasError || (calculatorModel.IsDecimalEntry && calculatorModel.IsLastDigitPressed))
            {
                return;
            }

            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.RegisterX = 0;
                calculatorModel.DecimalDigits = 0;
            }

            calculatorModel.IsDecimalEntry = true;
            calculatorModel.IsLastDigitPressed = true;
        }

        public void PressBackspace(CalculatorModel calculatorModel)
        {
            if (calculatorModel.HasError || !calculatorModel.IsLastDigitPressed)
            {
                return;
            }

            if (calculatorModel.IsDecimalEntry)
            {
                if (calculatorModel.DecimalDigits > 0)
                {
                    var scaled = (long)Math.Round(calculatorModel.RegisterX * Math.Pow(10, calculatorModel.DecimalDigits));
                    scaled /= 10;
                    calculatorModel.DecimalDigits--;
                    calculatorModel.RegisterX = scaled / Math.Pow(10, calculatorModel.DecimalDigits);
                }
                else
                {
                    calculatorModel.IsDecimalEntry = false;
                }
            }
            else
            {
                calculatorModel.RegisterX = Math.Truncate(calculatorModel.RegisterX / 10);
            }
        }

        public void PressToggleSign(CalculatorModel calculatorModel)
        {
            if (calculatorModel.HasError || calculatorModel.RegisterX == 0)
            {
                return;
            }

            calculatorModel.RegisterX = -calculatorModel.RegisterX;
        }

        public void PressClear(CalculatorModel calculatorModel)
        {
            if (calculatorModel.HasError)
            {
                calculatorModel.HasError = false;
                calculatorModel.RegisterY = 0;
                calculatorModel.OperationCode = null;
            }

            calculatorModel.RegisterX = 0;
            calculatorModel.IsLastDigitPressed = false;
            ResetEntry(calculatorModel);
        }

        public void MoveXToY(CalculatorModel calculatorModel)
        {
            if (calculatorModel.HasError)
            {
                return;
            }

            calculatorModel.RegisterY = calculatorModel.RegisterX;
        }

        private void CompleteOperation(CalculatorModel calculatorModel)
        {
            if (calculatorModel.OperationCode == null)
            {
                return;
            }

            var left = calculatorModel.RegisterY;
            var right = calculatorModel.RegisterX;
            double result;

            switch (calculatorModel.OperationCode)
            {
                case "+":
                    result = left + right;
                    break;

                case "-":
                    result = left - right;
                    break;

                case "*":
                    result = left * right;
                    break;

                case "/":
                    if (right == 0)
                    {
                        calculatorModel.HasError = true;
                        return;
                    }

                    result = left / right;
                    break;

                default:
                    return;
            }

            calculatorModel.History.Add(
                $"{Format(left)} {calculatorModel.OperationCode} {Format(right)} = {Format(result)}");
            calculatorModel.RegisterX = result;
        }

        public void PressOperation(CalculatorModel calculatorModel, string operationCode)
        {
            if (calculatorModel.HasError)
            {
                return;
            }

            if (!calculatorModel.IsLastDigitPressed && calculatorModel.OperationCode != null)
            {
                calculatorModel.OperationCode = operationCode;
                return;
            }

            CompleteOperation(calculatorModel);

            if (calculatorModel.HasError)
            {
                return;
            }

            MoveXToY(calculatorModel);
            calculatorModel.OperationCode = operationCode;
            calculatorModel.IsLastDigitPressed = false;
            ResetEntry(calculatorModel);
        }

        public void PressEqual(CalculatorModel calculatorModel)
        {
            if (calculatorModel.HasError)
            {
                return;
            }

            CompleteOperation(calculatorModel);

            if (calculatorModel.HasError)
            {
                return;
            }

            calculatorModel.OperationCode = null;
            calculatorModel.IsLastDigitPressed = false;
            ResetEntry(calculatorModel);
        }

        private static void ResetEntry(CalculatorModel calculatorModel)
        {
            calculatorModel.IsDecimalEntry = false;
            calculatorModel.DecimalDigits = 0;
        }

        private static string Format(double value)
        {
            return value.ToString("G15");
        }
    }
}