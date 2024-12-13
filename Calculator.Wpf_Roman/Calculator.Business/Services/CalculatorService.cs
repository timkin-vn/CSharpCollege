using Calculator.Business.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

        public void Negativ_and_Positiv_Operation(CalculatorState state, string opCode)
        {
            var operationLog = $"{state.XRegister}   {opCode}   = ";
            state.OperationLog = null;
            state.XRegister = -state.XRegister;
            state.NeedClear = true;
            state.OperationLog = operationLog + state.XRegister.ToString();
        }
        public void DegretOperation(CalculatorState state, string opCode)
        {
            var operationLog = $"{state.XRegister} {opCode}  = ";
            state.OperationLog = null;
            state.XRegister = state.XRegister * state.XRegister;
            state.NeedClear = true;
            state.OperationLog = operationLog + state.XRegister.ToString();
        }
        public void RowOperation(CalculatorState state, string opCode)
        {
            var operationLog = $"{state.XRegister} {opCode}  = ";
            state.OperationLog = null;
            state.XRegister = Math.Sqrt(state.XRegister);
            state.NeedClear = true;
            state.OperationLog = operationLog + state.XRegister.ToString();
        }
        public void LogOperation(CalculatorState state, string opCode)
        {
            var operationLog = $"{state.XRegister} {opCode}  = ";
            state.OperationLog = null;
            state.XRegister = Math.Log(state.XRegister,2);
            state.NeedClear = true;
            state.OperationLog = operationLog + state.XRegister.ToString();
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
            }

        }
    }
}
