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
        private int _decimalPlaces = 0;
        public void PressDigit(CalculatorModel calculatorModel, string digitString)
        {
            if (!int.TryParse(digitString, out var digit))
            {
                return;
            }

            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.RegisterX = 0;
                _decimalPlaces = 0;
            }

            if (_decimalPlaces > 0)
            {
                // Для цифр после первой (сотые, тысячные и т.д.)
                calculatorModel.RegisterX += digit * Math.Pow(10, -(_decimalPlaces + 1));
                _decimalPlaces++;
            }
            else if (_decimalPlaces < 0)
            {
                // ПЕРВАЯ ЦИФРА ПОСЛЕ ЗАПЯТОЙ (десятые)
                calculatorModel.RegisterX += digit * 0.1;
                _decimalPlaces = 1; // Переключаем в режим для последующих цифр
            }
            else
            {
                // Обычный ввод целой части
                calculatorModel.RegisterX *= 10;
                calculatorModel.RegisterX += digit;
            }

            calculatorModel.IsLastDigitPressed = true;
        }

        public void PressDecimalPoint(CalculatorModel calculatorModel)
        {
            if (_decimalPlaces > 0)
                return;

            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.RegisterX = 0;
            }

            _decimalPlaces = -1;
            calculatorModel.IsLastDigitPressed = true;
        }

        public void PressClear(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = 0;
            calculatorModel.IsLastDigitPressed = false;
            _decimalPlaces = 0;
        }

        public void PressMoveXToY(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterY = calculatorModel.RegisterX;
            calculatorModel.IsLastDigitPressed = false;
            _decimalPlaces = 0;
        }

        public void PressOperation(CalculatorModel calculatorModel, string operationCode)
        {
            CompleteOperation(calculatorModel);

            PressMoveXToY(calculatorModel);
            calculatorModel.OperationCode = operationCode;
            calculatorModel.IsLastDigitPressed = false;
            _decimalPlaces = 0;
        }

        public void PressEqual(CalculatorModel calculatorModel)
        {
            CompleteOperation(calculatorModel);
            calculatorModel.IsLastDigitPressed = false;
            _decimalPlaces = 0;
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
                        calculatorModel.RegisterX = double.NaN;
                    }
                    else
                    {
                        calculatorModel.RegisterX = calculatorModel.RegisterY / calculatorModel.RegisterX;
                    }
                    break;
            }
        }
    }
}
