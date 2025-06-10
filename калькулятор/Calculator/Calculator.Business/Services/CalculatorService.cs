﻿using Calculator.Business.Models;
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

            state.RegisterX = state.RegisterX * 10 + digit;
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
            MoveXToY(state);
            state.OperationCode = operationCode;
        }

        public void PressEqual(CalculatorState state)
        {
            CompleteOperation(state, state.OperationCode);
        }

        public void PressBackspace(CalculatorState state)
        {
            if (state.RegisterX == 0 || state.RegisterX < 1)
                return;

            state.RegisterX = Math.Floor(state.RegisterX / 10);
        }


    }
}
