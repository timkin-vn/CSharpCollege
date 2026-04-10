using CalculatorWPF.Models;
using System;
using System.Linq;

namespace CalculatorWPF.Services
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
                calculatorModel.IsDrob = false;
            }

            if (!calculatorModel.IsDrob)
            {
                calculatorModel.RegisterX = calculatorModel.RegisterX * 10 + digit;
            }
            else
            {
                string chText = Convert.ToString(calculatorModel.RegisterX, System.Globalization.CultureInfo.InvariantCulture);
                chText = chText.Replace('.', ',');

                bool isZap = chText.Contains(',');

                if (!isZap)
                    chText += "," + digitString;
                else
                    chText += digitString;

                calculatorModel.RegisterX = Convert.ToDouble(chText, System.Globalization.CultureInfo.InvariantCulture);
            }
            calculatorModel.IsLastDigitPressed = true;
        }

        public void PressClear(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = 0;
            calculatorModel.RegisterY = 0;
            calculatorModel.OperationCode = "";
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.IsDrob = false;
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
                    calculatorModel.RegisterX = calculatorModel.RegisterY + calculatorModel.RegisterX;
                    break;
                case "-":
                    calculatorModel.RegisterX = calculatorModel.RegisterY - calculatorModel.RegisterX;
                    break;
                case "*":
                    calculatorModel.RegisterX = calculatorModel.RegisterY * calculatorModel.RegisterX;
                    break;
                case "/":
                    if (calculatorModel.RegisterX != 0)
                        calculatorModel.RegisterX = calculatorModel.RegisterY / calculatorModel.RegisterX;
                    else
                        calculatorModel.RegisterX = 0;
                    break;
            }
            calculatorModel.IsDrob = false;
        }

        public void PressOperation(CalculatorModel calculatorModel, string operationCode)
        {
            if (!string.IsNullOrEmpty(calculatorModel.OperationCode) && calculatorModel.IsLastDigitPressed)
            {
                CompleteOperation(calculatorModel);
            }

            MoveXToY(calculatorModel);
            calculatorModel.OperationCode = operationCode;
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.IsDrob = false;
        }

        public void PressEqual(CalculatorModel calculatorModel)
        {
            if (!string.IsNullOrEmpty(calculatorModel.OperationCode))
            {
                CompleteOperation(calculatorModel);
                calculatorModel.OperationCode = "";
                calculatorModel.IsLastDigitPressed = false;
                calculatorModel.IsDrob = false;
            }
        }

        public void PressSquare(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = calculatorModel.RegisterX * calculatorModel.RegisterX;
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.IsDrob = false;
        }
    }
}