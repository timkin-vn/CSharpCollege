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

        private double CalculateFactorial(int n)
        {
            if (n <= 1) return 1;
            double result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
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
                case "√":
                    if (state.XRegister < 0)
                    {
                        state.XRegister = double.NaN;
                    }
                    else
                    {
                        state.XRegister = Math.Sqrt(state.XRegister);
                        state.OperationLog = $"√{state.YRegister} = {state.XRegister}";
                    }
                    break;
                case "n!":
                    if (state.XRegister < 0 || state.XRegister != Math.Floor(state.XRegister))
                    {
                        state.XRegister = double.NaN;
                    }
                    else
                    {
                        state.XRegister = CalculateFactorial((int)state.XRegister);
                        state.OperationLog = $"{state.YRegister}! = {state.XRegister}";
                    }
                    break;

                case "y^x":
                    try
                    {
                        double x = (double) state.XRegister;
                        double y = state.YRegister;
                        state.XRegister = Math.Pow(y, x);
                        state.OperationLog = $"{y}^{x} = {state.XRegister}";
                    }
                    catch (OverflowException)
                    {
                        state.XRegister = double.NaN;
                        state.OperationLog = "Ошибка: результат слишком велик";
                    }
                    catch (ArgumentException)
                    {
                        state.XRegister = double.NaN;
                        state.OperationLog = "Ошибка: недопустимые аргументы";
                    }
                    break;

            }
        }
    }
}
