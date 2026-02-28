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
            if (!int.TryParse(digitString, out var digit))
            {
                return;
            }

            if (state.IsClearNeeded)
            {
                state.RegisterX = 0;
            }

            state.RegisterX = state.RegisterX * 10 + digit;
            state.IsClearNeeded = false;
        }

        public void Clear(CalculatorState state)
        {
            state.RegisterX = 0;
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
                    state.RegisterX = state.RegisterY * state.RegisterX;
                    break;

                case "/":
                    state.RegisterX = state.RegisterY / state.RegisterX;
                    break;

                case "^":
                    state.RegisterX = Math.Pow(state.RegisterY, state.RegisterX);
                    break;

            }
        }

        public void PressOperation(CalculatorState state, string operationCode)
        {
            CompleteOperation(state, state.OperationCode);
            MoveXToY(state);
            state.OperationCode = operationCode;
            state.IsClearNeeded = true;
        }

        public void PressEqual(CalculatorState state)
        {
            CompleteOperation(state, state.OperationCode);
            state.IsClearNeeded = true;
        }
        public void ToggleSign(CalculatorState state)
        {
            state.RegisterX = -state.RegisterX;
        }

        public void Backspace(CalculatorState state)
        {
            var xStr = ((int)state.RegisterX).ToString();
            if (xStr.Length > 1)
                xStr = xStr.Substring(0, xStr.Length - 1);
            else
                xStr = "0";

            state.RegisterX = double.Parse(xStr);
        }

        public void PressSqrt(CalculatorState state)
        {
            if (state.RegisterX >= 0)
            {
                state.RegisterX = Math.Sqrt(state.RegisterX);
                state.IsClearNeeded = true;
            }
            else
            {
                state.RegisterX = double.NaN;
            }
        }

    }
}
