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
        public void PressDigit(CalculatorState state, string digitText)
        {
            if (!byte.TryParse(digitText, out var digit))
            {
                return;
            }

            if (state.NeedClearX)
            {
                state.RegisterX = 0;
                state.DecimalMode = false;
                state.DecimalFactor = 0.1;
            }

            if (state.DecimalMode)
            {
                state.RegisterX += digit * state.DecimalFactor;
                state.DecimalFactor *= 0.1;
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
            }
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

        public void PressSqrt(CalculatorState state)
        {
            state.RegisterX = Math.Sqrt(state.RegisterX);
            state.NeedClearX = true;
        }

        public void PressDecimalSeparator(CalculatorState state)
        {
            // Если уже есть запятая, ничего не делаем
            if (state.DecimalMode) return;
            state.DecimalMode = true;
            state.DecimalFactor = 0.1;
        }
    }
}
