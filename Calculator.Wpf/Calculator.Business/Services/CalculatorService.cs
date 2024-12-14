using Calculator.Business.Entites;
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

        public void InsertDegree(CalculatorState state)
        {
            PerformDegree2(state);
            MoveXToY(state);
            state.NeedClearX = true;
            
        }

        public void Insert_1x(CalculatorState state)
        {
            Perform_1x(state);
            MoveXToY(state);
            state.NeedClearX = true;

        }

        private void MoveXToY(CalculatorState state)
        {
            state.YRegister = state.XRegister;
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

                case "x^y":

                    log = $"{state.YRegister} ^ {state.XRegister} = ";
                    state.XRegister = Math.Pow(state.YRegister, state.XRegister);
                    state.OperationLog = log + state.XRegister.ToString();
                    break;

                case "*":
                    state.XRegister *= state.YRegister;
                    state.OperationLog = log + state.XRegister.ToString();
                    break;

                case "/":
                    if (state.XRegister == 0)
                    {
                        state.XRegister = double.NaN;
                    }
                    else
                    {
                        state.XRegister = state.YRegister / state.XRegister;
                        state.OperationLog = log + state.XRegister.ToString();
                    }

                    break;
            }
        }

        private void PerformDegree2(CalculatorState state)
        {

            var log = $"{state.XRegister} ^ 2 = ";
            state.XRegister *= state.XRegister;
            state.OperationLog = log + state.XRegister.ToString();

        }

        private void Perform_1x(CalculatorState state)
        {

            var log = $"1 / {state.XRegister} = ";
            state.XRegister = 1 / state.XRegister;
            state.OperationLog = log + state.XRegister.ToString();

        }



    }



}
