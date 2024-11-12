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

        private void MakeOperation(CalculatorState state, string opCode)
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
                        break;
                    }

                    state.XRegister = state.YRegister / state.XRegister;
                    break;
            }
        }

        public string ZeroCheck(CalculatorState state)
        {
            if (state.XRegister == 0)
            {
                return "Деление на ноль невозможно"; 
            }
            else
            {
                return null; 
            }
        }

        public void OnePartNumber(CalculatorState state)
        {
            state.XRegister = 1 / state.XRegister;
        }

        public void RaiseToDegree(CalculatorState state)
        {
            state.XRegister = Math.Pow(state.XRegister, 2);
        }

        public void CalculateRoot(CalculatorState state)
        {
            state.XRegister = Math.Sqrt(state.XRegister);
        }

    }
}
