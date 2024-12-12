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
            if (opCode != "Sin" && opCode != "Cos")
            { 
                MoveXToY(state); 
            }
            state.OpCodeRegister = opCode;
            state.NeedClear = true;
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
                case "Sin":
                    state.XRegister = Math.Sin(state.XRegister);
                    state.OperationLog = operationLog + state.XRegister.ToString();
                    break;

                case "Cos":
                    state.XRegister = Math.Cos(state.XRegister);
                    state.OperationLog = operationLog + state.XRegister.ToString();
                    break;
            }

        }
    }
}
