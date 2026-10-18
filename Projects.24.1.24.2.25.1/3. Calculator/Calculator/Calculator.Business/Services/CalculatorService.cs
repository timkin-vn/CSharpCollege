
using Calculator.Business.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        private readonly List<string> _history = new List<string>();

        public IReadOnlyList<string> History
        {
            get { return _history.AsReadOnly(); }
        }

        public void PressDigit(
            CalculatorModel calculatorModel,
            string digitString)
        {
            if (!int.TryParse(digitString, out int digit))
            {
                return;
            }

            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.RegisterX = 0;
            }

            calculatorModel.RegisterX =
                calculatorModel.RegisterX * 10 + digit;

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
            if (string.IsNullOrEmpty(calculatorModel.OperationCode))
            {
                return;
            }

            double x = calculatorModel.RegisterX;
            double y = calculatorModel.RegisterY;
            double result;

            switch (calculatorModel.OperationCode)
            {
                case "+":
                    result = y + x;
                    break;

                case "-":
                    result = y - x;
                    break;

                case "*":
                    result = y * x;
                    break;

                case "/":
                    if (x == 0)
                    {
                        throw new DivideByZeroException(
                            "Делить на ноль нельзя.");
                    }

                    result = y / x;
                    break;

                default:
                    return;
            }

            if (double.IsNaN(result) || double.IsInfinity(result))
            {
                throw new ArithmeticException(
                    "Результат невозможно представить числом.");
            }

            string expression =
                y.ToString("G", CultureInfo.CurrentCulture) +
                " " + calculatorModel.OperationCode + " " +
                x.ToString("G", CultureInfo.CurrentCulture) +
                " = " +
                result.ToString("G", CultureInfo.CurrentCulture);

            _history.Insert(0, expression);

            calculatorModel.RegisterX = result;
        }

        public void PressOperation(
            CalculatorModel calculatorModel,
            string operationCode)
        {
            if (operationCode != "+" &&
                operationCode != "-" &&
                operationCode != "*" &&
                operationCode != "/")
            {
                return;
            }

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

        public void ClearHistory()
        {
            _history.Clear();
        }
    }
}
