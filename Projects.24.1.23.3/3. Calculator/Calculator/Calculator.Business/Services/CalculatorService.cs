using Calculator.Business.Models;
using Microsoft.Win32;
using System.IO.Ports;
using System.Net;

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
            }

            state.RegisterX = state.RegisterX * 10 + digit;
            state.NeedClearX = false;
        }

        public void Clear(CalculatorState state)
        {
            state.RegisterX = 0;
            state.Operation = string.Empty;
        }
        public void ClearEntry(CalculatorState state)
        {
            state.RegisterX = 0;
            state.NeedClearX = false;
        }

        public void ToggleSign(CalculatorState state)
        {
            state.RegisterX = -state.RegisterX;
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
                    state.RegisterX = state.RegisterY * state.RegisterX / 100;
                    break;
            }
        }

        public void PressOperation(CalculatorState state, string operation)
        {
            if (!state.NeedClearX && !string.IsNullOrEmpty(state.Operation))
            {
                CompleteOperation(state, state.Operation);
            }
            MoveXToY(state);
            state.Operation = operation;
            state.NeedClearX = true;
        }

        public void PressEqual(CalculatorState state)
        {
            CompleteOperation(state, state.Operation);
            state.NeedClearX = true;
        }
    }
}
