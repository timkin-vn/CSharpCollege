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
            if (digitText == ",")
            {
                state.Isdou=true;
               
                return;
            }
            var digit = double.Parse(digitText);
            if (state.NeedClearX)
            {
                state.XRegister = 0;
            }

                state.XRegister = state.XRegister * (state.Isdou?1:10) + (state.Isdou?digit/ Math.Pow(10, ++state.Dec) : digit);
            

            state.NeedClearX = false;
        }

        public void Clear(CalculatorState state)
        {
            state.XRegister = 0;
            state.Isdou = false;
            state.Dec = 0;

        }

        public void InsertOperation(CalculatorState state, string opCode)
        {
            PerformOperation(state, state.OpCode);
            MoveXToY(state);
            state.OpCode = opCode;
            state.NeedClearX = true;
        }
        public void plus_minuss(CalculatorState state) {
            state.XRegister = -state.XRegister;
        }
        private void MoveXToY(CalculatorState state)
        {
            state.YRegister = state.XRegister;
            state.Isdou = false;
            state.Dec = 0;
        }

        private void PerformOperation(CalculatorState state, string opCode)
        {
            switch (opCode)
            {
                case "+/-":
                    state.XRegister = -state.XRegister;
                    break;
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
                    }
                    else
                    {
                        state.XRegister = state.YRegister / state.XRegister;
                    }

                    break;
            }
        }
    }
}
