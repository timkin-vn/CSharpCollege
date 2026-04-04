using Calculator.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        private bool _isDecimal = false;
        private int _decimalPower = 1;
        
        public void PressDigit(CalculatorModel calculatorModel, string digitString)
        {
            if (!int.TryParse(digitString, out var digit))
            {
                return;
            }
            
            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.RegisterX = 0;
                _isDecimal = false;
                _decimalPower = 1;
            }

            if (!_isDecimal)
            {
                // regular input
                calculatorModel.RegisterX = calculatorModel.RegisterX * 10 + digit;
            }
            else
            {
                // input after decimal
                calculatorModel.RegisterX += digit * Math.Pow(10, -_decimalPower);
                _decimalPower++;
            }
            
            calculatorModel.IsLastDigitPressed = true;
        }
        
        public void PressDecimal(CalculatorModel calculatorModel)
        {
            if (!_isDecimal)
            {
                _isDecimal = true;
                _decimalPower = 1;
            }
            calculatorModel.IsLastDigitPressed = true;
        }

        public void PressClear(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = 0;
            calculatorModel.IsLastDigitPressed = false;
            _isDecimal = false;
            _decimalPower = 1;
        }

        public void PressMoveXToY(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterY = calculatorModel.RegisterX;
            calculatorModel.IsLastDigitPressed = false;
        }
        
        public void PressPi(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = Math.PI;
            calculatorModel.IsLastDigitPressed = false;
            _isDecimal = false;
        }
        
        public void PressSqrt(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = Math.Sqrt(calculatorModel.RegisterX);
            calculatorModel.IsLastDigitPressed = false;
            _isDecimal = false;
        }

        public void PressOperation(CalculatorModel calculatorModel, string operationCode)
        {
            CompleteOperation(calculatorModel);

            PressMoveXToY(calculatorModel);
            calculatorModel.OperationCode = operationCode;
            calculatorModel.IsLastDigitPressed = false;
        }

        public void PressEqual(CalculatorModel calculatorModel)
        {
            CompleteOperation(calculatorModel);
            calculatorModel.IsLastDigitPressed = false;
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
                
                case "^":
                    calculatorModel.RegisterX = Math.Pow(calculatorModel.RegisterY, calculatorModel.RegisterX);
                    break;
            }
        }
    }
}
