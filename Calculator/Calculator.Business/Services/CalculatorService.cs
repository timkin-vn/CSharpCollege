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

        private void CalculateInverse(CalculatorState state)
        {
            if (state.XRegister != 0) // Проверка на деление на ноль
            {
                state.XRegister = 1.0 / state.XRegister;
            }
            else
            {
                throw new DivideByZeroException("Деление на ноль невозможно.");
            }
        }

        

        private void MoveXToY(CalculatorState state)
        {
            state.YRegister = state.XRegister;
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
                case "1/x":
                    if (state.XRegister != 0)
                    {
                        state.XRegister = 1 / state.XRegister;
                    }
                    else
                    {
                        state.XRegister = double.NaN; // Обработка деления на ноль
                    }
                    break;

                case "x^2":
                    state.XRegister = Math.Pow(state.XRegister, 2);
                    break;

                case "√x":
                    if (state.XRegister >= 0)
                    {
                        state.XRegister = Math.Sqrt(state.XRegister);
                    }
                    else
                    {
                        state.XRegister = double.NaN; // Обработка извлечения корня из отрицательного числа
                    }
                    break;
            }
            

        }
    }
}
