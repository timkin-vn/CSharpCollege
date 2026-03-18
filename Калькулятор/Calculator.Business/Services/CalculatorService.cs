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
        public void PressDigit(CalculatorModel model, string digitString)
        {
            if (!int.TryParse(digitString, out var digit))
            {
                return;
            }

            if (!model.IsLastDigitPressed)
            {
                model.RegisterX = 0;
                model.IsDecimalMode = false;
                model.DecimalMultiplier = 0.1;
            }

            if (model.IsDecimalMode)
            {
                model.RegisterX += digit * model.DecimalMultiplier;
                model.DecimalMultiplier /= 10;
            }
            else
            {
                model.RegisterX = model.RegisterX * 10 + digit;
            }

            model.IsLastDigitPressed = true;
        }

        public void PressDecimal(CalculatorModel model, string separator)
        {
            if (!model.IsDecimalMode)
            {
                model.IsDecimalMode = true;
                model.DecimalMultiplier = 0.1;
                model.IsLastDigitPressed = true;
            }
        }

        public void PressClear(CalculatorModel model)
        {
            model.RegisterX = 0;
            model.RegisterY = 0;
            model.OperationCode = null;
            model.IsLastDigitPressed = false;
            model.IsDecimalMode = false;
            model.DecimalMultiplier = 0.1;
        }

        public void MoveXToY(CalculatorModel model)
        {
            model.RegisterY = model.RegisterX;
        }

        private void CompleteOperation(CalculatorModel model)
        {
            if (string.IsNullOrEmpty(model.OperationCode))
                return;

            switch (model.OperationCode)
            {
                case "+":
                    model.RegisterX = model.RegisterY + model.RegisterX;
                    break;
                case "-":
                    model.RegisterX = model.RegisterY - model.RegisterX;
                    break;
                case "*":
                    model.RegisterX = model.RegisterY * model.RegisterX;
                    break;
                case "/":
                    if (model.RegisterX != 0)
                        model.RegisterX = model.RegisterY / model.RegisterX;
                    else
                        model.RegisterX = 0;
                    break;
            }
        }

        public void PressOperation(CalculatorModel model, string operationCode)
        {
            string code = operationCode;
            if (code == "×" || code == "x" || code == "X") code = "*";
            if (code == "÷") code = "/";
            if (!string.IsNullOrEmpty(model.OperationCode) && model.IsLastDigitPressed)
            {
                CompleteOperation(model);
                MoveXToY(model);
            }
            else
            {
                if (string.IsNullOrEmpty(model.OperationCode))
                {
                    MoveXToY(model);
                }
            }

            model.OperationCode = code;
            model.IsLastDigitPressed = false;
            model.IsDecimalMode = false;
            model.DecimalMultiplier = 0.1;
        }

        public void PressEqual(CalculatorModel model)
        {
            CompleteOperation(model);
            model.OperationCode = null;
            model.IsLastDigitPressed = false;
            model.IsDecimalMode = false;
            model.DecimalMultiplier = 0.1;
        }

        public void PressSqrt(CalculatorModel model)
        {
            if (model.RegisterX >= 0)
            {
                model.RegisterX = Math.Sqrt(model.RegisterX);
                model.IsLastDigitPressed = false;
            }
        }

        public void PressSqr(CalculatorModel model)
        {
            model.RegisterX = model.RegisterX * model.RegisterX;
            model.IsLastDigitPressed = false;
        }
    }
}