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
        public string CreateOPeratioonLog(CalculatorState state, string opCode, double start_XRegister)
        {
            var operationLog = $" {opCode} {start_XRegister} = ";
            state.OperationLog = null;
            return operationLog;
        }
        public string CreateOPeratioonLog(CalculatorState state, string opCode)
        {
            var operationLog = $"{state.YRegister} {opCode} {state.XRegister} = ";
            state.OperationLog = null;
            return operationLog;
        }
        public void SaveRelutat(CalculatorState state, string opCode, string operationLog)
        {

            state.OperationLog = operationLog + state.XRegister.ToString();

        }
        public void Negativ_and_Positiv_Operation(CalculatorState state, string opCode)
        {
            var operationLog = CreateOPeratioonLog(state, opCode, state.XRegister);

            state.XRegister = -state.XRegister;

            SaveRelutat(state, opCode, operationLog);

            state.NeedClear = true;
        }  
        public void Cos_Operation(CalculatorState state, string opCode)
        {
            var operationLog = CreateOPeratioonLog(state, opCode, state.XRegister);

            state.XRegister = Math.Cosh(state.XRegister);

            SaveRelutat(state, opCode, operationLog);

            state.NeedClear = true;
        } 
        public void Sin_Operation(CalculatorState state, string opCode)
        {
            var operationLog = CreateOPeratioonLog(state, opCode, state.XRegister);

            state.XRegister = Math.Sinh(state.XRegister);

            SaveRelutat(state, opCode, operationLog);

            state.NeedClear = true;
        }  
        public void Tag_Operation(CalculatorState state, string opCode)
        {
            var operationLog = CreateOPeratioonLog(state, opCode, state.XRegister);

            state.XRegister = Math.Tanh(state.XRegister);

            SaveRelutat(state, opCode, operationLog);

            state.NeedClear = true;
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

        private void MakeOperation(CalculatorState state, string opCode)
        {
            var operationLog = CreateOPeratioonLog(state, opCode);

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
