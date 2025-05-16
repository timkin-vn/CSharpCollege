using Calculator.Business.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
                state.RegisterX = 0;
                state.HasDecimalPoint = false;
                state.DecimalPlaces = 0;
                state.IsClearNeeded = false;
            }

            if (!int.TryParse(digitString, out var digit))
            {
                return;
            }

            if (state.HasDecimalPoint)
            {
                state.DecimalPlaces++;
                state.RegisterX += digit * Math.Pow(10, -state.DecimalPlaces);
            }
            else
            {
                state.RegisterX = state.RegisterX * 10 + digit;
            }
        }

        public void Clear(CalculatorState state)
        {
            state.RegisterX = 0;
            state.HasDecimalPoint = false;
            state.DecimalPlaces = 0;
        }

        public void MoveXtoY(CalculatorState state)
        {
            state.RegisterY = state.RegisterX;
            state.HasDecimalPoint = false;
            state.DecimalPlaces = 0;
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
                default:
                    break;
            }
        }

        public void PressOperation(CalculatorState state, string operationCode)
        {
            CompleteOperation(state, state.OperationCode);
            MoveXtoY(state);
            state.OperationCode = operationCode;
            state.IsClearNeeded = true;
        }

        public void PressEqual(CalculatorState state)
        {
            CompleteOperation(state, state.OperationCode);
            state.IsClearNeeded = true;
        }

        public void AddDecimal(CalculatorState state)
        {
            if (!state.HasDecimalPoint)
            {
                state.HasDecimalPoint = true;
                state.DecimalPlaces = 0;
                state.IsClearNeeded = false;
            }
        }
        public void PressSquare(CalculatorState state)
        {
            state.RegisterX = Math.Pow(state.RegisterX, 2);
            state.IsClearNeeded = true;
        }

        public void PressSquareRoot(CalculatorState state)
        {
            state.RegisterX = Math.Sqrt(state.RegisterX);
            state.IsClearNeeded = true;
        }

        public void PressLogarithm(CalculatorState state)
        {
            state.RegisterX = Math.Log(state.RegisterX);
            state.IsClearNeeded = true;
        }
    }
}
