
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;

namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _expression = "";
        private string _result = "0";
        private double _memory;
        private bool _justEvaluated;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Expression
        {
            get { return _expression; }
            private set
            {
                _expression = value;
                OnPropertyChanged(nameof(Expression));
            }
        }

        public string Result
        {
            get { return _result; }
            private set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public string MemoryValue
        {
            get { return "M = " + _memory.ToString(CultureInfo.CurrentCulture); }
        }

        public ObservableCollection<string> History { get; }
            = new ObservableCollection<string>();

        public void InputToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return;

            if (_justEvaluated)
            {
                if (IsOperator(token))
                {
                    Expression = Result;
                }
                else
                {
                    Expression = "";
                    Result = "0";
                }

                _justEvaluated = false;
            }

            if (token == ".")
                token = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Expression += token;
        }

        public void PressClear()
        {
            Expression = "";
            Result = "0";
            _justEvaluated = false;
        }

        public void PressEqual()
        {
            if (string.IsNullOrWhiteSpace(Expression))
                return;

            try
            {
                double value = new ExpressionParser(Expression).Parse();

                if (double.IsNaN(value) || double.IsInfinity(value))
                    throw new ArithmeticException("Результат не является конечным числом.");

                string formatted = value.ToString("G15", CultureInfo.CurrentCulture);

                History.Insert(0, Expression + " = " + formatted);

                while (History.Count > 100)
                    History.RemoveAt(History.Count - 1);

                Result = formatted;
                Expression = "";
                _justEvaluated = true;
            }
            catch (DivideByZeroException)
            {
                Result = "Деление на ноль";
                _justEvaluated = true;
            }
            catch (Exception ex)
            {
                Result = "Ошибка: " + ex.Message;
                _justEvaluated = true;
            }
        }

        public void ClearHistory()
        {
            History.Clear();
        }

        public void MemoryAdd()
        {
            if (TryGetCurrentValue(out double value))
            {
                _memory += value;
                OnPropertyChanged(nameof(MemoryValue));
            }
        }

        public void MemoryRecall()
        {
            Expression = _memory.ToString("G15", CultureInfo.CurrentCulture);
            Result = Expression;
            _justEvaluated = false;
        }

        public void MemoryClear()
        {
            _memory = 0;
            OnPropertyChanged(nameof(MemoryValue));
        }

        private bool TryGetCurrentValue(out double value)
        {
            if (double.TryParse(
                Result,
                NumberStyles.Float,
                CultureInfo.CurrentCulture,
                out value))
            {
                return true;
            }

            try
            {
                value = new ExpressionParser(Expression).Parse();
                return true;
            }
            catch
            {
                value = 0;
                return false;
            }
        }

        private static bool IsOperator(string token)
        {
            return token == "+" || token == "-" ||
                   token == "*" || token == "/";
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }

        private sealed class ExpressionParser
        {
            private readonly string _text;
            private int _position;

            public ExpressionParser(string text)
            {
                _text = text.Replace(',', '.');
            }

            public double Parse()
            {
                double value = ParseExpression();
                SkipSpaces();

                if (_position < _text.Length)
                    throw new FormatException("Лишние символы в выражении.");

                return value;
            }

            private double ParseExpression()
            {
                double value = ParseTerm();

                while (true)
                {
                    SkipSpaces();

                    if (Match('+'))
                        value += ParseTerm();
                    else if (Match('-'))
                        value -= ParseTerm();
                    else
                        return value;
                }
            }

            private double ParseTerm()
            {
                double value = ParseFactor();

                while (true)
                {
                    SkipSpaces();

                    if (Match('*'))
                    {
                        value *= ParseFactor();
                    }
                    else if (Match('/'))
                    {
                        double divisor = ParseFactor();

                        if (divisor == 0)
                            throw new DivideByZeroException();

                        value /= divisor;
                    }
                    else
                    {
                        return value;
                    }
                }
            }

            private double ParseFactor()
            {
                SkipSpaces();

                if (Match('+'))
                    return ParseFactor();

                if (Match('-'))
                    return -ParseFactor();

                if (Match('('))
                {
                    double value = ParseExpression();
                    SkipSpaces();

                    if (!Match(')'))
                        throw new FormatException("Не закрыта скобка.");

                    return value;
                }

                return ParseNumber();
            }

            private double ParseNumber()
            {
                SkipSpaces();

                int start = _position;
                bool decimalPoint = false;

                while (_position < _text.Length)
                {
                    char c = _text[_position];

                    if (char.IsDigit(c))
                    {
                        _position++;
                    }
                    else if (c == '.' && !decimalPoint)
                    {
                        decimalPoint = true;
                        _position++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (start == _position)
                    throw new FormatException("Ожидалось число.");

                string number = _text.Substring(start, _position - start);

                if (!double.TryParse(
                    number,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out double value))
                {
                    throw new FormatException("Некорректное число.");
                }

                return value;
            }

            private bool Match(char expected)
            {
                if (_position >= _text.Length ||
                    _text[_position] != expected)
                    return false;

                _position++;
                return true;
            }

            private void SkipSpaces()
            {
                while (_position < _text.Length &&
                       char.IsWhiteSpace(_text[_position]))
                {
                    _position++;
                }
            }
        }
    }
}
