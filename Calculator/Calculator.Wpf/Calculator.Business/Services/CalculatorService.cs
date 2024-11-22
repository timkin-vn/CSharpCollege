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

        public void Negativ_and_Positiv_Operation(CalculatorState state)
        {
            state.XRegister = -state.XRegister;

            state.NeedClearX = true;
        }

        private void PerformOperation(CalculatorState state, string opCode)
        {
            var log = $"{state.YRegister} {opCode} {state.XRegister} = ";
            state.OperationLog = null;

            switch (opCode)
            {
                case "+":
                    state.XRegister += state.YRegister;
                    state.OperationLog = log + state.XRegister.ToString();
                    break;

                case "-":
                    state.XRegister = state.YRegister - state.XRegister;
                    state.OperationLog = log + state.XRegister.ToString();
                    break;

                case "*":
                    state.XRegister *= state.YRegister;
                    state.OperationLog = log + state.XRegister.ToString();
                    break;

                case "x²":
                    state.XRegister = state.XRegister * state.XRegister;
                    state.OperationLog = $"x² {state.XRegister}";
                    break;

                case "√":
                    if (state.XRegister < 0)
                    {
                        state.XRegister = double.NaN;
                        break;
                    }
                    state.XRegister = Math.Sqrt(state.XRegister);
                    state.OperationLog = $"√ {state.XRegister}";
                    break;

                case "+/-":
                    state.XRegister = -state.XRegister;
                    state.OperationLog = $"+/- {state.XRegister}";
                    break;
            }
        }
    }
}
