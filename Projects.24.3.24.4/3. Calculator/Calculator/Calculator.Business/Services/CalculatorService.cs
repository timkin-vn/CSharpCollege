using Calculator.Business.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
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
                calculatorModel.InputString = "";
            }

            calculatorModel.InputString += digitString;

            if (double.TryParse(calculatorModel.InputString, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
            {
                calculatorModel.RegisterX = value;
            }

            calculatorModel.DisplayText = calculatorModel.InputString;
            calculatorModel.IsLastDigitPressed = true;
        }
        
        public void PressDecimal(CalculatorModel calculatorModel)
        {
            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.InputString = "0";
            }

            if (!calculatorModel.InputString.Contains("."))
            {
                if (string.IsNullOrEmpty(calculatorModel.InputString)) 
                    calculatorModel.InputString = "0";

                calculatorModel.InputString += ".";
            }

            calculatorModel.DisplayText = calculatorModel.InputString;
            calculatorModel.IsLastDigitPressed = true;
        }

        public void PressClear(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = 0;
            calculatorModel.RegisterY = 0;
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.InputString = "";
            calculatorModel.DisplayText = "0";
            calculatorModel.OperationCode = null;
        }

        public void PressMoveXToY(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterY = calculatorModel.RegisterX;
            calculatorModel.IsLastDigitPressed = false;
        }
        
        public void PressSin(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = Math.Sin(calculatorModel.RegisterX * Math.PI/180.0);
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.DisplayText = calculatorModel.RegisterX.ToString(CultureInfo.InvariantCulture);
        }

        public void PressCos(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = Math.Cos(calculatorModel.RegisterX * Math.PI/180.0);
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.DisplayText = calculatorModel.RegisterX.ToString(CultureInfo.InvariantCulture);
        }

        public void PressTg(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = Math.Tan(calculatorModel.RegisterX * Math.PI/180.0);
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.DisplayText = calculatorModel.RegisterX.ToString(CultureInfo.InvariantCulture);
        }

        public void PressCtg(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = 1.0 / Math.Tan(calculatorModel.RegisterX * Math.PI/180.0);
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.DisplayText = calculatorModel.RegisterX.ToString(CultureInfo.InvariantCulture);
        }

        public void PressOperation(CalculatorModel calculatorModel, string operationCode)
        {
            CompleteOperation(calculatorModel);

            PressMoveXToY(calculatorModel);
            calculatorModel.OperationCode = operationCode;
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.DisplayText = calculatorModel.RegisterX.ToString(CultureInfo.InvariantCulture);
        }

        private void CompleteOperation(CalculatorModel calculatorModel)
        {
            if (string.IsNullOrEmpty(calculatorModel.OperationCode)) return;

            switch (calculatorModel.OperationCode)
            {
                case "+":
                    calculatorModel.RegisterX = calculatorModel.RegisterY + calculatorModel.RegisterX;
                    break;

                case "-":
                    calculatorModel.RegisterX = calculatorModel.RegisterY - calculatorModel.RegisterX;
                    break;

                case "*":
                    calculatorModel.RegisterX = calculatorModel.RegisterY * calculatorModel.RegisterX;
                    break;

                case "/":
                    calculatorModel.RegisterX = calculatorModel.RegisterY / calculatorModel.RegisterX;
                    break;
            }
        }
    }
}