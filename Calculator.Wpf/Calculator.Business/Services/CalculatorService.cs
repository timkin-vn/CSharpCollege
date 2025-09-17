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
        public void DegretOperation(CalculatorState state, string opCode)
        {
            var operationLog = CreateOPeratioonLog(state, opCode, state.XRegister);

            state.XRegister = state.XRegister * state.XRegister;
            SaveRelutat(state, opCode, operationLog);

            state.NeedClear = true;

        }   
        public void RowOperation(CalculatorState state,string opCode)
        {
            var operationLog = CreateOPeratioonLog(state, opCode,state.XRegister);

            state.XRegister = Math.Sqrt(state.XRegister );
            SaveRelutat(state, opCode ,operationLog);
            state.NeedClear = true;
        }
        public string CreateOPeratioonLog(CalculatorState state,string opCode,double start_XRegister)
        {
            var operationLog = $" {opCode} {start_XRegister} = ";
            state.OperationLog = null;
            return operationLog;
        }  
        public string CreateOPeratioonLog(CalculatorState state,string opCode)
        {
            var operationLog = $"{state.YRegister} {opCode} {state.XRegister} = ";
            state.OperationLog = null;
            return operationLog;
        }
        public void  SaveRelutat(CalculatorState state, string opCode,string operationLog) 
        {
               
               state.OperationLog = operationLog + state.XRegister.ToString();
            
        }

        public void ABSOperation(CalculatorState state, string opCode)
        {
            var operationLog = CreateOPeratioonLog(state, opCode, state.XRegister);
            if (state.XRegister >= 0)
            {
                state.XRegister = state.XRegister;
            }
            else
            {
                state.XRegister = -state.XRegister;
            }
            SaveRelutat(state, opCode, operationLog);
            state.NeedClear = true;
        } 
        public void  LnOperation(CalculatorState state, string opCode)
        {
            var operationLog = CreateOPeratioonLog(state, opCode, state.XRegister);
            if (state.XRegister <= 0)
            {
                state.XRegister = double.NaN;
                
            }
            else
            {
                state.XRegister = Math.Log10(state.XRegister);
               

            }
            SaveRelutat(state, opCode, operationLog);
            state.NeedClear = true;
            
        }
        

        public void  Negativ_and_Positiv_Operation(CalculatorState state, string opCode) 
        {
            var operationLog = CreateOPeratioonLog(state, opCode, state.XRegister);

            state.XRegister = -state.XRegister;

            SaveRelutat(state, opCode, operationLog);

            state.NeedClear = true;
        }
        private void MakeOperation(CalculatorState state, string opCode)
        {
            var operationLog = CreateOPeratioonLog(state, opCode);

            switch (opCode)
            {
                case "+":
                    state.XRegister += state.YRegister;
                    SaveRelutat(state, opCode, operationLog);
                    break;
                case "-":
                    state.XRegister = state.YRegister - state.XRegister;
                    SaveRelutat(state, opCode, operationLog); ;
                    break;
                case "*":
                    state.XRegister *= state.YRegister;
                    SaveRelutat(state, opCode, operationLog);
                    break;
                case "/":
                    if (state.XRegister == 0)
                    {
                        state.XRegister = double.NaN;
                        SaveRelutat(state, opCode, operationLog);
                        break;
                    }

                    state.XRegister = state.YRegister / state.XRegister;
                    SaveRelutat(state, opCode, operationLog);
                    break;  
                case "logXY":
                    if (state.XRegister <= 0)
                    {
                        state.XRegister = double.NaN;
                        SaveRelutat(state, opCode, operationLog);
                        break;
                    }

                    state.XRegister = Math.Log(state.YRegister  ,state.XRegister);
                    SaveRelutat(state, opCode, operationLog);
                    break;
            }

        }
    }
}
