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
                state.IsDecimalMode = false;
                state.DecimalFactor = 1;
                state.DisplayedOperation = string.Empty;
            }

            if (state.IsDecimalMode)
            {
                state.DecimalFactor /= 10;
                state.XRegister += digit * state.DecimalFactor;
            }
            else
            {
                if (state.XRegister == 0 && digit == 0)
                {
                    return;
                }

                state.XRegister = state.XRegister * 10 + digit;
            }

            state.NeedClearX = false;
        }

        public void InsertDecimalPoint(CalculatorState state)
        {
            if (!state.IsDecimalMode)
            {
                state.IsDecimalMode = true;
                state.DecimalFactor = 1;
            }
        }
        public void InsertOperation(CalculatorState state, string opCode)
        {
            // Проверяем, является ли операция специальной и выполняем её сразу
            if (opCode == "1/x" || opCode == "x²" || opCode == "√" || opCode == "±" || opCode == "%")
            {
                PerformOperation(state, opCode);
                state.DisplayedOperation = $"{opCode}({state.XRegister})"; // Отображаем операцию
                state.OpCode = string.Empty; // Сбрасываем оператор
                state.NeedClearX = true; // Подготавливаем для нового ввода после специальной операции
                return;
            }

            // Стандартная обработка для базовых операций
            if (!string.IsNullOrEmpty(state.OpCode))
            {
                PerformOperation(state, state.OpCode);
            }
            else
            {
                state.YRegister = state.XRegister;
                state.Result = state.XRegister;
            }

            if (opCode == "=")
            {
                state.OpCode = string.Empty;
                state.DisplayedOperation = string.Empty;
                state.XRegister = state.Result;
            }
            else
            {
                state.OpCode = opCode;
                state.NeedClearX = true;
                state.DisplayedOperation = $"{state.Result} {opCode}";
            }
        }

        public void Clear(CalculatorState state)
        {
            state.XRegister = 0;
            state.YRegister = 0;
            state.Result = 0;
            state.OpCode = string.Empty;
            state.NeedClearX = false;
            state.IsDecimalMode = false;
            state.DecimalFactor = 1;
            state.DisplayedOperation = string.Empty;
        }

        public void ClearEntry(CalculatorState state)
        {
            state.XRegister = 0;
            state.NeedClearX = true;
        }
        public void Backspace(CalculatorState state)
        {
            if (state.NeedClearX || state.XRegister == 0)
            {
                return;
            }

            if (state.IsDecimalMode)
            {
                state.XRegister = Math.Floor(state.XRegister * 10) / 10;
                state.DecimalFactor *= 10;

                if (state.DecimalFactor >= 1)
                {
                    state.IsDecimalMode = false;
                }
            }
            else
            {
                state.XRegister = Math.Floor(state.XRegister / 10);
            }
        }
        private void PerformOperation(CalculatorState state, string opCode)
        {
            switch (opCode)
            {
                case "+":
                    state.Result = state.YRegister + state.XRegister;
                    break;

                case "-":
                    state.Result = state.YRegister - state.XRegister;
                    break;

                case "*":
                    state.Result = state.YRegister * state.XRegister;
                    break;

                case "/":
                    state.Result = state.XRegister != 0 ? state.YRegister / state.XRegister : double.NaN;
                    break;

                case "%":
                    if (state.OpCode == "+" || state.OpCode == "-" || state.OpCode == "*" || state.OpCode == "/")
                    {
                        state.XRegister = state.YRegister * (state.XRegister / 100.0);
                        PerformOperation(state, state.OpCode);
                    }
                    else
                    {
                        state.Result = state.XRegister / 100.0;
                    }
                    break;


                case "1/x":
                    state.Result = state.XRegister != 0 ? 1 / state.XRegister : double.NaN;
                    break;

                case "x²":
                    state.Result = Math.Pow(state.XRegister, 2);
                    break;

                case "√":
                    state.Result = state.XRegister >= 0 ? Math.Sqrt(state.XRegister) : double.NaN;
                    break;

                case "±":
                    state.Result = -state.XRegister;
                    break;
            }

            state.YRegister = state.Result;
            state.XRegister = state.Result;
        }
    }
}
