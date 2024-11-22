using Calculator.Business.Models;
using System;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        public void InsertDigit(CalculatorState state, string digitString)
        {
            var digit = byte.Parse(digitString);
            if (state.NeedClear)
            {
                state.XRegister = 0;
            }

            state.XRegister = state.XRegister * 10 + digit;
            state.NeedClear = false;
        }

        public void Clear(CalculatorState state)
        {
            state.XRegister = 0;
        }

        public void PressOperation(CalculatorState state, string opCode)
        {
            if (opCode == "√")
            {
                MakeOperation(state, opCode);
            }
            else
            {
                MakeOperation(state, state.OpCodeRegister);
                MoveXToY(state);
                state.OpCodeRegister = opCode;
                state.NeedClear = true;
            }
        }

        private void MoveXToY(CalculatorState state)
        {
            state.YRegister = state.XRegister;
        }

        private void MakeOperation(CalculatorState state, string opCode)
        {
            var operationLog = $"{state.YRegister} {opCode} {state.XRegister} = ";
            state.OperationLog = null;

            switch (opCode)
            {
                case "+":
                    state.XRegister += state.YRegister;
                    state.OperationLog = operationLog + state.XRegister.ToString();
                    break;
                case "-":
                    state.XRegister = state.YRegister - state.XRegister;
                    state.OperationLog = operationLog + state.XRegister.ToString();
                    break;
                case "*":
                    state.XRegister *= state.YRegister;
                    state.OperationLog = operationLog + state.XRegister.ToString();
                    break;
                case "/":
                    if (state.XRegister == 0)
                    {
                        state.XRegister = double.NaN;
                        state.OperationLog = operationLog + state.XRegister.ToString();
                        break;
                    }

                    state.XRegister = state.YRegister / state.XRegister;
                    state.OperationLog = operationLog + state.XRegister.ToString();
                    break;
                case "^":
                    state.XRegister = Math.Pow(state.YRegister, state.XRegister);
                    state.OperationLog = operationLog + state.XRegister.ToString();
                    break;
                case "√":
                    state.XRegister = Math.Sqrt(state.XRegister);
                    state.OperationLog = operationLog + state.XRegister.ToString();
                    break;
            }
        }
    }
}