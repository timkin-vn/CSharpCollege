// File: Calculator.Business.Services/CalculatorService.cs
using Calculator.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        // Вспомогательный метод для форматирования числа в строку без лишних ".0"
        // Этот метод используется для формирования строк для отображения и лога.
        private string FormatNumberString(double number)
        {
            if (double.IsNaN(number)) return "Ошибка"; // Обработка NaN
            if (double.IsInfinity(number)) return "Бесконечность"; // Обработка Infinity

            // Проверяем, является ли число целым.
            // Используем небольшой допуск для сравнения с целыми числами из-за особенностей double.
            if (Math.Abs(number - Math.Round(number)) < 1e-9 && Math.Abs(number) < 1e15) // Если число практически целое и не слишком большое
            {
                return number.ToString("0"); // Форматируем как целое
            }
            return number.ToString(); // Иначе стандартное форматирование для double
        }

        // Вставка цифры в текущее число
        public void InsertDigit(CalculatorState state, string digitString)
        {
            if (state.IsNewNumber)
            {
                state.CurrentInput = "0"; // Сбрасываем ввод, если начинаем новое число
                state.HasDecimalPoint = false; // Сбрасываем флаг десятичной точки
                state.IsNewNumber = false;
            }

            // Замена "0" на первую введенную цифру (если это не "0")
            if (state.CurrentInput == "0" && digitString != "0")
            {
                state.CurrentInput = digitString;
            }
            // Замена "-0" на "-цифру"
            else if (state.CurrentInput == "-0" && digitString != "0")
            {
                state.CurrentInput = "-" + digitString;
            }
            // Добавление "0" к "0" или "-0"
            else if ((state.CurrentInput == "0" || state.CurrentInput == "-0") && digitString == "0")
            {
                // Не добавляем лишние нули в начале, кроме случая с десятичной точкой (0.0)
                if (state.HasDecimalPoint)
                {
                    state.CurrentInput += digitString;
                }
                else
                {
                    return; // Игнорируем ввод "0" если текущий ввод "0"
                }
            }
            else if (state.CurrentInput.Length < 16) // Ограничение длины ввода для удобства
            {
                state.CurrentInput += digitString; // Добавляем цифру
            }

            // Обновляем XRegister, парся текущую строку ввода
            if (double.TryParse(state.CurrentInput, out double parsedValue))
            {
                state.XRegister = parsedValue;
            }
            else
            {
                // Это не должно произойти при корректном управлении CurrentInput
                state.XRegister = double.NaN;
            }
        }

        // Полная очистка калькулятора (AC)
        public void ClearAll(CalculatorState state)
        {
            state.XRegister = 0;
            state.YRegister = 0;
            state.OpCodeRegister = null;
            state.IsNewNumber = true;
            state.HasDecimalPoint = false;
            state.CurrentInput = "0";
            state.OperationLog = null; // Очищаем лог текущей операции
        }

        // Обработка нажатия на операцию (+, -, *, /, =, logXY)
        public void PressOperation(CalculatorState state, string newOpCode)
        {
            // Если есть ожидающая операция и мы только что закончили ввод числа (не начинаем новое)
            // ИЛИ если это не знак равенства, но есть ожидающая операция
            if (!string.IsNullOrEmpty(state.OpCodeRegister) && !state.IsNewNumber || (newOpCode != "=" && !string.IsNullOrEmpty(state.OpCodeRegister)))
            {
                MakeOperation(state); // Выполняем ожидающую операцию
            }
            // Если нажато '=' и нет ожидающей операции (например, "5 ="), просто отображаем число
            else if (newOpCode == "=" && string.IsNullOrEmpty(state.OpCodeRegister))
            {
                state.OperationLog = $"{FormatNumberString(state.XRegister)} = {FormatNumberString(state.XRegister)}";
            }
            // Если нет ожидающей операции, текущее число (XRegister) становится YRegister для следующей операции
            else if (string.IsNullOrEmpty(state.OpCodeRegister))
            {
                state.YRegister = state.XRegister;
            }
            // Если уже есть ожидающая операция и мы ввели новое число, это число становится XRegister для следующей операции
            else if (!state.IsNewNumber)
            {
                // Это уже было обработано выше в MakeOperation, так что это условие может быть избыточным,
                // но его наличие не повредит, если логика будет изменена.
            }

            state.IsNewNumber = true; // Следующий ввод цифры начнет новое число
            state.HasDecimalPoint = false; // Сбрасываем флаг десятичной точки для нового числа

            // Если новая операция - это '=', сбрасываем OpCodeRegister
            if (newOpCode == "=")
            {
                state.OpCodeRegister = null;
            }
            else
            {
                state.OpCodeRegister = newOpCode;
            }

            // Обновляем CurrentInput на основе XRegister (результата или YRegister)
            state.CurrentInput = FormatNumberString(state.XRegister);
        }

        // Выполнение арифметической операции
        private void MakeOperation(CalculatorState state)
        {
            double result = 0;
            double x = state.XRegister; // Второе число (текущий ввод)
            double y = state.YRegister; // Первое число (из предыдущего ввода)
            string operationText = $"{FormatNumberString(y)} {state.OpCodeRegister} {FormatNumberString(x)} = ";

            switch (state.OpCodeRegister)
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
                    {
                        result = double.NaN; // Деление на ноль
                    }
                    else
                    {
                        result = y / x;
                    }
                    break;
                case "logXY": // Логарифм по основанию Y от X (log_Y(X))
                    // Проверка условий для логарифма: основание (Y) > 0 и != 1, число (X) > 0
                    if (x <= 0 || y <= 0 || y == 1)
                    {
                        result = double.NaN; // Некорректные аргументы
                    }
                    else
                    {
                        result = Math.Log(x, y); // log_y(x)
                    }
                    break;
                default:
                    result = x; // Если нет операции, просто оставляем X (или XRegister)
                    break;
            }

            state.XRegister = result; // Результат в XRegister
            state.OperationLog = operationText + FormatNumberString(result); // Формируем запись для лога
            state.YRegister = result; // Результат текущей операции становится первым числом для следующей
        }


        // Изменение знака числа (+/-)
        public void NegateNumber(CalculatorState state)
        {
            // Здесь мы не формируем лог в OperationLog, т.к. это модификация текущего числа, а не полная операция
            state.XRegister = -state.XRegister;
            state.CurrentInput = FormatNumberString(state.XRegister); // Обновляем отображение
            // state.IsNewNumber = true; // Можно установить, чтобы следующий ввод начал новое число, но обычно для +/- продолжают редактировать
        }

        // Вставка десятичной точки (.)
        public void InsertDecimal(CalculatorState state)
        {
            if (state.HasDecimalPoint)
            {
                return; // Десятичная точка уже есть, игнорируем
            }

            if (state.IsNewNumber)
            {
                state.CurrentInput = "0"; // Начинаем с "0." если это новое число
                state.IsNewNumber = false;
            }

            state.CurrentInput += "."; // Добавляем десятичную точку
            state.HasDecimalPoint = true; // Устанавливаем флаг

            // XRegister обновляем, но для отображения используем CurrentInput
            if (double.TryParse(state.CurrentInput, out double parsedValue))
            {
                state.XRegister = parsedValue;
            }
        }

        // Применение процента (%)
        public void ApplyPercent(CalculatorState state)
        {
            double originalX = state.XRegister;

            if (!string.IsNullOrEmpty(state.OpCodeRegister) && state.OpCodeRegister != "=")
            {
                // Если есть ожидающая операция, то процент применяется к XRegister,
                // вычисляя его как процент от YRegister (классический калькулятор)
              

                // Для операций сложения/вычитания процент применяется к YRegister
                if (state.OpCodeRegister == "+" || state.OpCodeRegister == "-")
                {
                    state.XRegister = state.YRegister * (state.XRegister / 100.0);
                    // После вычисления процента, можно сразу применить операцию
                    // (например, 100 + 5% станет 100 + 5)
                    // Это более сложно, классический калькулятор сначала вычисляет процент, а потом ждет '='
                    // Сейчас сделаем так: 100 + 5% => XRegister становится 5 (если YRegister 100), ждет '='
                }
                else // Для умножения/деления просто делим на 100
                {
                    state.XRegister /= 100.0;
                }
            }
            else
            {
                // Если нет ожидающей операции, X становится X / 100
                state.XRegister /= 100.0;
            }

            state.CurrentInput = FormatNumberString(state.XRegister); // Обновляем отображение
            state.OperationLog = $"{FormatNumberString(originalX)}% = {state.CurrentInput}"; // Запись в лог
            state.IsNewNumber = true; // Результат готов, следующий ввод начнет новое число
            state.HasDecimalPoint = false; // Сбрасываем флаг десятичной точки
        }

        // --- Научные функции (сохранены и адаптированы) ---

        // X^2 (Возведение в квадрат)
        public void DegretOperation(CalculatorState state, string opCode)
        {
            double originalX = state.XRegister;
            state.XRegister = originalX * originalX;
            state.OperationLog = $"{opCode}({FormatNumberString(originalX)}) = {FormatNumberString(state.XRegister)}";
            state.IsNewNumber = true;
            state.HasDecimalPoint = false;
            state.CurrentInput = FormatNumberString(state.XRegister);
            state.YRegister = state.XRegister; // Результат становится Y для следующей операции
        }

        // Квадратный корень (√x)
        public void RowOperation(CalculatorState state, string opCode)
        {
            double originalX = state.XRegister;
            if (originalX < 0)
            {
                state.XRegister = double.NaN; // Корень из отрицательного числа
            }
            else
            {
                state.XRegister = Math.Sqrt(originalX);
            }
            state.OperationLog = $"{opCode}({FormatNumberString(originalX)}) = {FormatNumberString(state.XRegister)}";
            state.IsNewNumber = true;
            state.HasDecimalPoint = false;
            state.CurrentInput = FormatNumberString(state.XRegister);
            state.YRegister = state.XRegister; // Результат становится Y для следующей операции
        }

        // Модуль (|x|)
        public void ABSOperation(CalculatorState state, string opCode)
        {
            double originalX = state.XRegister;
            state.XRegister = Math.Abs(originalX);
            state.OperationLog = $"{opCode}({FormatNumberString(originalX)}) = {FormatNumberString(state.XRegister)}";
            state.IsNewNumber = true;
            state.HasDecimalPoint = false;
            state.CurrentInput = FormatNumberString(state.XRegister);
            state.YRegister = state.XRegister; // Результат становится Y для следующей операции
        }

        // Натуральный логарифм (ln)
        public void LnOperation(CalculatorState state, string opCode)
        {
            double originalX = state.XRegister;
            if (originalX <= 0)
            {
                state.XRegister = double.NaN; // Логарифм от неположительного числа
            }
            else
            {
                state.XRegister = Math.Log(originalX); // Math.Log(originalX) - это натуральный логарифм (по основанию e)
            }
            state.OperationLog = $"{opCode}({FormatNumberString(originalX)}) = {FormatNumberString(state.XRegister)}";
            state.IsNewNumber = true;
            state.HasDecimalPoint = false;
            state.CurrentInput = FormatNumberString(state.XRegister);
            state.YRegister = state.XRegister; // Результат становится Y для следующей операции
        }
    }
}