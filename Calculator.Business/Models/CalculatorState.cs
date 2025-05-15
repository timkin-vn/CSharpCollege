namespace Calculator.Business.Models
{
    public class CalculatorState
    {
        // Основные регистры
        public double RegisterX { get; set; } = 0;
        public double RegisterY { get; set; } = 0;
        public string OperationCode { get; set; } = "";
        public bool IsClearNeeded { get; set; } = false;

        // Научные функции
        public bool IsRadiansMode { get; set; } = false;

        // Память
        public double Memory { get; set; } = 0;

        // Ошибки
        public bool HasError { get; set; } = false;
        public string LastError { get; set; } = "";

        // Вспомогательные флаги
        public bool IsNewOperation { get; set; } = true;
    }
}