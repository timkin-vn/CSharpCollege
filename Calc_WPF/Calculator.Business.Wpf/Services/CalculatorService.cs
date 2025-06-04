
using Calculator.Business.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO.IsolatedStorage;
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
            state.bonus = 0;
        }

        public void MoveXToY(CalculatorState state)
        {
            state.RegisterY = state.RegisterX;
            state.IsFloat = false;
            state.bonus = 0;
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

                case "sin":
                    state.RegisterX = Math.Sin(state.RegisterX);
                    break;

                case "cos":
                    state.RegisterX = Math.Cos(state.RegisterX);
                    break;

                case "tg":
                    state.RegisterX = Math.Tan(state.RegisterX);
                    break;

                case "+/-":
                    state.RegisterX *= -1;
                    break;

                case "x^2":
                    state.RegisterX = Math.Pow(state.RegisterX, 2);
                    break;

                case "√x":
                    state.RegisterX = Math.Sqrt(state.RegisterX);
                    break;

                case "x^y":
                    state.RegisterX = Math.Pow(state.RegisterY, state.RegisterX);
                    break;

                case "x!":
                    double f = state.RegisterX-1;
                    while (f > 0)
                    {
                        state.RegisterX *= f;
                        f--;
                    }
                    break;

                case "log":
                    state.RegisterX = Math.Log(state.RegisterX);
                    break;

                case ".":
                    state.IsFloat = true;
                    state.RegisterX *= 1.0;
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
    }
}
