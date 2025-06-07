// File: Calculator.Business.Models/CalculatorState.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Models
{
    public class CalculatorState
    {
        public double XRegister { get; set; } // Текущее число (введенное или результат)

        public double YRegister { get; set; } // Первое число для бинарных операций (число перед операцией)

        public string OpCodeRegister { get; set; } // Ожидающая операция (+, -, *, /, logXY)

        public bool IsNewNumber { get; set; } = true; // Флаг: true, если следующий ввод цифры должен начать новое число (после операции или '=')

        public bool HasDecimalPoint { get; set; } // Флаг: true, если текущее вводимое число уже содержит десятичную точку

        public string CurrentInput { get; set; } = "0"; // Строковое представление текущего вводимого числа для отображения на дисплее

        public string OperationLog { get; set; } // Строка для записи последней выполненной операции для истории (потом добавится в ObservableCollection)
    }
}