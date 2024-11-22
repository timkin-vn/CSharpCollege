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
            if (!byte.TryParse(digitText, out var digit))
                throw new ArgumentException("Invalid digit input.");

            if (state.NeedClearX)
            {
                state.XRegister = 0;
                state.NeedClearX = false;
            }

            state.XRegister = state.XRegister * 10 + digit; 
        }

        public void Clear(CalculatorState state)
        {
            state.XRegister = 0;
            state.YRegister = 0;
            state.OpCode = null;
            state.NeedClearX = false;
            state.OperationLog = null;
        }
        public void InsertOperation(CalculatorState state, string opCode)
        {
            switch (opCode)
            {
                case "×":
                    opCode = "*";
                    break;
                case "÷":
                    opCode = "/";
                    break;
            }

            if (opCode == "=")
            {
                PerformOperation(state, state.OpCode);
                state.OpCode = null;
            }
            else
            {
                if (!string.IsNullOrEmpty(state.OpCode))
                {
                    PerformOperation(state, state.OpCode);
                }

                state.OpCode = opCode;
                MoveXToY(state);
                state.NeedClearX = true;
            }
        }
        private void MoveXToY(CalculatorState state)
        {
            state.YRegister = state.XRegister;
        }
        private void PerformOperation(CalculatorState state, string opCode)
        {
            var x = state.XRegister;
            var y = state.YRegister;
            double result;

            switch (opCode)
            {
                case "+":
                    result = y + x;
                    break;
                case "-":
                    result = y - x;
                    break;
                case "*":
                    result = y * x;
                    break;
                case "/":
                    if (x == 0)
                        throw new InvalidOperationException("Division by zero is not allowed.");
                    result = y / x;
                    break;
                default:
                    throw new InvalidOperationException($"Unknown operation: {opCode}");
            }

            state.XRegister = result;
            state.OperationLog = $"{y} {opCode} {x} = {result}";
        }

        public void CalculatePercentage(CalculatorState state)
        {
            state.XRegister = state.XRegister / 100.0;
            state.OperationLog = $"{state.XRegister * 100}% = {state.XRegister}";
        }
        public void CalculateSquareRoot(CalculatorState state)
        {
            if (state.XRegister < 0)
                throw new InvalidOperationException("Cannot calculate the square root of a negative number.");

            state.XRegister = Math.Sqrt(state.XRegister);
            state.OperationLog = $"√{state.XRegister} = {state.XRegister}";
        }

        public void CalculatePower(CalculatorState state)
        {
            state.XRegister = Math.Pow(state.YRegister, state.XRegister);
            state.OperationLog = $"{state.YRegister}^{state.XRegister} = {state.XRegister}";
        }
        public void CalculateSin(CalculatorState state)
        {
            state.XRegister = Math.Sin(state.XRegister);
            state.OperationLog = $"sin({state.XRegister}) = {state.XRegister}";
        }
        public void CalculateCos(CalculatorState state)
        {
            state.XRegister = Math.Cos(state.XRegister);
            state.OperationLog = $"cos({state.XRegister}) = {state.XRegister}";
        }

        public void CalculateTan(CalculatorState state)
        {
            state.XRegister = Math.Tan(state.XRegister);
            state.OperationLog = $"tan({state.XRegister}) = {state.XRegister}";
        }

        public void CalculateLog(CalculatorState state)
        {
            state.XRegister = Math.Log10(state.XRegister);
            state.OperationLog = $"log({state.XRegister}) = {state.XRegister}";
        }

        public void CalculateLn(CalculatorState state)
        {
            state.XRegister = Math.Log(state.XRegister);
            state.OperationLog = $"ln({state.XRegister}) = {state.XRegister}";
        }
    }
}
