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
            {
                return;
            }

            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.RegisterX = 0;
            }

            calculatorModel.RegisterX = calculatorModel.RegisterX * 10 + digit;
            calculatorModel.IsLastDigitPressed = true;
        }

        public void PressClear(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = 0;
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
                    if (calculatorModel.RegisterX == 0)
                    {
                        calculatorModel.RegisterX = double.NaN; // Деление на ноль
                    }
                    else
                    {
                        calculatorModel.RegisterX = calculatorModel.RegisterY / calculatorModel.RegisterX;
                    }
                    break;
            }
        }

        // НОВЫЙ МЕТОД: Вычисление факториала
        private double Factorial(double n)
        {
            // Проверка на целое число и неотрицательность
            if (n < 0 || Math.Truncate(n) != n)
            {
                return double.NaN; // Факториал определен только для неотрицательных целых чисел
            }

            int number = (int)n;
            long result = 1;

            for (int i = 2; i <= number; i++)
            {
                result *= i;
            }

            return result;
        }

        public void PressOperation(CalculatorModel calculatorModel, string operationCode)
        {
            // Особый случай для π - не требует вычислений с Y
            if (operationCode == "π")
            {
                calculatorModel.RegisterX = Math.PI;
                calculatorModel.IsLastDigitPressed = true;
                return;
            }

            // Особый случай для x² - работает с текущим числом
            if (operationCode == "x²")
            {
                calculatorModel.RegisterX = calculatorModel.RegisterX * calculatorModel.RegisterX;
                calculatorModel.IsLastDigitPressed = true;
                return;
            }

            // Особый случай для n! - работает с текущим числом
            if (operationCode == "n!")
            {
                calculatorModel.RegisterX = Factorial(calculatorModel.RegisterX);
                calculatorModel.IsLastDigitPressed = true;
                return;
            }

            // Для обычных операций (+, -, *, /)
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

        public void PressChangeSign(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = -calculatorModel.RegisterX;
        }

        public void PressBackspace(CalculatorModel calculatorModel)
        {
            var text = calculatorModel.RegisterX.ToString();

            if (text.Length <= 1 || (text.Length == 2 && text.StartsWith("-")))
            {
                calculatorModel.RegisterX = 0;
            }
            else
            {
                text = text.Substring(0, text.Length - 1);

                if (text.EndsWith(",") || text.EndsWith("."))
                {
                    text = text.Substring(0, text.Length - 1);
                }

                calculatorModel.RegisterX = double.Parse(text);
            }

            calculatorModel.IsLastDigitPressed = true;
        }
    }
}