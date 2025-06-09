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
        private bool _hasDecimalPoint = false;
        private double _decimalMultiplier = 0.1;

        public void PressDigit(CalculatorState state, string digitText)
        {
            if (!byte.TryParse(digitText, out var digit))
            {
                return;
            }

            if (state.NeedClearX)
            {
                state.RegisterX = 0;
                _hasDecimalPoint = false;
                _decimalMultiplier = 0.1;
            }

            if (_hasDecimalPoint)
            {
                state.RegisterX += digit * _decimalMultiplier;
                _decimalMultiplier *= 0.1;
            }
            else
            {
                state.RegisterX = state.RegisterX * 10 + digit;
            }

            state.NeedClearX = false;
        }

        public void Clear(CalculatorState state)
        {
            state.RegisterX = 0;
            state.Operation = string.Empty;
            _hasDecimalPoint = false;
            _decimalMultiplier = 0.1;
        }

        public void MoveXToY(CalculatorState state)
        {
            state.RegisterY = state.RegisterX;
        }

        public void CompleteOperation(CalculatorState state, string operation)
        {
            switch (operation)
            {
                case "+":
                    state.RegisterX = state.RegisterY + state.RegisterX;
                    break;

                case "-":
                    state.RegisterX = state.RegisterY - state.RegisterX;
                    break;

                case "*":
                    state.RegisterX = state.RegisterY * state.RegisterX;
                    break;

                case "/":
                    state.RegisterX = state.RegisterY / state.RegisterX;
                    break;

                case "%":
                    state.RegisterX = state.RegisterY * (state.RegisterX / 100);
                    break;
            }

            _hasDecimalPoint = false;
            _decimalMultiplier = 0.1;
        }

        public void PressOperation(CalculatorState state, string operation)
        {
            CompleteOperation(state, state.Operation);
            MoveXToY(state);
            state.Operation = operation;
            state.NeedClearX = true;
        }

        public void PressEqual(CalculatorState state)
        {
            CompleteOperation(state, state.Operation);
            state.NeedClearX = true;
        }

        public void PressPi(CalculatorState state)
        {
            if (state.NeedClearX || state.RegisterX == 0)
            {
                state.RegisterX = Math.PI;
            }
            else
            {
                state.RegisterX *= Math.PI;
            }

            _hasDecimalPoint = false;
            _decimalMultiplier = 0.1;
        }


        public void SquareRoot(CalculatorState state)
        {
            if (state.RegisterX < 0)
            {
                // Обработка ошибки для отрицательных чисел
                throw new InvalidOperationException("Cannot calculate square root of negative number");
            }

            state.RegisterX = Math.Sqrt(state.RegisterX);

            _hasDecimalPoint = false;
            _decimalMultiplier = 0.1;
        }

        public void DecimalPoint(CalculatorState state)
        {
            if (state.NeedClearX)
            {
                state.RegisterX = 0;
                state.NeedClearX = false;
            }

            if (!_hasDecimalPoint)
            {
                _hasDecimalPoint = true;
                _decimalMultiplier = 0.1;
            }
        }
    }
}