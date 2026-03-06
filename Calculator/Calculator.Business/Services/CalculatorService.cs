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
        // нажали на числа от 0 до 9
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

            if(calculatorModel.IsDrob == false) calculatorModel.RegisterX = calculatorModel.RegisterX * 10 + digit; // если целая часть, то прибавляем число к целой части
            else // если дробная часть
            {
                string chText = Convert.ToString(calculatorModel.RegisterX); // конвертирую число в текст
                bool isZap = false;
                for(int i = 0; i < chText.Length; i++) // ищу стоит ли в числе запятая
                    if (chText[i] == ',')
                    {
                        isZap = true;
                        break;
                    }
                // добавляю запятую к числу в виде текста и само число добавления
                if (isZap == false) chText += "," + digitString;
                else chText += digitString;
                calculatorModel.RegisterX = Convert.ToDouble(chText); // конечное число в виде текста перевожу к double
            }
            calculatorModel.IsLastDigitPressed = true;
        }

        public void PressClear(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = 0;
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
            calculatorModel.IsDrob = false;
        }

        public void PressOperation(CalculatorModel calculatorModel, string operationCode)
        {
            CompleteOperation(calculatorModel);

            MoveXToY(calculatorModel);
            calculatorModel.OperationCode = operationCode;
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.IsDrob = false;
        }

        public void PressEqual(CalculatorModel calculatorModel)
        {
            CompleteOperation(calculatorModel);
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.IsDrob = false;
        }

        public void PressSquare(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = calculatorModel.RegisterX * calculatorModel.RegisterX;
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.IsDrob = false;
        }
    }
}
