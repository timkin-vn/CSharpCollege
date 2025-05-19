using Calculator.Business.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

            state.InputBuffer += digitString;
            UpdateRegisterXFromBuffer(state);
        }

        public void PressDot(CalculatorState state)
        {
            if (state.IsClearNeeded)
            {
                state.InputBuffer = "";
                state.IsClearNeeded = false;
            }

            if (!state.InputBuffer.Contains("."))
            {
                if (string.IsNullOrEmpty(state.InputBuffer))
                    state.InputBuffer = "0.";

                else
                    state.InputBuffer += ".";
            }

            UpdateRegisterXFromBuffer(state);
        }

        public void PressSignChange(CalculatorState state)
        {
            if (string.IsNullOrEmpty(state.InputBuffer))
            {
                state.RegisterX = -state.RegisterX;
                state.InputBuffer = state.RegisterX.ToString();
            }
            else
            {
                if (state.InputBuffer.StartsWith("-"))
                    state.InputBuffer = state.InputBuffer.Substring(1);
                else
                    state.InputBuffer = "-" + state.InputBuffer;

                UpdateRegisterXFromBuffer(state);
            }
        }

        public void PressBackspace(CalculatorState state)
        {
            if (!string.IsNullOrEmpty(state.InputBuffer))
            {
                state.InputBuffer = state.InputBuffer.Substring(0, state.InputBuffer.Length - 1);
                UpdateRegisterXFromBuffer(state);
            }
        }

        public void ClearEntry(CalculatorState state)
        {
            state.InputBuffer = "";
            state.RegisterX = 0;
        }

        public void ClearAll(CalculatorState state)
        {
            state.InputBuffer = "";
            state.RegisterX = 0;
            state.RegisterY = 0;
            state.OperationCode = "";
            state.IsClearNeeded = false;
        }

        public void PressPi(CalculatorState state)
        {
            state.RegisterX = 3.1415926535;
            state.InputBuffer = state.RegisterX.ToString();
            state.IsClearNeeded = false;
        }

        public void MoveXToY(CalculatorState state)
        {
            state.RegisterY = state.RegisterX;
        }

        public void CompleteOperation(CalculatorState state, string operationCode)
        {
            switch (operationCode)
            {
                case "+":
                    state.RegisterX = state.RegisterY + state.RegisterX;
                    break;
                case "-":
                    state.RegisterX = state.RegisterY - state.RegisterX;
                    break;
                case "*":
                case "X":
                    state.RegisterX = state.RegisterY * state.RegisterX;
                    break;
                case "/":
                    state.RegisterX = (state.RegisterX != 0) ? state.RegisterY / state.RegisterX : 0;
                    break;
                case "%":
                    state.RegisterX = state.RegisterY % state.RegisterX;
                    break;
                case "x²":
                    state.RegisterX = Math.Pow(state.RegisterX, 2);
                    break;
                case "√":
                    state.RegisterX = state.RegisterX >= 0 ? Math.Sqrt(state.RegisterX) : 0;
                    break;
                case "1/x":
                    state.RegisterX = state.RegisterX != 0 ? 1 / state.RegisterX : 0;
                    break;
            }

            state.InputBuffer = state.RegisterX.ToString();
        }

        public void PressOperation(CalculatorState state, string operationCode)
        {
            if (!string.IsNullOrEmpty(state.


OperationCode))
                CompleteOperation(state, state.OperationCode);

            MoveXToY(state);
            state.OperationCode = operationCode;
            state.IsClearNeeded = true;
        }

        public void PressEqual(CalculatorState state)
        {
            CompleteOperation(state, state.OperationCode);
            state.OperationCode = "";
            state.IsClearNeeded = true;
        }

        private void UpdateRegisterXFromBuffer(CalculatorState state)
        {
            if (double.TryParse(state.InputBuffer, out var result))
            {
                state.RegisterX = result;
            }
        }
    }
}
