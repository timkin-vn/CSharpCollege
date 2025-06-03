using Calculator.Business.Models;
using System;
using System.Globalization;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        public void PressDigit(CalculatorState state, string digitString)
        {
            if (state.IsClearNeeded)
            {
                state.InputBuffer = "";
                state.IsClearNeeded = false;
            }

            if (state.InputBuffer == "0")
                state.InputBuffer = digitString;
            else
                state.InputBuffer += digitString;

            UpdateRegisterX(state);
        }

        public void PressDecimalPoint(CalculatorState state)
        {
            if (state.IsClearNeeded)
            {
                state.InputBuffer = "0";
                state.IsClearNeeded = false;
            }

            if (!state.InputBuffer.Contains("."))
                state.InputBuffer += ".";

            UpdateRegisterX(state);
        }

        public void Clear(CalculatorState state)
        {
            state.RegisterX = 0;
            state.RegisterY = 0;
            state.OperationCode = null;
            state.InputBuffer = "0";
            state.IsClearNeeded = false;
        }

        public void Backspace(CalculatorState state)
        {
            if (state.IsClearNeeded)
            {
                state.InputBuffer = "0";
                state.IsClearNeeded = false;
            }
            else
            {
                if (state.InputBuffer.Length > 1)
                    state.InputBuffer = state.InputBuffer.Substring(0, state.InputBuffer.Length - 1);
                else
                    state.InputBuffer = "0";
            }

            UpdateRegisterX(state);
        }


        public void MoveXToY(CalculatorState state)
        {
            state.RegisterY = state.RegisterX;
        }

        public void CompleteOperation(CalculatorState state, string operationCode)
        {
            switch (operationCode)
            {
                case "+": state.RegisterX = state.RegisterY + state.RegisterX; break;
                case "-": state.RegisterX = state.RegisterY - state.RegisterX; break;
                case "*": state.RegisterX = state.RegisterY * state.RegisterX; break;
                case "/":
                    state.RegisterX = (state.RegisterX == 0) ? 0 : state.RegisterY / state.RegisterX;
                    break;
            }

            state.InputBuffer = state.RegisterX.ToString("0.################", CultureInfo.InvariantCulture);
        }

        public void PressOperation(CalculatorState state, string operationCode)
        {
            UpdateRegisterX(state);

            if (!string.IsNullOrEmpty(state.OperationCode))
                CompleteOperation(state, state.OperationCode);
            else
                MoveXToY(state);

            state.OperationCode = operationCode;
            state.IsClearNeeded = true;
        }

        public void PressEqual(CalculatorState state)
        {

            UpdateRegisterX(state);

            if (!string.IsNullOrEmpty(state.OperationCode))
            {
                CompleteOperation(state, state.OperationCode);
                state.OperationCode = null;
                state.IsClearNeeded = true;
            }


        }

        private void UpdateRegisterX(CalculatorState state)
        {
            if (double.TryParse(state.InputBuffer, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
                state.RegisterX = result;
        }
        public void Square(CalculatorState state)
        {
            if (double.TryParse(state.InputBuffer, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
            {
                state.RegisterX = Math.Pow(value, 2);
                state.InputBuffer = state.RegisterX.ToString("0.################", CultureInfo.InvariantCulture);
                state.IsClearNeeded = true;
            }
        }

    }
}
