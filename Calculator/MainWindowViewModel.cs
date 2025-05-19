using Calculator.Business.Models;
using System.ComponentModel;
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private CalculatorState _state = new CalculatorState();
        private CalculatorService _service = new CalculatorService();

        public string Display => _state.Display;

        public void PressDigit(string digit)
        {
            _service.PressDigit(_state, digit);
            OnPropertyChanged(nameof(Display));
        }

        public void PressOperation(string op)
        {
            _service.PressOperation(_state, op);
            OnPropertyChanged(nameof(Display));
        }

        public void PressEqual()
        {
            _service.PressEqual(_state);
            OnPropertyChanged(nameof(Display));
        }

        public void PressClear()
        {
            _service.PressClear(_state);
            OnPropertyChanged(nameof(Display));
        }

        public void PressClearEntry()
        {
            _service.PressClearEntry(_state);
            OnPropertyChanged(nameof(Display));
        }

        public void PressBackspace()
        {
            _service.PressBackspace(_state);
            OnPropertyChanged(nameof(Display));
        }

        public void PressSqrt() => DoUpdate(() => _service.PressSqrt(_state));
        public void PressSquare() => DoUpdate(() => _service.PressSquare(_state));
        public void PressInverse() => DoUpdate(() => _service.PressInverse(_state));
        public void PressNegate() => DoUpdate(() => _service.PressNegate(_state));
        public void PressComma() => DoUpdate(() => _service.PressComma(_state));

        private void DoUpdate(System.Action action)
        {
            action();
            OnPropertyChanged(nameof(Display));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}