using Calculator.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        private Stack<Operation> _operationStack = new Stack<Operation>();

        public void PressDigit(CalculatorState state, string digitString)
        {
            if (!int.TryParse(digitString, out var digit))
            {
                return;
            }

            if (state.IsClearNeeded)
            {
                state.RegisterX = 0;
            }

            state.RegisterX = state.RegisterX * 10 + digit;
            state.IsClearNeeded = false;
        }

        public void Clear(CalculatorState state)
        {
            state.RegisterX = 0;
            _operationStack.Clear();
        }

        public void PressOperation(CalculatorState state, string operationCode)
        {
            int priority = GetOperationPriority(operationCode);
            while (_operationStack.Count > 0 &&
                   _operationStack.Peek().Priority >= priority)
            {
                CompleteOperation(state, _operationStack.Pop());
            }

            _operationStack.Push(new Operation
            {
                Sign = operationCode,
                Priority = priority,
                XValue = state.RegisterX
            });

            state.IsClearNeeded = true;
        }

        public void PressEqual(CalculatorState state)
        {
            while (_operationStack.Count > 0)
            {
                CompleteOperation(state, _operationStack.Pop());
            }

            state.IsClearNeeded = true;
        }

        private int GetOperationPriority(string operationCode)
        {
            switch (operationCode)
            {
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                case "=":
                    return 0;
                default:
                    throw new ArgumentException("Неверный код операции");
            }
        }

        private void CompleteOperation(CalculatorState state, Operation operation)
        {
            switch (operation.Sign)
            {
                case "+":
                    state.RegisterX = operation.XValue + state.RegisterX;
                    break;
                case "-":
                    state.RegisterX = operation.XValue - state.RegisterX;
                    break;
                case "*":
                    state.RegisterX = operation.XValue * state.RegisterX;
                    break;
                case "/":
                    state.RegisterX = operation.XValue / state.RegisterX;
                    break;
            }
        }

        private class Operation
        {
            public string Sign { get; set; }
            public int Priority { get; set; }
            public decimal XValue { get; set; }
        }
    }
}