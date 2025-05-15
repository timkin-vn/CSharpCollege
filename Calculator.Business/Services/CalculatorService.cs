using Calculator.Business.Models;
using System;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        // Основные операции
        public void PressDigit(CalculatorState state, string digitString)
        {
            if (state.HasError) Clear(state);

            if (!int.TryParse(digitString, out var digit))
            {
                state.HasError = true;
                state.LastError = "Invalid digit";
                return;
            }

            if (state.IsClearNeeded || state.RegisterX == 0)
            {
                state.RegisterX = digit;
                state.IsClearNeeded = false;
            }
            else
            {
                state.RegisterX = state.RegisterX * 10 + digit;
            }
        }

        public void Clear(CalculatorState state)
        {
            state.RegisterX = 0;
            state.RegisterY = 0;
            state.OperationCode = "";
            state.IsClearNeeded = false;
            state.HasError = false;
            state.LastError = "";
        }

        public void MoveXToY(CalculatorState state)
        {
            state.RegisterY = state.RegisterX;
        }

        // Арифметические операции
        public void CompleteOperation(CalculatorState state)
        {
            try
            {
                switch (state.OperationCode)
                {
                    case "+":
                        state.RegisterX = state.RegisterY + state.RegisterX;
                        break;
                    case "-":
                        state.RegisterX = state.RegisterY - state.RegisterX;
                        break;
                    case "*":
                        state.RegisterX = state.RegisterY * state.RegisterX;
                        break;
                    case "/":
                        if (state.RegisterX == 0) throw new DivideByZeroException();
                        state.RegisterX = state.RegisterY / state.RegisterX;
                        break;
                    case "^":
                        state.RegisterX = Math.Pow(state.RegisterY, state.RegisterX);
                        break;
                    case "mod":
                        state.RegisterX = state.RegisterY % state.RegisterX;
                        break;
                }
            }
            catch (Exception ex)
            {
                state.HasError = true;
                state.LastError = ex.Message;
            }
        }

        public void PressOperation(CalculatorState state, string operationCode)
        {
            if (!state.IsNewOperation)
            {
                CompleteOperation(state);
            }
            MoveXToY(state);
            state.OperationCode = operationCode;
            state.IsClearNeeded = true;
            state.IsNewOperation = false;
        }

        public void PressEqual(CalculatorState state)
        {
            CompleteOperation(state);
            state.IsClearNeeded = true;
            state.IsNewOperation = true;
        }

        // Научные функции
        public void Sin(CalculatorState state)
        {
            state.RegisterX = state.IsRadiansMode
                ? Math.Sin(state.RegisterX)
                : Math.Sin(state.RegisterX * Math.PI / 180);
            state.IsClearNeeded = true;
        }

        public void Cos(CalculatorState state)
        {
            state.RegisterX = state.IsRadiansMode
                ? Math.Cos(state.RegisterX)
                : Math.Cos(state.RegisterX * Math.PI / 180);
            state.IsClearNeeded = true;
        }

        public void Tan(CalculatorState state)
        {
            double x = state.IsRadiansMode ? state.RegisterX : state.RegisterX * Math.PI / 180;
            if (Math.Abs(Math.Cos(x)) < 1e-10) throw new ArgumentException("Invalid tangent value");
            state.RegisterX = Math.Tan(x);
            state.IsClearNeeded = true;
        }

        public void SquareRoot(CalculatorState state)
        {
            if (state.RegisterX < 0) throw new ArgumentException("Square root of negative number");
            state.RegisterX = Math.Sqrt(state.RegisterX);
            state.IsClearNeeded = true;
        }

        public void PowerOfTwo(CalculatorState state)
        {
            state.RegisterX = Math.Pow(state.RegisterX, 2);
            state.IsClearNeeded = true;
        }

        public void Factorial(CalculatorState state)
        {
            if (state.RegisterX < 0 || state.RegisterX % 1 != 0)
                throw new ArgumentException("Factorial requires non-negative integer");

            int n = (int)state.RegisterX;
            int result = 1;
            for (int i = 2; i <= n; i++) result *= i;

            state.RegisterX = result;
            state.IsClearNeeded = true;
        }

        // Константы
        public void SetPi(CalculatorState state)
        {
            state.RegisterX = Math.PI;
            state.IsClearNeeded = true;
        }

        public void SetE(CalculatorState state)
        {
            state.RegisterX = Math.E;
            state.IsClearNeeded = true;
        }

        // Память
        public void MemoryAdd(CalculatorState state)
        {
            state.Memory += state.RegisterX;
        }

        public void MemorySubtract(CalculatorState state)
        {
            state.Memory -= state.RegisterX;
        }

        public void MemoryRecall(CalculatorState state)
        {
            state.RegisterX = state.Memory;
            state.IsClearNeeded = true;
        }

        public void MemoryClear(CalculatorState state)
        {
            state.Memory = 0;
        }

        // Дополнительные функции
        public void ChangeSign(CalculatorState state)
        {
            state.RegisterX = -state.RegisterX;
        }

        public void Percent(CalculatorState state)
        {
            state.RegisterX = state.RegisterX / 100;
        }
    }
}