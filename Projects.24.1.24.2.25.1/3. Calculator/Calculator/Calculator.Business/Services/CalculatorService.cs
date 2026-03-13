using Calculator.Business.Models;
using System;

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

        private double FastPow(double x, double n)
        {
            double res = 1;
            bool minus = n < 0;
            long exp = (long)n;

            if (minus) exp = -exp;

            while (exp > 0)
            {
                if (exp % 2 == 1)
                {
                    res *= x;
                }
                x *= x;
                exp /= 2;
            }

            return minus ? 1.0 / res : res;
        }

        private double Root(double x, double n)
        {
            if (x < 0 && n % 2 == 0) return -1;
            if (x == 0) return 0;

            bool neg = x < 0;
            if (neg) x = -x;

            double low = 0;
            double high = x < 1 ? 1 : x;
            double mid;

            for (int i = 0; i < 100; i++)
            {
                mid = (low + high) / 2;
                double pow = 1;
                for (int j = 0; j < n; j++) pow *= mid;

                if (pow > x) high = mid;
                else low = mid;
            }

            return neg ? -(low + high) / 2 : (low + high) / 2;
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
                    break;

                case "mod":
                    calculatorModel.RegisterX = calculatorModel.RegisterY % calculatorModel.RegisterX;
                    break;

                case "^":
                    calculatorModel.RegisterX = FastPow(calculatorModel.RegisterY, calculatorModel.RegisterX);
                    break;

                case "√^n":
                    calculatorModel.RegisterX = Root(calculatorModel.RegisterY, calculatorModel.RegisterX);
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
            calculatorModel.OperationCode = null;
        }
    }
}
