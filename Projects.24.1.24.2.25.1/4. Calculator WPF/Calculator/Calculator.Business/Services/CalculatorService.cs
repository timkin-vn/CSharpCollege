using Calculator.Business.Models;
using System;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        public void PressDigit(CalculatorModel calculatorModel, string digitString)
        {
            if (!int.TryParse(digitString, out var digit)) return;
            if (!calculatorModel.IsLastDigitPressed) calculatorModel.RegisterX = 0;
            calculatorModel.RegisterX = calculatorModel.RegisterX * 10 + digit;
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
            switch (calculatorModel.OperationCode)
            {
                case "+": calculatorModel.RegisterX = calculatorModel.RegisterY + calculatorModel.RegisterX; break;
                case "-": calculatorModel.RegisterX = calculatorModel.RegisterY - calculatorModel.RegisterX; break;
                case "*": calculatorModel.RegisterX = calculatorModel.RegisterY * calculatorModel.RegisterX; break;
                case "/":
                    if (calculatorModel.RegisterX == 0) throw new DivideByZeroException("На ноль делить нельзя.");
                    calculatorModel.RegisterX = calculatorModel.RegisterY / calculatorModel.RegisterX;
                    break;
            }
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

        public void PressUnaryOperation(CalculatorModel calculatorModel, string operationCode)
        {
            double value = calculatorModel.RegisterX;
            switch (operationCode)
            {
                case "x²": calculatorModel.RegisterX = value * value; break;
                case "√x":
                    if (value < 0) throw new InvalidOperationException("Нельзя извлечь квадратный корень из отрицательного числа.");
                    calculatorModel.RegisterX = Math.Sqrt(value);
                    break;
                case "1/x":
                    if (value == 0) throw new DivideByZeroException("Нельзя вычислить 1 / 0.");
                    calculatorModel.RegisterX = 1 / value;
                    break;
                case "%": calculatorModel.RegisterX = value / 100.0; break;
                case "±": calculatorModel.RegisterX = -value; break;
            }
            calculatorModel.IsLastDigitPressed = false;
        }
    }
}
