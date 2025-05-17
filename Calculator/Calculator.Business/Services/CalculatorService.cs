using Calculator.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        private Stack<Operation> _operationStack = new Stack<Operation>();
        private int _openBracketsCount = 0;
        private int _closeBracketsCount = 0;

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


        public void PressOperation(CalculatorState state, string operationCode)
        {
            int priority = GetOperationPriority(operationCode);
            if (operationCode == "(")
            {
                _openBracketsCount++;
                _operationStack.Push(new Operation
                {
                    Sign = operationCode,
                    Priority = priority,
                    XValue = state.RegisterX
                });
                return;
            }

            if (operationCode == ")")
            {
                _closeBracketsCount++;
                if (_closeBracketsCount > _openBracketsCount)
                {
                    throw new ArgumentException("Введены лишние скобки");
                }

                while (_operationStack.Count > 0 &&
                       _operationStack.Peek().Sign != "(")
                {
                    CompleteOperation(state, _operationStack.Pop());
                }

                if (_operationStack.Count > 0 &&
                    _operationStack.Peek().Sign == "(")
                {
                    _operationStack.Pop();
                }
                return;
            }
            bool hasOpenBracket = false;
            var tempStack = new Stack<Operation>(_operationStack);
            while (tempStack.Count > 0)
            {
                if (tempStack.Peek().Sign == "(")
                {
                    hasOpenBracket = true;
                    break;
                }
                tempStack.Pop();
            }
            if (hasOpenBracket)
            {
                _operationStack.Push(new Operation
                {
                    Sign = operationCode,
                    Priority = priority,
                    XValue = state.RegisterX
                });
                state.IsClearNeeded = true;
                return;
            }
            while (_operationStack.Count > 0 &&
                   _operationStack.Peek().Priority > priority)
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

        public void Clear(CalculatorState state)
        {
            state.RegisterX = 0;
            _operationStack.Clear();
            _openBracketsCount = 0;
            _closeBracketsCount = 0;
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
                case "(":
                case ")":
                    return 3;
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