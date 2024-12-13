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
            if(state.NeedClear)
            {
                state.XRegister = 0;
            }
            if (state.IsDecimalMode)
            {
                state.DecimalFactor /= 10;
                state.XRegister += digit * state.DecimalFactor;
            }
            else
            {
                if (state.XRegister == 0 && digit == 0)
                {
                    return;
                }

                state.XRegister = state.XRegister * 10 + digit;
            }

            state.IsDecimalMode = false;
            state.NeedClear = false;


        }
        public void InsertDecimalPoint(CalculatorState state)
        {
            if (!state.IsDecimalMode)
            {
                state.IsDecimalMode = true;
                state.DecimalFactor = 1;
            }
        }



        public string CreateOPeratioonLog(CalculatorState state, string opCode)
        {
            var operationLog = $"{state.YRegister} {opCode} {state.XRegister} = ";
            state.OperationLog = null;
            return operationLog;
        }
        public string CreateOPeratioonLog(CalculatorState state, string opCode, double start_XRegister)
        {
            var operationLog = $" {opCode} {start_XRegister} = ";
            state.OperationLog = null;
            return operationLog;
        }
       
        public void SaveRelutat(CalculatorState state, string opCode, string operationLog)
        {

            state.OperationLog = operationLog + state.XRegister.ToString();

        }
        public void PowOperationOperation(CalculatorState state, string opCode)
        {
            var operationLog = CreateOPeratioonLog(state, opCode,state.XRegister);

            state.XRegister = state.XRegister * state.XRegister;
            SaveRelutat(state, opCode, operationLog);

            state.NeedClear = true;

        }
        public void RowOperation(CalculatorState state, string opCode)
        {
            var operationLog = CreateOPeratioonLog(state, opCode, state.XRegister);

            state.XRegister = Math.Sqrt(state.XRegister);
            SaveRelutat(state, opCode, operationLog);
            state.NeedClear = true;
        }
        public void Fractional_DivisionOperation(CalculatorState state, string opCode)
        {
            var operationLog = CreateOPeratioonLog(state, opCode, state.XRegister);

            state.XRegister = 1/state.XRegister;
            SaveRelutat(state, opCode, operationLog);
            state.NeedClear = true;
        }

        public void LnOperation(CalculatorState state, string opCode)
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


        public void Negativ_and_Positiv_Operation(CalculatorState state, string opCode)
        {
           var operationLog = CreateOPeratioonLog(state, opCode,state.XRegister);

            state.XRegister = -state.XRegister;

            SaveRelutat(state, opCode, operationLog);

            state.NeedClear = true;
        }

        public void Clear(CalculatorState state)
        {
            state.XRegister = 0;
            state.OperationLog = string.Empty;
        }
        public void ClearEm(CalculatorState state)
        {
            state.OperationLog = string.Empty;
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
            if (state.NeedClearX || state.XRegister == 0)
            {
                return;
            }

            if (state.IsDecimalMode)
            {
                state.XRegister = Math.Floor(state.XRegister * 10) / 10;
                state.DecimalFactor *= 10;

                if (state.DecimalFactor >= 1)
                {
                    state.IsDecimalMode = false;
                }
            }
            else
            {
                state.XRegister = Math.Floor(state.XRegister / 10);
            }
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
                case "%":
                    
                        state.XRegister = state.YRegister * (state.XRegister / 100.0);
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
