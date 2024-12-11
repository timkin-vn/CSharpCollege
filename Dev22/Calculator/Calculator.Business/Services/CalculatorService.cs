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

        public void InsertOperation(CalculatorState state, string opCode)
        {
            PerformOperation(state, state.OpCode);
            MoveXToY(state);
            state.OpCode = opCode;
            state.NeedClearX = true;
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
            state.OpCode = "^";
            state.NeedClearX = true;
        }

        public void SquareRoot(CalculatorState state)
        {
            if (state.XRegister >= 0)
            {
                state.XRegister = Math.Sqrt(state.XRegister);
            }
            else
            {
                state.XRegister = double.NaN; // Корень из отрицательного числа
            }
        }

        public void InsertDecimal(CalculatorState state)
        {
            if (!state.XRegister.ToString().Contains("."))
            {
                state.XRegister = double.Parse(state.XRegister.ToString() + ".");
            }
        }


        private void PerformOperation(CalculatorState state, string opCode)
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
                    }
                    else
                    {
                        state.XRegister = state.YRegister / state.XRegister;
                    }

                    break;
                case "^":
                    state.XRegister = Math.Pow(state.YRegister, state.XRegister);
                    break;

            }
        }

    }
}
