// File: Calculator.Wpf.ViewModel/CalculatorViewModel.cs
using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input; // Важно: добавлено для ICommand

namespace Calculator.Wpf.ViewModel
{
    // --- Вспомогательные классы RelayCommand для реализации ICommand ---

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        // !!! ИСПРАВЛЕНИЕ: canExecute = null делает его необязательным !!!
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();
        public void Execute(object parameter) => _execute();

        // Этот событие должно быть поднято, когда CanExecute меняется
        // CommandManager.RequerySuggested - стандартный способ в WPF для автоматической проверки CanExecute
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        // !!! ИСПРАВЛЕНИЕ: canExecute = null делает его необязательным !!!
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);
        public void Execute(object parameter) => _execute((T)parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    // --- Основной ViewModel ---

    internal class CalculatorViewModel : INotifyPropertyChanged
    {
        private CalculatorService _service = new CalculatorService();
        private CalculatorState _state = new CalculatorState(); // Используем обновленный CalculatorState

        private string _displayOutput = "0";
        public string DisplayOutput
        {
            get => _displayOutput;
            set
            {
                if (_displayOutput != value)
                {
                    _displayOutput = value;
                    OnPropertyChanged(nameof(DisplayOutput));
                }
            }
        }

        public ObservableCollection<string> OperationLog { get; set; } = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        // --- Команды для привязки в XAML ---
        public ICommand DigitCommand { get; private set; }
        public ICommand OperationCommand { get; private set; }
        public ICommand EqualsCommand { get; private set; }
        public ICommand ClearCommand { get; private set; } // Кнопка "AC"
        public ICommand NegateCommand { get; private set; } // Кнопка "+/-"
        public ICommand PercentCommand { get; private set; } // Кнопка "%"
        public ICommand DecimalCommand { get; private set; } // Кнопка "."

        // Команда для очистки лога истории
        public ICommand ClearLogCommand { get; private set; }

        // !!! НОВЫЕ КОМАНДЫ ДЛЯ НАУЧНЫХ ФУНКЦИЙ !!!
        public ICommand LnCommand { get; private set; } // Для натурального логарифма (ln)
        public ICommand LogXYCommand { get; private set; } // Для логарифма по основанию Y от X (log_Y(X))
        public ICommand SquareCommand { get; private set; } // Для X^2
        public ICommand SquareRootCommand { get; private set; } // Для √x
        public ICommand AbsoluteValueCommand { get; private set; } // Для |x|


        public CalculatorViewModel()
        {
            // Инициализация команд, привязка их к методам ViewModel
            DigitCommand = new RelayCommand<string>(InsertDigit);
            OperationCommand = new RelayCommand<string>(InsertOperation);
            EqualsCommand = new RelayCommand(CalculateResult);
            ClearCommand = new RelayCommand(ClearAll);
            NegateCommand = new RelayCommand(NegateNumber);
            PercentCommand = new RelayCommand(ApplyPercent);
            DecimalCommand = new RelayCommand(InsertDecimalPoint);
            ClearLogCommand = new RelayCommand(ClearOperationLog);

            // Инициализация НОВЫХ КОМАНД
            LnCommand = new RelayCommand(ApplyLn);
            LogXYCommand = new RelayCommand(ApplyLogXY); // logXY - бинарная операция, как +, -, *, /
            SquareCommand = new RelayCommand(ApplySquare);
            SquareRootCommand = new RelayCommand(ApplySquareRoot);
            AbsoluteValueCommand = new RelayCommand(ApplyAbsoluteValue);

            DisplayResult(); // Изначальное отображение "0" при запуске
        }

        // --- Методы ViewModel (вызываются командами) ---

        private void InsertDigit(string digit)
        {
            _service.InsertDigit(_state, digit);
            DisplayResult();
        }

        private void InsertOperation(string opCode)
        {
            _service.PressOperation(_state, opCode);
            DisplayResult();

            if (!string.IsNullOrEmpty(_state.OperationLog))
            {
                OperationLog.Add(_state.OperationLog);
                _state.OperationLog = null; // Очищаем в состоянии сервиса после добавления в UI
            }
        }

        private void CalculateResult()
        {
            _service.PressOperation(_state, "=");
            DisplayResult();

            if (!string.IsNullOrEmpty(_state.OperationLog))
            {
                OperationLog.Add(_state.OperationLog);
                _state.OperationLog = null;
            }
        }

        private void ClearAll()
        {
            _service.ClearAll(_state); // Вызываем ClearAll из сервиса
            DisplayResult();
            
        }

        private void NegateNumber()
        {
            _service.NegateNumber(_state); // Вызываем метод NegateNumber из сервиса
            DisplayResult();
            // Обычно изменение знака не добавляется в лог истории как отдельная операция.
        }

        private void ApplyPercent()
        {
            _service.ApplyPercent(_state); // Вызываем метод ApplyPercent из сервиса
            DisplayResult();
            if (!string.IsNullOrEmpty(_state.OperationLog))
            {
                OperationLog.Add(_state.OperationLog);
                _state.OperationLog = null;
            }
        }

        private void InsertDecimalPoint()
        {
            _service.InsertDecimal(_state); // Вызываем метод InsertDecimal из сервиса
            DisplayResult();
        }

        public void ClearOperationLog()
        {
            OperationLog.Clear();
        }


        private void ApplyLn()
        {
            _service.LnOperation(_state, "ln"); // Передаем "ln" для формирования лога
            DisplayResult();
            if (!string.IsNullOrEmpty(_state.OperationLog))
            {
                OperationLog.Add(_state.OperationLog);
                _state.OperationLog = null;
            }
        }

        private void ApplyLogXY()
        {
            // logXY - это бинарная операция, как +, -, *, /.
            // Она требует, чтобы YRegister уже было установлено (например, после нажатия первого числа и logXY).
            // Поэтому она вызывается через PressOperation, который обрабатывает ожидающие операции.
            _service.PressOperation(_state, "logXY");
            DisplayResult();
            if (!string.IsNullOrEmpty(_state.OperationLog))
            {
                OperationLog.Add(_state.OperationLog);
                _state.OperationLog = null;
            }
        }

        private void ApplySquare()
        {
            _service.DegretOperation(_state, "sq"); // Ваш существующий DegretOperation
            DisplayResult();
            if (!string.IsNullOrEmpty(_state.OperationLog))
            {
                OperationLog.Add(_state.OperationLog);
                _state.OperationLog = null;
            }
        }

        private void ApplySquareRoot()
        {
            _service.RowOperation(_state, "sqrt"); // Ваш существующий RowOperation
            DisplayResult();
            if (!string.IsNullOrEmpty(_state.OperationLog))
            {
                OperationLog.Add(_state.OperationLog);
                _state.OperationLog = null;
            }
        }

        private void ApplyAbsoluteValue()
        {
            _service.ABSOperation(_state, "abs"); // Ваш существующий ABSOperation
            DisplayResult();
            if (!string.IsNullOrEmpty(_state.OperationLog))
            {
                OperationLog.Add(_state.OperationLog);
                _state.OperationLog = null;
            }
        }

        // --- Вспомогательные методы ---

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Обновляет отображение на дисплее калькулятора
        private void DisplayResult()
        {
            DisplayOutput = _state.CurrentInput;
        }
    }
}