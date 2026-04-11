using Calculator.Business.Models;
using System;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        public void PressDigit(CalculatorModel model, string digit)
        {
            if (!model.IsLastDigitPressed)
            {
                model.RegisterX = 0;
                model.IsLastDigitPressed = true;
            }

            if (double.TryParse(digit, out double d))
            {
                model.RegisterX = model.RegisterX * 10 + d;
            }
        }

        public void PressOperation(CalculatorModel model, string operationCode)
        {
            if (!string.IsNullOrEmpty(model.OperationCode))
            {
                ExecuteCalculation(model);
            }
            model.RegisterY = model.RegisterX;
            model.OperationCode = operationCode;
            model.IsLastDigitPressed = false;
        }

        public void PressEqual(CalculatorModel model)
        {
            ExecuteCalculation(model);
            model.OperationCode = null;
            model.IsLastDigitPressed = false;
        }

        public void MoveXToY(CalculatorModel model)
        {
            model.RegisterY = model.RegisterX;
            model.IsLastDigitPressed = false;
        }

        public void PressClear(CalculatorModel model)
        {
            model.RegisterX = 0;
            model.RegisterY = 0;
            model.OperationCode = null;
            model.IsLastDigitPressed = false;
        }

        public void PressSqrt(CalculatorModel model)
        {
            if (model.RegisterX >= 0)
            {
                model.RegisterX = Math.Sqrt(model.RegisterX);
            }
            else
            {
                model.RegisterX = 0;
            }

            model.IsLastDigitPressed = false;
        }

        private void ExecuteCalculation(CalculatorModel model)
        {
            switch (model.OperationCode)
            {
                case "+": model.RegisterX = model.RegisterY + model.RegisterX; break;
                case "-": model.RegisterX = model.RegisterY - model.RegisterX; break;
                case "*": model.RegisterX = model.RegisterY * model.RegisterX; break;
                case "/":
                    model.RegisterX = model.RegisterX != 0 ? model.RegisterY / model.RegisterX : 0;
                    break;
                case "%": model.RegisterX = (model.RegisterY * model.RegisterX) / 100; break;
            }
        }
    }
}