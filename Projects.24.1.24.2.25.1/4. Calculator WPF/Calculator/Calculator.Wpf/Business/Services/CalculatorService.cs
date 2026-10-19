using Calculator.Business.Models;
using System;
using System.Globalization;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        private void UpdateNumber(CalculatorModel model)
        {
            double number;
            string text = model.InputText.Replace(',', '.');

            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out number)
                && !double.IsNaN(number) && !double.IsInfinity(number))
            {
                model.RegisterX = number;
            }
            else
            {
                SetError(model);
            }
        }

        private void ShowResult(CalculatorModel model)
        {
            if (double.IsNaN(model.RegisterX) || double.IsInfinity(model.RegisterX))
            {
                SetError(model);
                return;
            }

            model.InputText = model.RegisterX.ToString("G15", CultureInfo.InvariantCulture).Replace('.', ',');
        }

        private void SetError(CalculatorModel model)
        {
            model.HasError = true;
            model.InputText = "Ошибка";
            model.OperationCode = "";
            model.LastOperation = "";
            model.IsLastDigitPressed = false;
            model.StartFreshOnDigit = true;
            model.AfterEquals = false;
        }

        public void PressDigit(CalculatorModel model, string digitString)
        {
            int digit;
            if (digitString == null || digitString.Length != 1 ||
                !int.TryParse(digitString, out digit) || digit < 0 || digit > 9)
                return;

            if (model.HasError)
                PressClear(model);

            if (!model.IsLastDigitPressed || model.StartFreshOnDigit)
            {
                model.InputText = "0";
                model.RegisterX = 0;
            }

            // New typed digits start a new number after '='.
            if (model.AfterEquals)
            {
                model.LastOperation = "";
                model.AfterEquals = false;
            }

            model.StartFreshOnDigit = false;
            // Prevent the display and double parsing from overflowing with huge inputs.
            int digitCount = 0;
            foreach (char ch in model.InputText)
                if (ch >= '0' && ch <= '9') digitCount++;
            if (digitCount >= 15 && model.InputText != "0")
                return;

            if (model.InputText == "0")
                model.InputText = digitString;
            else
                model.InputText += digitString;

            UpdateNumber(model);
            model.IsLastDigitPressed = true;
        }

        public void PressComma(CalculatorModel model)
        {
            if (model.HasError)
                PressClear(model);

            if (!model.IsLastDigitPressed || model.StartFreshOnDigit)
            {
                model.InputText = "0";
                model.RegisterX = 0;
            }

            model.StartFreshOnDigit = false;
            model.AfterEquals = false;
            model.LastOperation = "";

            if (!model.InputText.Contains(","))
                model.InputText += ",";

            model.IsLastDigitPressed = true;
        }

        public void PressClear(CalculatorModel model)
        {
            model.RegisterX = 0;
            model.RegisterY = 0;
            model.OperationCode = "";
            model.LastOperation = "";
            model.LastOperand = 0;
            model.InputText = "0";
            model.IsLastDigitPressed = false;
            model.StartFreshOnDigit = false;
            model.HasError = false;
            model.AfterEquals = false;
        }

        public void MoveXToY(CalculatorModel model)
        {
            model.RegisterY = model.RegisterX;
        }

        private bool CompleteOperation(CalculatorModel model, string operation, double right)
        {
            switch (operation)
            {
                case "+": model.RegisterX = model.RegisterY + right; break;
                case "-": model.RegisterX = model.RegisterY - right; break;
                case "*": model.RegisterX = model.RegisterY * right; break;
                case "/":
                    if (right == 0)
                    {
                        SetError(model);
                        return false;
                    }
                    model.RegisterX = model.RegisterY / right;
                    break;
                default: return false;
            }

            ShowResult(model);
            return !model.HasError;
        }

        public void PressOperation(CalculatorModel model, string operationCode)
        {
            if (model.HasError || (operationCode != "+" && operationCode != "-" &&
                                   operationCode != "*" && operationCode != "/"))
                return;

            if (model.OperationCode != "" && model.IsLastDigitPressed)
            {
                if (!CompleteOperation(model, model.OperationCode, model.RegisterX))
                    return;
            }

            // If the operator was pressed twice, just replace the operator.
            MoveXToY(model);
            model.OperationCode = operationCode;
            model.LastOperation = "";
            model.IsLastDigitPressed = false;
            model.StartFreshOnDigit = true;
            model.AfterEquals = false;
        }

        public void PressEqual(CalculatorModel model)
        {
            if (model.HasError)
                return;

            if (model.OperationCode != "")
            {
                if (!model.IsLastDigitPressed)
                    return;

                double right = model.RegisterX;
                string operation = model.OperationCode;
                if (!CompleteOperation(model, operation, right))
                    return;

                model.LastOperand = right;
                model.LastOperation = operation;
                model.OperationCode = "";
            }
            else if (model.AfterEquals && model.LastOperation != "")
            {
                MoveXToY(model);
                if (!CompleteOperation(model, model.LastOperation, model.LastOperand))
                    return;
            }
            else
            {
                return;
            }

            model.IsLastDigitPressed = false;
            model.StartFreshOnDigit = true;
            model.AfterEquals = true;
        }

        public void PressSquare(CalculatorModel model)
        {
            if (model.HasError)
                return;

            model.RegisterX = model.RegisterX * model.RegisterX;
            ShowResult(model);
            if (model.HasError)
                return;

            // The squared number can be used in a pending operation.
            model.IsLastDigitPressed = true;
            model.StartFreshOnDigit = true;
            model.LastOperation = "";
            model.AfterEquals = false;
        }
    }
}
