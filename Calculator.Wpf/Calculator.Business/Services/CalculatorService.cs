using Calculator.Business.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        public int DigitsAfterDot;
        public void InsertDigit(CalculatorState state, string digitString)
        {
            if (state.NeedClearX)
            {
                state.XRegister = 0;
            }
            state.NeedClearX = false;
            if (digitString == ".")
            {
                ++DigitsAfterDot;
            }
            else
            {
                var digit = byte.Parse(digitString);
                if (DigitsAfterDot==0)
                { 
                    state.XRegister = state.XRegister * 10 + digit;
                }
                else
                {
                    state.XRegister = state.XRegister + digit / Math.Pow(10.0, DigitsAfterDot);
                    ++DigitsAfterDot;
                }
            }
        }

        public void Clear(CalculatorState state)
        {
            state.XRegister = 0;
            state.YRegister = 0;
            state.OpCode = "";
            DigitsAfterDot = 0;
        }

        public void InsertOperation(CalculatorState state, string opCode)
        {
            PerformOperation(state, state.OpCode);
            MoveXToY(state);
            DigitsAfterDot = 0;
            state.OpCode = opCode;
            state.NeedClearX = true;
        }

        private void MoveXToY(CalculatorState state)
        {
            state.YRegister = state.XRegister;
        }

        private void PerformOperation(CalculatorState state, string opCode)
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
                    else
                    {
                        state.XRegister = state.YRegister / state.XRegister;
                        state.OperationLog = operationLog + state.XRegister.ToString();
                        break;
                    }
            }

        }
    }
}
