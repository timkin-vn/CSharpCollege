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
        public void PressDigit(CalculatorModel calculatorModel, string digitString)
        {
            if (!int.TryParse(digitString, out var digit))
                return;

            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.RegisterX = 0;
                calculatorModel.IsDecimalMode = false;
                calculatorModel.DecimalFactor = 0.1;
            }

            if (!calculatorModel.IsDecimalMode)
            {
                // обычное число
                calculatorModel.RegisterX = calculatorModel.RegisterX * 10 + digit;
            }
            else
            {
                // дробная часть
                calculatorModel.RegisterX += digit * calculatorModel.DecimalFactor;
                calculatorModel.DecimalFactor /= 10;
            }

            calculatorModel.IsLastDigitPressed = true;
        }

        public void PressClear(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = 0;
            calculatorModel.IsLastDigitPressed = false;

            calculatorModel.IsDecimalMode = false;
            calculatorModel.DecimalFactor = 0.1;
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
                    calculatorModel.RegisterX = calculatorModel.RegisterX + calculatorModel.RegisterY;
                    break;

                case "-":
                    calculatorModel.RegisterX = calculatorModel.RegisterY - calculatorModel.RegisterX;
                    break;

                case "*":
                    calculatorModel.RegisterX = calculatorModel.RegisterX * calculatorModel.RegisterY;
                    break;

                case "/":
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
            calculatorModel.IsLastDigitPressed = false;
        }
        public void PressDecimal(CalculatorModel calculatorModel)
        {
            if (!calculatorModel.IsDecimalMode)
            {
                calculatorModel.IsDecimalMode = true;
                calculatorModel.DecimalFactor = 0.1;
            }
        }
        public void Square(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = calculatorModel.RegisterX * calculatorModel.RegisterX;
            calculatorModel.IsLastDigitPressed = false;
        }

        public void Sqrt(CalculatorModel calculatorModel)
        {
            if (calculatorModel.RegisterX < 0)
            {
                throw new Exception("Нельзя извлечь корень из отрицательного числа");
            }

            calculatorModel.RegisterX = Math.Sqrt(calculatorModel.RegisterX);
            calculatorModel.IsLastDigitPressed = false;
        }
    }
}
