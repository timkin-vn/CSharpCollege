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
            NewMethod(state, operationCode);
        }

        private static void NewMethod(CalculatorState state, string operationCode)
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
                    if (state.RegisterX != 0)
                    {
                        state.RegisterX = state.RegisterY / state.RegisterX;
                    }
                    else
                    {
                        state.RegisterX = 0;
                    }
                    break;

                case "xⁿ":
                    state.RegisterX = Math.Pow(state.RegisterY, state.RegisterX);
                    break;

                case "√":
                    if (state.RegisterX >= 0)
                    {
                        state.RegisterX = Math.Sqrt(state.RegisterX);
                    }
                    else
                    {
                        state.RegisterX = 0;
                    }
                    break;

                case "cos":
                    state.RegisterX = Math.Cos(state.RegisterX);
                    break;

                case "sin":
                    state.RegisterX = Math.Sin(state.RegisterX);
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
    }
}
