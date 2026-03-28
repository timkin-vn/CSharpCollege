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
        private bool _isCommaPressed = false;
        private int _decimalPlaces = 0;

        public void PressDigit(CalculatorModel calculatorModel, string digitString)
        {
            if (!int.TryParse(digitString, out var digit)) return;

            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.RegisterX = 0;
                _isCommaPressed = false;
                _decimalPlaces = 0;
            }

            if (!_isCommaPressed)
            {
                calculatorModel.RegisterX = calculatorModel.RegisterX * 10 + digit;
            }
            else
            {
                _decimalPlaces++;
                calculatorModel.RegisterX += digit / Math.Pow(10, _decimalPlaces);
            }

            calculatorModel.IsLastDigitPressed = true;
        }

        public void PressComma(CalculatorModel calculatorModel)
        {
            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.RegisterX = 0;
                calculatorModel.IsLastDigitPressed = true;
            }
            _isCommaPressed = true;
        }

        private void CompleteOperation(CalculatorModel calculatorModel)
        {
            if (string.IsNullOrEmpty(calculatorModel.OperationCode)) return;

            switch (calculatorModel.OperationCode)
            {
                case "+": calculatorModel.RegisterX = calculatorModel.RegisterY + calculatorModel.RegisterX; break;
                case "-": calculatorModel.RegisterX = calculatorModel.RegisterY - calculatorModel.RegisterX; break;
                case "*": calculatorModel.RegisterX = calculatorModel.RegisterY * calculatorModel.RegisterX; break;
                case "/":
                    if (calculatorModel.RegisterX != 0)
                        calculatorModel.RegisterX = calculatorModel.RegisterY / calculatorModel.RegisterX;
                    break;
                case "x^y": 
                    calculatorModel.RegisterX = Math.Pow(calculatorModel.RegisterY, calculatorModel.RegisterX);
                    break;
            }

            calculatorModel.OperationCode = null;
        }

        public void PressOperation(CalculatorModel calculatorModel, string operationCode)
        {
            if (operationCode == "x²")
            {
                calculatorModel.RegisterX = Math.Pow(calculatorModel.RegisterX, 2);
                calculatorModel.IsLastDigitPressed = false;
                return;
            }

            if (calculatorModel.IsLastDigitPressed)
            {
                CompleteOperation(calculatorModel);
            }

            calculatorModel.RegisterY = calculatorModel.RegisterX;
            calculatorModel.OperationCode = operationCode;
            calculatorModel.IsLastDigitPressed = false;
            _isCommaPressed = false;
            _decimalPlaces = 0;
        }

        public void PressEqual(CalculatorModel calculatorModel)
        {
            CompleteOperation(calculatorModel);
            calculatorModel.IsLastDigitPressed = false;
            _isCommaPressed = false;
            _decimalPlaces = 0;
        }

        public void PressClear(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = 0;
            calculatorModel.RegisterY = 0;
            calculatorModel.OperationCode = null;
            calculatorModel.IsLastDigitPressed = false;
            _isCommaPressed = false;
            _decimalPlaces = 0;
        }
    }
}

