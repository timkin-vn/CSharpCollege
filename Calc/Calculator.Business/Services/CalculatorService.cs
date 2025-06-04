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

                case "%":
                    state.RegisterX = state.RegisterX * (state.RegisterY / 100);
                    break;

                case "sin":
                    state.RegisterX = Math.Sin(state.RegisterY);
                    break;

                case "cos":
                    state.RegisterX = Math.Cos(state.RegisterY);
                    break;

                case "tg":
                    state.RegisterX = Math.Tan(state.RegisterY);
                    break;

                case "x^2":
                    state.RegisterX = Math.Pow(state.RegisterY, 2);
                    break;

                case "√x":
                    state.RegisterX = Math.Sqrt(state.RegisterY);
                    break;

                case "x^y":
                    state.RegisterX= Math.Pow(state.RegisterY, state.RegisterX);
                    break;

                case "ln":
                    state.RegisterX = Math.Log(state.RegisterY);
                    break;

                case "log":
                    state.RegisterX = Math.Log(state.RegisterY, state.RegisterX);
                    break;

                case "x!":
                    double f=state.RegisterX-1;
                    while (f>0)
                    {
                        state.RegisterX *= f;
                        f--;
                    }
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
