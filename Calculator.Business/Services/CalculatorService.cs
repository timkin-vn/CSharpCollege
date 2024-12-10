using Calculator.Business.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        private bool isDecimal = false; // Флаг, указывающий, что введено дробное число
        private int decimalPlaces = 0; // Количество знаков после запятой
        private bool isCommaUsed = false; // Флаг для использования запятой вместо точки

        public void InsertDigit(CalculatorState state, string digitText)
        {
            if (digitText == ".")
            {
                if (!isDecimal) // Если дробная часть еще не введена
                {
                    isDecimal = true;
                    decimalPlaces = 0; // Сброс счетчика знаков после запятой
                    state.XRegister = state.XRegister; // Переход к дробной части
                }
            }
            else if (digitText == ",") // Проверка на запятую
            {
                if (!isCommaUsed) // Если запятая еще не использовалась
                {
                    isCommaUsed = true;
                    if (!isDecimal)
                    {
                        isDecimal = true;
                        decimalPlaces = 0;
                    }
                    // Переводим число из десятичной формы с точкой в десятичную форму с запятой
                    state.XRegister = Convert.ToDouble(state.XRegister.ToString().Replace(".", ","));
                }
            }
            else
            {
                if (isDecimal)
                {
                    decimalPlaces++;
                    state.XRegister = state.XRegister + (double.Parse(digitText) / Math.Pow(10, decimalPlaces));
                }
                else
                {
                    var digit = byte.Parse(digitText);
                    if (state.NeedClearX)
                    {
                        state.XRegister = 0;
                    }
                    state.XRegister = state.XRegister * 10 + digit;
                }
            }

            state.NeedClearX = false;
        }

        public void Clear(CalculatorState state)
        {
            state.XRegister = 0;
            isDecimal = false;
            decimalPlaces = 0;
            isCommaUsed = false; // Сброс флага запятой
        }

        public void InsertOperation(CalculatorState state, string opCode)
        {
            PerformOperation(state, state.OpCode);
            MoveXToY(state);
            state.OpCode = opCode;
            state.NeedClearX = true;
            isDecimal = false; // Сброс флага дробной части
            decimalPlaces = 0; // Сброс счетчика знаков после запятой
            isCommaUsed = false; // Сброс флага запятой
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
                        // Преобразуем число в десятичную форму с точкой для деления
                        state.XRegister = Convert.ToDouble(state.XRegister.ToString().Replace(",", "."));
                        state.YRegister = Convert.ToDouble(state.YRegister.ToString().Replace(",", "."));
                        state.XRegister = state.YRegister / state.XRegister;
                    }
                    break;

                case "Sqrt":
                    if (state.XRegister < 0)
                    {
                        throw new InvalidOperationException("Невозможно вычислить квадратный корень из отрицательного числа.");
                    }
                    state.XRegister = Math.Sqrt(state.XRegister);
                    break;

                case "Deg":
                    state.XRegister = Math.Pow(state.XRegister, state.YRegister);
                    break;

                case "Log":
                    if (state.XRegister <= 0)
                    {
                        throw new InvalidOperationException("Логарифм не определен для нуля и отрицательных чисел.");
                    }
                    state.XRegister = Math.Log(state.XRegister);
                    break;

                case "Log10":
                    if (state.XRegister <= 0)
                    {
                        throw new InvalidOperationException("Логарифм не определен для нуля и отрицательных чисел.");
                    }
                    state.XRegister = Math.Log10(state.XRegister);
                    break;

            

            }
        }
    }
}