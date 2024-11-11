using Calculator.Business.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        public void InsertDigit(CalculatorState state, string digitText)
        {
            var digit = byte.Parse(digitText);
            if (state.NeedClearX)
            {
                state.XRegister = 0;
            }

            state.XRegister = state.XRegister * 10 + digit;
            state.NeedClearX = false;
        }
        
        public void Clear(CalculatorState state)
        {
            state.XRegister = 0;
        }

        public void InsertOperation(CalculatorState state,CalcHistory history, string opCode)
        {
            PerformOperation(state, history, state.OpCode);
            MoveXToY(state);
            state.OpCode = opCode;
            state.NeedClearX = true;
        }

        private void MoveXToY(CalculatorState state)
        {
            state.YRegister = state.XRegister;
        }

        private void PerformOperation(CalculatorState state, CalcHistory history, string opCode)
        {
            switch (opCode)
            {
                case "+":

                    history.first = state.XRegister;
                    history.last = state.YRegister;

                    state.XRegister += state.YRegister;

                    history.result = state.XRegister;
                    history.symbol = '+';
                    break;

                case "-":
                    history.first = state.XRegister;
                    history.last = state.YRegister;

                    state.XRegister = state.YRegister - state.XRegister;

                    history.result = state.XRegister;
                    history.symbol = '-';
                    break;

                case "*":
                    history.first = state.XRegister;
                    history.last = state.YRegister;

                    state.XRegister *= state.YRegister;

                    history.result = state.XRegister;
                    history.symbol = '*';
                    break;

                case "/":
                    if (state.XRegister == 0)
                    {
                        history.first = state.XRegister;
                        history.last = state.YRegister;

                        state.XRegister = double.NaN;

                        history.result = state.XRegister;
                        history.symbol = '/';
                    }
                    else
                    {
                        history.first = state.XRegister;
                        history.last = state.YRegister;

                        state.XRegister = state.YRegister / state.XRegister;

                        history.result = state.XRegister;
                        history.symbol = '/';
                    }

                    break;
            }
        }



    }
}
