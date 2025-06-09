// File: Calculator.Business.Services/CalculatorService.cs
using Calculator.Business.Models;
using System;
// using Microsoft.SqlServer.Server; // Эту ссылку можно удалить, она не используется в логике калькулятора
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        // Вспомогательный метод для форматирования числа для отображения.
        // Будет использоваться в CalculatorForm.cs для DisplayLabel.
        public string FormatNumberForDisplay(double number)
        {
            if (double.IsNaN(number)) return "Ошибка";
            if (double.IsInfinity(number)) return "Бесконечность";

            // Проверяем, является ли число целым.
            // Используем небольшой допуск для сравнения с целыми числами из-за особенностей double.
            if (Math.Abs(number - Math.Round(number)) < 1e-9 && Math.Abs(number) < 1e15) // Если число практически целое и не слишком большое
            {
                return number.ToString("0"); // Форматируем как целое (без .0)
            }
            return number.ToString(); // Иначе стандартное форматирование для double
        }

        public void InsertDigit(CalculatorState state, string digitString)
        {
            // Важно: текущая логика InsertDigit не поддерживает десятичные числа.
            // Если вам нужна десятичная точка, эта логика должна быть изменена.
            var digit = byte.Parse(digitString);
            if (state.NeedClear)
            {
                state.XRegister = 0;
            }

            state.XRegister = state.XRegister * 10 + digit;
            state.NeedClear = false;
        }

        public void Clear(CalculatorState state)
        {
            state.XRegister = 0;
            state.YRegister = 0;
            state.OpCodeRegister = null;
            state.NeedClear = true;
        }

        public void PressOperation(CalculatorState state, string opCode)
        {
            // Перед выполнением новой операции, если есть старая и мы не начинаем новое число,
            // выполняем предыдущую операцию.
            if (!string.IsNullOrEmpty(state.OpCodeRegister) && !state.NeedClear)
            {
                MakeOperation(state, state.OpCodeRegister);
            }

            // Если нажата кнопка '=', то сбрасываем OpCodeRegister.
            // Иначе сохраняем новую операцию.
            if (opCode == "=")
            {
                state.OpCodeRegister = null;
            }
            else
            {
                state.OpCodeRegister = opCode;
            }

            MoveXToY(state); // Перемещаем текущий результат в YRegister для следующей операции
            state.NeedClear = true; // Следующий ввод цифры начнет новое число
        }

        private void MoveXToY(CalculatorState state)
        {
            state.YRegister = state.XRegister;
        }

        public void DegretOperation(CalculatorState state) // Для x²
        {
            state.XRegister = state.XRegister * state.XRegister;
            state.NeedClear = true;
        }

        public void RowOperation(CalculatorState state) // Для √x
        {
            if (state.XRegister < 0)
            {
                state.XRegister = double.NaN; // Корень из отрицательного числа
            }
            else
            {
                const double precision = 0.000001;
                double row_X = state.XRegister / 2;
                if (row_X == 0 && state.XRegister != 0) row_X = 1; // Избегаем деления на ноль для итерации, если XRegister не 0

                while (Math.Abs(row_X * row_X - state.XRegister) > precision)
                {
                    row_X = (row_X + state.XRegister / row_X) / 2;
                }
                state.XRegister = row_X;
            }
            state.NeedClear = true;
        }

        public void ABSOperation(CalculatorState state) // Для |x|
        {
            state.XRegister = Math.Abs(state.XRegister);
            state.NeedClear = true;
        }

        public void Negativ_and_Positiv_Operation(CalculatorState state) // Для +/-
        {
            state.XRegister = -state.XRegister;
            state.NeedClear = true; // Обычно для +/- можно продолжать ввод, но следуем вашей логике.
        }

        // --- НОВЫЙ МЕТОД: Возведение в куб (X^3) ---
        public void CubeOperation(CalculatorState state)
        {
            state.XRegister = state.XRegister * state.XRegister * state.XRegister;
            // Альтернативно: state.XRegister = Math.Pow(state.XRegister, 3);
            state.NeedClear = true;
        }

        // --- НОВЫЙ МЕТОД: Процент (%) ---
        public void PercentOperation(CalculatorState state)
        {
            // Логика процента может быть разной, это одна из распространенных реализаций.
            // Например, 100 + 5% = 105, 100 * 5% = 5
            if (!string.IsNullOrEmpty(state.OpCodeRegister))
            {
                if (state.OpCodeRegister == "+" || state.OpCodeRegister == "-")
                {
                    // Для сложения/вычитания: X% от Y. Пример: 100 + 5% -> 5% от 100 = 5.
                    state.XRegister = state.YRegister * (state.XRegister / 100.0);
                }
                else // Для умножения/деления просто делим X на 100
                {
                    state.XRegister /= 100.0;
                }
            }
            else
            {
                // Если нет ожидающей операции, X становится X / 100
                state.XRegister /= 100.0;
            }
            state.NeedClear = true;
        }
        // --- КОНЕЦ НОВЫХ МЕТОДОВ ---

        private void MakeOperation(CalculatorState state, string opCode)
        {
            switch (opCode)
            {
                case "+":
                    state.XRegister = state.YRegister + state.XRegister;
                    break;
                case "-":
                    state.XRegister = state.YRegister - state.XRegister;
                    break;
                case "*":
                    state.XRegister = state.YRegister * state.XRegister;
                    break;
                case "/":
                    if (state.XRegister == 0)
                    {
                        state.XRegister = double.NaN; // Деление на ноль
                        break;
                    }
                    state.XRegister = state.YRegister / state.XRegister;
                    break;
                    // case "X^Y": // Этот кейс закомментирован, так как это бинарная операция X^Y
                    // и требует другого подхода для обработки (например, отдельной кнопки X^Y).
            }
        }
    }
}