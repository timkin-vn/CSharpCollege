using Calculator.Business.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        public void InsertDigit(CalculatorState state, string digitText)
        {
            var digit = byte.Parse(digitText);
            if (state.NeedClearX)
            {
                state.XRegister = 0;
            }

            state.XRegister = state.XRegister * 10 + digit;
            state.NeedClearX = false;
        }

        public void Clear(CalculatorState state)
        {
            state.XRegister = 0;
        }

        public void InsertOperation(CalculatorState state, string opCode)
        {
            PerformOperation(state, state.OpCode);
            MoveXToY(state);
            state.OpCode = opCode;
            state.NeedClearX = true;
        }

        private void MoveXToY(CalculatorState state)
        {
            state.YRegister = state.XRegister;
        }

        private void PerformOperation(CalculatorState state, string opCode)
        {
            switch (opCode)
            {
                case "+":
                    state.XRegister += state.YRegister;
                    break;

                case "-":
                    state.XRegister = state.YRegister - state.XRegister;
                    break;

                case "*":
                    state.XRegister *= state.YRegister;
                    break;

                case "/":
                    if (state.XRegister == 0)
                    {
                        state.XRegister = double.NaN;
                    }
                    else
                    {
                        state.XRegister = state.YRegister / state.XRegister;
                    }
                    break;

                case "^":
                    state.XRegister = state.YRegister * state.YRegister;
                    break;

                case "√":
                    state.XRegister = Math.Sqrt(state.YRegister);
                    break;
            }
        }
    }
}