using Calculator.Business.Models;
using System;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        private int digitsAfterDot = 0;

        public void InsertDigit(CalculatorState state, string digitString)
        {
            if (state.NeedClearX)
            {
                state.XRegister = 0;
                digitsAfterDot = 0;
            }
            state.NeedClearX = false;

            if (digitString == ".")
            {
                if (digitsAfterDot == 0)
                {
                    digitsAfterDot = 1;
                }
            }
            else
            {
                var digit = int.Parse(digitString);
                if (digitsAfterDot == 0)
                {
                    state.XRegister = state.XRegister * 10 + digit;
                }
                else
                {
                    state.XRegister += digit / Math.Pow(10.0, digitsAfterDot);
                    digitsAfterDot++;
                }
            }
        }

        public void Clear(CalculatorState state)
        {
            state.XRegister = 0;
            state.YRegister = 0;
            state.OpCode = "";
            digitsAfterDot = 0;
        }

        public void InsertOperation(CalculatorState state, string opCode)
        {
            PerformOperation(state, state.OpCode);
            MoveXToY(state);
            digitsAfterDot = 0;
            state.OpCode = opCode;
            state.NeedClearX = true;
        }

        private void MoveXToY(CalculatorState state)
        {
            state.YRegister = state.XRegister;
        }

        private void PerformOperation(CalculatorState state, string opCode)
        {
            if (string.IsNullOrEmpty(opCode))
            {
                return;
            }

            var operationLog = $"{state.YRegister} {opCode} {state.XRegister} = ";
            state.OperationLog = null;

            try
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
                            throw new DivideByZeroException("Cannot divide by zero.");
                        }
                        state.XRegister = state.YRegister / state.XRegister;
                        break;
                    default:
                        throw new ArgumentException("Invalid operation code.");
                }
                state.OperationLog = operationLog + state.XRegister.ToString();
            }
            catch (Exception ex)
            {
                state.OperationLog = ex.Message;
            }
        }
    }
}