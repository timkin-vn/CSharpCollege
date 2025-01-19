using Calculator.Business.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Services
{
    public class CalculatorLogic
    {
        public void InsertDigit(Models.CalculatorContext state, string digitString)
        {
            var digit = byte.Parse(digitString);
            if (state.NeedClear)
            {
                state.XRegister = 0;
            }

            state.XRegister = state.XRegister * 10 + digit;
            state.NeedClear = false;
        }

        public void Negativ_and_Positiv_Operation(Models.CalculatorContext state)
        {
            if (state.XRegister >= 0 || state.XRegister < 0)
            {
                state.XRegister = -state.XRegister;
            }

            state.NeedClear = true;
        }
        public void Cos_Operation(Models.CalculatorContext state)
        {
            state.XRegister =Math.Cos(state.XRegister);

            state.NeedClear = true;
        }  
        public void Sin_Operation(Models.CalculatorContext state)
        {
            state.XRegister = Math.Cos(state.XRegister);

            state.NeedClear = true;
        }

        public void Clear(Models.CalculatorContext state)
        {
            state.XRegister = 0;
        }

        public void PressOperation(Models.CalculatorContext state, string opCode)
        {
            MakeOperation(state, state.OpCodeRegister);
            MoveXToY(state);
            state.OpCodeRegister = opCode;
            state.NeedClear = true;
        }

        private void MoveXToY(Models.CalculatorContext state)
        {
            state.YRegister = state.XRegister;
        }

        private void MakeOperation(Models.CalculatorContext state, string opCode)
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
    }
}
