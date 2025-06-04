using Calculator.Business.Models;
using System;

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

            if (state.IsFloat)
            {
                state.RegisterX = state.RegisterX + digit / state.bonus;
                state.bonus *= 10.0;
                state.IsClearNeeded = false;
            }
            else
            {
                state.RegisterX = state.RegisterX * 10 + digit;
                state.IsClearNeeded = false;
            }
        }

        public void Clear(CalculatorState state)
        {
            state.RegisterX = 0;
            state.RegisterY = 0;
            state.IsFloat = false;
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
            }
        }

        public void PressOperation(CalculatorState state, string operationCode)
        {
            CompleteOperation(state, state.OperationCode);
            MoveXToY(state);
            state.bonus = 10.0;
            state.OperationCode = operationCode;
            state.IsClearNeeded = true;
        }

        public void PressEqual(CalculatorState state)
        {
            CompleteOperation(state, state.OperationCode);
            state.IsClearNeeded = true;
        }

        public void Backspace(CalculatorState state)
        {
            string str = state.RegisterX.ToString();

            if (str.Length > 1)
            {
                str = str.Substring(0, str.Length - 1);

                if (str == "." || str == "-")
                {
                    str = "0";
                    state.IsFloat = false;
                }

                double result;
                if (double.TryParse(str, out result))
                {
                    state.RegisterX = result;
                }
                else
                {
                    state.RegisterX = 0;
                }
            }
            else
            {
                state.RegisterX = 0;
                state.IsFloat = false;
            }

            state.IsClearNeeded = false;
        }

        public void SquareRoot(CalculatorState state)
        {
            if (state.RegisterX >= 0)
            {
                state.RegisterX = Math.Sqrt(state.RegisterX);
                state.IsClearNeeded = true;
            }
            else
            {
                state.RegisterX = 0;
            }
        }
    }
}
