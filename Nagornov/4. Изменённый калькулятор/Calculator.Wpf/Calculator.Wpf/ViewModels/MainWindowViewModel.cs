using Calculator.Business.Models;
using Calculator.Business.Services;
using System.ComponentModel;

namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorState _state = new CalculatorState();
        private readonly CalculatorService _service = new CalculatorService();

        public event PropertyChangedEventHandler PropertyChanged;

        public string DisplayValue => _state.RegisterX.ToString();

        public void PressDigit(string digit)
        {
            _service.PressDigit(_state, digit);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressClear()
        {
            _service.Clear(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressOperation(string operationCode)
        {
            _service.PressOperation(_state, operationCode);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressEqual()
        {
            _service.PressEqual(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void SquareRoot_Click(object sender, System.EventArgs e)
        {
            _service.SquareRoot(_state);
            ShowResult();
        }

        public void PressPi_Click(object sender, System.EventArgs e)
        {
            _service.PressPi(_state);
            ShowResult();
        }

        public void DecimalPoint_Click(object sender, System.EventArgs e)
        {
            _service.DecimalPoint(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        private void ShowResult()
        {
            OnPropertyChanged(nameof(DisplayValue));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}