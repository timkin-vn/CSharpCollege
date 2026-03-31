using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorModel _model = new CalculatorModel();
        private readonly CalculatorService _service = new CalculatorService();

        public event PropertyChangedEventHandler PropertyChanged;

        public string Result => _model.RegisterX.ToString(CultureInfo.CurrentCulture);

        public void PressDigit(string digit) => Update(() => _service.PressDigit(_model, digit));

        public void PressDecimal(string separator) => Update(() => _service.PressDecimal(_model, separator));

        public void PressOperation(string operation) => Update(() => _service.PressOperation(_model, operation));

        public void PressClear() => Update(() => _service.PressClear(_model));

        public void PressEqual() => Update(() => _service.PressEqual(_model));

        public void PressSqrt() => Update(() => _service.PressSqrt(_model));

        public void PressSqr() => Update(() => _service.PressSqr(_model));

        private void Update(System.Action action)
        {
            action();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
        }
    }
}
