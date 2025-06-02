using Calculator.Business.Models;
using System;

namespace Calculator.Business.Services {
    public class CalculatorService {
        public void PressDigit(CalculatorState state, string digitText) {
            if (state.NeedClearX) {
                state.CurrentInput = digitText;
                state.NeedClearX = false;
            }
            else {
                state.CurrentInput += digitText;
            }
            UpdateExpression(state);
        }

        public void Clear(CalculatorState state) {
            state.RegisterX = 0;
            state.RegisterY = 0;
            state.Operation = string.Empty;
            state.CurrentInput = "";
            state.NeedClearX = false;
            state.Memory = 0;
            state.Expression = "";
            state.SelectedFunction = "sin";
        }


        public void CompleteOperation(CalculatorState state, string operation) {
            switch (operation) {
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
                    if (state.RegisterX != 0)
                        state.RegisterX = state.RegisterY / state.RegisterX;
                    else
                        state.RegisterX = 0;
                    break;
            }
        }

        public void PressOperation(CalculatorState state, string operation) {
            if (!string.IsNullOrEmpty(state.CurrentInput)) {
                state.RegisterX = double.Parse(state.CurrentInput);
                state.CurrentInput = "";
            }
            CompleteOperation(state, state.Operation);
            state.RegisterY = state.RegisterX;
            state.Operation = operation;
            state.NeedClearX = true;
            UpdateExpression(state);
        }

        public void PressEqual(CalculatorState state) {
            if (!string.IsNullOrEmpty(state.CurrentInput)) {
                state.RegisterX = double.Parse(state.CurrentInput);
                state.CurrentInput = "";
            }
            double previousX = state.RegisterX;
            CompleteOperation(state, state.Operation);
            state.Expression = $"{state.RegisterY} {state.Operation} {previousX} = {state.RegisterX}";
            state.NeedClearX = true;
        }

        public void ComputeTrigFunction(CalculatorState state) {
            if (!string.IsNullOrEmpty(state.CurrentInput)) {
                state.RegisterX = double.Parse(state.CurrentInput);
                state.CurrentInput = "";
            }
            double input = state.RegisterX;
            double result = 0;
            bool isValid = true;

            switch (state.SelectedFunction) {
                case "sin":
                    result = Math.Sin(input * Math.PI / 180);
                    break;
                case "cos":
                    result = Math.Cos(input * Math.PI / 180);
                    break;
                case "tan":
                    if (Math.Abs(input % 180) == 90) {
                        isValid = false;
                        result = 0;
                        state.Expression = $"Error: tan({input}°) не определен";
                    }
                    else {
                        result = Math.Tan(input * Math.PI / 180);
                    }
                    break;
                case "cot":
                    if (Math.Abs(input % 180) == 0) {
                        isValid = false;
                        result = 0;
                        state.Expression = $"Error: cot({input}°) undefined";
                    }
                    else {
                        result = 1 / Math.Tan(input * Math.PI / 180);
                    }
                    break;
                case "arcsin":
                    if (input < -1 || input > 1) {
                        isValid = false;
                        result = 0;
                        state.Expression = $"Ошибка: arcsin({input}) вне диапазона [-1, 1]";
                    }
                    else {
                        result = Math.Asin(input) * 180 / Math.PI;
                    }
                    break;
                case "arccos":
                    if (input < -1 || input > 1) {
                        isValid = false;
                        result = 0;
                        state.Expression = $"Ошибка: arccos({input}) Вне диапазона [-1, 1]";
                    }
                    else {
                        result = Math.Acos(input) * 180 / Math.PI;
                    }
                    break;
                case "arctan":
                    result = Math.Atan(input) * 180 / Math.PI;
                    break;
                case "arccot":
                    result = (Math.PI / 2 - Math.Atan(input)) * 180 / Math.PI;
                    break;
            }

            if (isValid) {
                state.RegisterX = result;
                state.Expression = $"{state.SelectedFunction}({input}°) = {result:F6}".TrimEnd('0').TrimEnd('.');
            }
            else {
                state.RegisterX = 0;
            }
            state.NeedClearX = true;
        }

        public void SquareRoot(CalculatorState state) {
            if (!string.IsNullOrEmpty(state.CurrentInput)) {
                state.RegisterX = double.Parse(state.CurrentInput);
                state.CurrentInput = "";
            }
            if (state.RegisterX >= 0) {
                double input = state.RegisterX;
                state.RegisterX = Math.Sqrt(state.RegisterX);
                state.Expression = $"√({input}) = {state.RegisterX}";
            }
            else {
                state.RegisterX = 0;
                state.Expression = "Error: Negative Square Root";
            }
            state.NeedClearX = true;
        }

        public void Square(CalculatorState state) {
            if (!string.IsNullOrEmpty(state.CurrentInput)) {
                state.RegisterX = double.Parse(state.CurrentInput);
                state.CurrentInput = "";
            }
            double input = state.RegisterX;
            state.RegisterX = Math.Pow(state.RegisterX, 2);
            state.Expression = $"({input})² = {state.RegisterX}";
            state.NeedClearX = true;
        }

        public void Percent(CalculatorState state) {
            if (!string.IsNullOrEmpty(state.CurrentInput)) {
                state.RegisterX = double.Parse(state.CurrentInput);
                state.CurrentInput = "";
            }
            double input = state.RegisterX;
            state.RegisterX = state.RegisterY * state.RegisterX / 100;
            state.Expression = $"{state.RegisterY} * {input}% = {state.RegisterX}";
            state.NeedClearX = true;
        }

        public void ChangeSign(CalculatorState state) {
            if (!string.IsNullOrEmpty(state.CurrentInput)) {
                state.RegisterX = double.Parse(state.CurrentInput);
                state.CurrentInput = "";
            }
            double input = state.RegisterX;
            state.RegisterX = -state.RegisterX;
            state.Expression = $"-({input}) = {state.RegisterX}";
            state.NeedClearX = true;
        }

        public void AddDecimal(CalculatorState state) {
            if (state.NeedClearX) {
                state.CurrentInput = "0.";
                state.NeedClearX = false;
            }
            else if (!state.CurrentInput.Contains(".")) {
                state.CurrentInput += ".";
            }
            UpdateExpression(state);
        }

        public void MemoryStore(CalculatorState state) {
            if (!string.IsNullOrEmpty(state.CurrentInput)) {
                state.RegisterX = double.Parse(state.CurrentInput);
            }
            state.Memory = state.RegisterX;
            state.Expression = $"MS({state.RegisterX})";
            state.NeedClearX = true;
        }

        public void MemoryRecall(CalculatorState state) {
            state.RegisterX = state.Memory;
            state.CurrentInput = state.Memory.ToString();
            state.Expression = $"MR = {state.Memory}";
            state.NeedClearX = true;
        }

        private void UpdateExpression(CalculatorState state) {
            string yPart = (!string.IsNullOrEmpty(state.Operation) && state.RegisterY != 0) ? state.RegisterY.ToString() : "";
            string opPart = state.Operation;
            string inputPart = state.CurrentInput;

            if (string.IsNullOrEmpty(yPart) && string.IsNullOrEmpty(opPart) && string.IsNullOrEmpty(inputPart)) {
                state.Expression = "";
            }
            else {
                state.Expression = $"{yPart} {opPart} {inputPart}".Trim();
            }
        }
    }
}