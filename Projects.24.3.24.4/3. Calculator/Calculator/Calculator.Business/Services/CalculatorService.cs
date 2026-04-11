using Calculator.Business.Models;
using System;
using System.Globalization;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        private readonly string _decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        public void PressDigit(CalculatorModel calculatorModel, string digitString)
        {
            if (string.IsNullOrWhiteSpace(digitString) || digitString.Length != 1 || !char.IsDigit(digitString[0]))
            {
                return;
            }

            if (calculatorModel.DisplayText == "Ошибка")
            {
                PressClear(calculatorModel);
            }

            if (calculatorModel.IsNewInput)
            {
                calculatorModel.DisplayText = "";
                calculatorModel.IsNewInput = false;
            }

            if (calculatorModel.DisplayText == "0")
            {
                calculatorModel.DisplayText = "";
            }

            calculatorModel.DisplayText += digitString;
        }

        public void PressDecimalSeparator(CalculatorModel calculatorModel)
        {
            if (calculatorModel.DisplayText == "Ошибка")
            {
                PressClear(calculatorModel);
            }

            if (calculatorModel.IsNewInput)
            {
                calculatorModel.DisplayText = "0" + _decimalSeparator;
                calculatorModel.IsNewInput = false;
                return;
            }

            if (!calculatorModel.DisplayText.Contains(_decimalSeparator))
            {
                calculatorModel.DisplayText += _decimalSeparator;
            }
        }

        public void PressBackspace(CalculatorModel calculatorModel)
        {
            if (calculatorModel.DisplayText == "Ошибка")
            {
                PressClear(calculatorModel);
                return;
            }

            if (calculatorModel.IsNewInput)
            {
                return;
            }

            if (calculatorModel.DisplayText.Length <= 1 ||
                (calculatorModel.DisplayText.Length == 2 && calculatorModel.DisplayText.StartsWith("-")))
            {
                calculatorModel.DisplayText = "0";
                calculatorModel.IsNewInput = true;
                return;
            }

            calculatorModel.DisplayText = calculatorModel.DisplayText.Substring(0, calculatorModel.DisplayText.Length - 1);

            if (calculatorModel.DisplayText.EndsWith(_decimalSeparator))
            {
                calculatorModel.DisplayText = calculatorModel.DisplayText.Substring(0, calculatorModel.DisplayText.Length - _decimalSeparator.Length);
            }

            if (string.IsNullOrWhiteSpace(calculatorModel.DisplayText) || calculatorModel.DisplayText == "-")
            {
                calculatorModel.DisplayText = "0";
                calculatorModel.IsNewInput = true;
            }
        }

        public void PressToggleSign(CalculatorModel calculatorModel)
        {
            if (calculatorModel.DisplayText == "Ошибка" || calculatorModel.DisplayText == "0")
            {
                return;
            }

            if (calculatorModel.DisplayText.StartsWith("-"))
            {
                calculatorModel.DisplayText = calculatorModel.DisplayText.Substring(1);
            }
            else
            {
                calculatorModel.DisplayText = "-" + calculatorModel.DisplayText;
            }

            calculatorModel.IsNewInput = false;
        }

        public void PressPercent(CalculatorModel calculatorModel)
        {
            if (calculatorModel.DisplayText == "Ошибка")
            {
                return;
            }

            var currentValue = GetDisplayValue(calculatorModel);

            if (string.IsNullOrEmpty(calculatorModel.OperationCode))
            {
                currentValue = currentValue / 100.0;
            }
            else
            {
                currentValue = calculatorModel.RegisterY * currentValue / 100.0;
            }

            SetDisplayValue(calculatorModel, currentValue);
            calculatorModel.IsNewInput = false;
        }

        public void PressSquareRoot(CalculatorModel calculatorModel)
        {
            if (calculatorModel.DisplayText == "Ошибка")
            {
                return;
            }

            var currentValue = GetDisplayValue(calculatorModel);
            if (currentValue < 0)
            {
                return;
            }

            SetDisplayValue(calculatorModel, Math.Sqrt(currentValue));
            calculatorModel.IsNewInput = true;
        }

        public void PressClear(CalculatorModel calculatorModel)
        {
            calculatorModel.DisplayText = "0";
            calculatorModel.RegisterY = 0;
            calculatorModel.OperationCode = null;
            calculatorModel.LastOperationCode = null;
            calculatorModel.LastOperationValue = 0;
            calculatorModel.IsNewInput = true;
        }

        public void PressOperation(CalculatorModel calculatorModel, string operationCode)
        {
            if (calculatorModel.DisplayText == "Ошибка")
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(operationCode))
            {
                return;
            }

            if (!string.IsNullOrEmpty(calculatorModel.OperationCode) && !calculatorModel.IsNewInput)
            {
                if (!CompleteOperation(calculatorModel, GetDisplayValue(calculatorModel)))
                {
                    return;
                }
            }
            else if (!calculatorModel.IsNewInput)
            {
                calculatorModel.RegisterY = GetDisplayValue(calculatorModel);
            }

            calculatorModel.OperationCode = operationCode;
            calculatorModel.IsNewInput = true;
        }

        public void PressEqual(CalculatorModel calculatorModel)
        {
            if (calculatorModel.DisplayText == "Ошибка")
            {
                return;
            }

            if (!string.IsNullOrEmpty(calculatorModel.OperationCode))
            {
                var rightValue = calculatorModel.IsNewInput
                    ? calculatorModel.RegisterY
                    : GetDisplayValue(calculatorModel);

                calculatorModel.LastOperationValue = rightValue;
                calculatorModel.LastOperationCode = calculatorModel.OperationCode;

                if (!CompleteOperation(calculatorModel, rightValue))
                {
                    return;
                }

                calculatorModel.OperationCode = null;
                calculatorModel.IsNewInput = true;
                return;
            }

            if (!calculatorModel.IsRepeatedEqualEnabled || string.IsNullOrEmpty(calculatorModel.LastOperationCode))
            {
                return;
            }

            var leftValue = GetDisplayValue(calculatorModel);
            var result = PerformOperation(calculatorModel, leftValue, calculatorModel.LastOperationValue, calculatorModel.LastOperationCode);

            if (!result.HasValue)
            {
                return;
            }

            calculatorModel.RegisterY = result.Value;
            SetDisplayValue(calculatorModel, result.Value);
            calculatorModel.IsNewInput = true;
        }

        private bool CompleteOperation(CalculatorModel calculatorModel, double rightValue)
        {
            var result = PerformOperation(calculatorModel, calculatorModel.RegisterY, rightValue, calculatorModel.OperationCode);

            if (!result.HasValue)
            {
                return false;
            }

            calculatorModel.RegisterY = result.Value;
            SetDisplayValue(calculatorModel, result.Value);
            return true;
        }

        private double? PerformOperation(CalculatorModel calculatorModel, double leftValue, double rightValue, string operationCode)
        {
            switch (operationCode)
            {
                case "+":
                    return leftValue + rightValue;

                case "-":
                    return leftValue - rightValue;

                case "*":
                    return leftValue * rightValue;

                case "/":
                    if (rightValue == 0 && calculatorModel.IsDivisionByZeroHandled)
                    {
                        calculatorModel.DisplayText = "Ошибка";
                        calculatorModel.OperationCode = null;
                        calculatorModel.LastOperationCode = null;
                        calculatorModel.LastOperationValue = 0;
                        calculatorModel.IsNewInput = true;
                        return null;
                    }

                    return leftValue / rightValue;
            }

            return rightValue;
        }

        private double GetDisplayValue(CalculatorModel calculatorModel)
        {
            if (double.TryParse(calculatorModel.DisplayText, out var value))
            {
                return value;
            }

            return 0;
        }

        private void SetDisplayValue(CalculatorModel calculatorModel, double value)
        {
            calculatorModel.DisplayText = value.ToString(CultureInfo.CurrentCulture);
        }
    }
}
