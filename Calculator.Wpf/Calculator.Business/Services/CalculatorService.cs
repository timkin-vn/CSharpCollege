using System;
using Calculator.Business.Models;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        private bool _hasDecimalPoint;
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
                state.NeedClearX = false;
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
        }

        public void Clear(CalculatorState state)
        {
            state.RegisterX = 0;
            state.RegisterY = 0;
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
                    state.RegisterX = state.RegisterX == 0 ? double.NaN : state.RegisterY / state.RegisterX;
                    break;
                case "%":
                    if (state.RegisterY == 0)
                        state.RegisterX = state.RegisterX * (state.RegisterX / 100.0); 
                    else
                        state.RegisterX = state.RegisterY * (state.RegisterX / 100.0);
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

        public void SquareRoot(CalculatorState state)
        {
            if (state.RegisterX >= 0)
            {
                state.RegisterX = Math.Sqrt(state.RegisterX);
                state.NeedClearX = true;
            }
        }

        public void PressPi(CalculatorState state)
        {
            state.RegisterX = Math.PI;
            state.NeedClearX = true;
        }

        public void DecimalPoint(CalculatorState state)
        {
            if (!_hasDecimalPoint)
            {
                _hasDecimalPoint = true;
                if (state.NeedClearX)
                {
                    state.RegisterX = 0;
                    state.NeedClearX = false;
                }
            }
        }
    }
}