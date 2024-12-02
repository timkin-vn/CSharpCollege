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
            if (byte.TryParse(digitText, out var digit))
            {
                if (state.NeedClearX)
                {
                    state.XRegister = 0;
                }

                state.XRegister = state.XRegister * 10 + digit;
                state.NeedClearX = false;
            }

        }

        public void Clear(CalculatorState state)
        {
            state.XRegister = 0;
            state.YRegister = 0;
            state.OpCode = string.Empty;
            state.OperationLog = "";
        }

        public void InsertOperation(CalculatorState state, string opCode)
        {
            if (!string.IsNullOrEmpty(state.OpCode))
            {
                PerformOperation(state, state.OpCode);
            }

            MoveXToY(state);
            state.OpCode = opCode;
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
                    state.OperationLog = log + state.XRegister;
                    break;

                case "-":
                    state.XRegister = state.YRegister - state.XRegister;
                    state.OperationLog = log + state.XRegister;
                    break;

                case "*":
                    state.XRegister *= state.YRegister;
                    state.OperationLog = log + state.XRegister;
                    break;

                case "/":
                    if (state.XRegister == 0)
                    {
                        state.XRegister = double.NaN;
                    }
                    else
                    {
                        state.XRegister = state.YRegister / state.XRegister;
                        state.OperationLog = log + state.XRegister;
                    }
                    break;

                case "√":
                    if (state.YRegister < 0)
                    {
                        state.XRegister = double.NaN;
                        state.OperationLog = "невозможно извлечь корень из отрицательного числа.";
                    }
                    else
                    {
                        
                        state.XRegister = Math.Sqrt(state.YRegister);
                        state.OperationLog = $"√{state.YRegister} = {state.XRegister}";
                    }
                    break;

                case "^":
                    var baseValue = state.YRegister;  // Сохраняем основание
                    var exponent = state.XRegister;   // Сохраняем степень
                    state.XRegister = Math.Pow(baseValue, exponent);  
                    state.OperationLog = $"{baseValue} ^ {exponent} = {state.XRegister}";
                    break;
            }
        }
    }
}
