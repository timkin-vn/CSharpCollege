using Calculator.Business.Entites;
using Calculator.Business.Services;
using System.Windows;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media.Animation;
using System;

namespace Calculator.Wpf.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private CalculatorService _service = new CalculatorService();

        private CalculatorState _state = new CalculatorState();

        public string OutputDisplay { get; set; } = "0";

        public ObservableCollection<string> OperationLog { get; set; } = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        // Themes
        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                _isDarkTheme = value;
                OnPropertyChanged(nameof(IsDarkTheme));
                UpdateTheme();
            }
        }

        // Temp History
        private string _currentOperationDisplay = string.Empty;
        public string CurrentOperationDisplay
        {
            get => _currentOperationDisplay;
            set
            {
                _currentOperationDisplay = value;
                OnPropertyChanged(nameof(CurrentOperationDisplay));
            }
        }

        public void InsertDigit(string digit)
        {
            _service.InsertDigit(_state, digit);
            OutputDisplay = _state.XRegister.ToString();
            OnPropertyChanged(nameof(OutputDisplay));
        }

        public void Clear()
        {
            _service.Clear(_state);
            OutputDisplay = "0";
            OnPropertyChanged(nameof(OutputDisplay));
            OperationLog.Clear();
        }
        public void PressOperation(string opCode)
        {
            try
            {
                _service.InsertOperation(_state, opCode);

                OutputDisplay = _state.XRegister.ToString();
                OnPropertyChanged(nameof(OutputDisplay));

                if (opCode != "=")
                {
                    CurrentOperationDisplay = $"{_state.YRegister} {opCode}";
                }
                else
                {
                    CurrentOperationDisplay = string.Empty;
                }
            }
            catch (InvalidOperationException ex)
            {
                OutputDisplay = "Error";
                CurrentOperationDisplay = ex.Message;
                OnPropertyChanged(nameof(OutputDisplay));
                OnPropertyChanged(nameof(CurrentOperationDisplay));
            }
        }

        public void ClearLog()
        {
            OperationLog.Clear();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateTheme()
        {
            Application.Current.Resources["WindowBackgroundColor"] = IsDarkTheme
                ? new SolidColorBrush(Color.FromRgb(34, 34, 34))
                : new SolidColorBrush(Color.FromRgb(250, 250, 250));

            Application.Current.Resources["ButtonBackgroundColor"] = IsDarkTheme
                ? new SolidColorBrush(Color.FromRgb(64, 64, 64))
                : new SolidColorBrush(Color.FromRgb(238, 238, 238));

            Application.Current.Resources["ButtonForegroundColor"] = IsDarkTheme
                ? new SolidColorBrush(Color.FromRgb(250, 250, 250))
                : new SolidColorBrush(Color.FromRgb(34, 34, 34));
        }

        public void CalculatePercentage()
        {
            _service.CalculatePercentage(_state);
            OutputDisplay = _state.XRegister.ToString();
            OnPropertyChanged(nameof(OutputDisplay));
            if (!string.IsNullOrEmpty(_state.OperationLog))
            {
                OperationLog.Add(_state.OperationLog);
            }
        }
        public void CalculateSquareRoot()
        {
            _service.CalculateSquareRoot(_state);
            OutputDisplay = _state.XRegister.ToString();
            OnPropertyChanged(nameof(OutputDisplay));
            if (!string.IsNullOrEmpty(_state.OperationLog))
            {
                OperationLog.Add(_state.OperationLog);
            }
        }
        public void CalculatePower()
        {
            _service.CalculatePower(_state);
            OutputDisplay = _state.XRegister.ToString();
            OnPropertyChanged(nameof(OutputDisplay));
            if (!string.IsNullOrEmpty(_state.OperationLog))
            {
                OperationLog.Add(_state.OperationLog);
            }
        }
        public void CalculateSin() => PerformUnaryOperation(_service.CalculateSin);
        public void CalculateCos() => PerformUnaryOperation(_service.CalculateCos);
        public void CalculateTan() => PerformUnaryOperation(_service.CalculateTan);
        public void CalculateLog() => PerformUnaryOperation(_service.CalculateLog);
        public void CalculateLn() => PerformUnaryOperation(_service.CalculateLn);

        private void PerformUnaryOperation(Action<CalculatorState> operation)
        {
            operation(_state);
            OutputDisplay = _state.XRegister.ToString();
            OnPropertyChanged(nameof(OutputDisplay));
            if (!string.IsNullOrEmpty(_state.OperationLog))
            {
                OperationLog.Add(_state.OperationLog);
            }
        }
    }
}
