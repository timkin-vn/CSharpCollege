// File: Calculator.Business.Models/CalculatorState.cs
using System;

namespace Calculator.Business.Models
{
    public class CalculatorState
    {
        public double XRegister { get; set; } = 0; // Текущее число на дисплее
        public double YRegister { get; set; } = 0; // Первое число для бинарной операции
        public string OpCodeRegister { get; set; } = null; // Текущая ожидающая операция (+, -, *, /)
        public bool NeedClear { get; set; } = true; // Флаг: если true, следующий ввод цифры очистит дисплей
    }
}
