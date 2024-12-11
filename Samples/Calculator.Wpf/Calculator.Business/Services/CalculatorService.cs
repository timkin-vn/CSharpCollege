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
            MakeOperation(state, state.OpCodeRegister);
            MoveXToY(state);
            state.OpCodeRegister = opCode;
            state.NeedClear = true;
        }

        private void MoveXToY(CalculatorState state)
        {
            state.YRegister = state.XRegister;
        }

        public void Backspace(CalculatorState state)
        {
            var xText = state.XRegister.ToString();
            if (xText.Length > 1)
            {
                xText = xText.Substring(0, xText.Length - 1);
                state.XRegister = double.Parse(xText);
            }
            else
            {
                state.XRegister = 0;
            }
        }

        public void Square(CalculatorState state)
        {
            state.XRegister *= state.XRegister;
        }

        public void PrepareForExponentiation(CalculatorState state)
        {
            MoveXToY(state);
            state.OpCodeRegister = "^";
            state.NeedClear = true;
        }

        public void SquareRoot(CalculatorState state)
        {
            if (state.XRegister >= 0)
            {
                state.XRegister = Math.Sqrt(state.XRegister);
            }
            else
            {
                state.XRegister = double.NaN;
            }
        }


        public void InsertDecimal(CalculatorState state)
        {
            if (!state.XRegister.ToString().Contains("."))
            {
                state.XRegister = double.Parse(state.XRegister.ToString() + ".");
            }
        }


        private void MakeOperation(CalculatorState state, string opCode)
        {
            var operationLog = $"{state.YRegister} {opCode} {state.XRegister} = ";
            state.OperationLog = null;

            switch (opCode)
            {
                case "^":
                    state.XRegister = Math.Pow(state.YRegister, state.XRegister);
                    break;
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
            }

        }
    }
}
