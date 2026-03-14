using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfHello.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public string XInput { get; set; }

        public string YInput { get; set; }

        public string Result { get; set; } = "0";

        public event PropertyChangedEventHandler PropertyChanged;

        public void Add()
        {
            if (!double.TryParse(XInput, out var x))
            {
                return;
            }

            if (!double.TryParse(YInput, out var y))
            {
                return;
            }

            var result = x + y;
            Result = result.ToString();
            OnPropertyChanged(nameof(Result));
        }

        public void Subtract()
        {
            if (!double.TryParse(XInput, out var x))
            {
                return;
            }

            if (!double.TryParse(YInput, out var y))
            {
                return;
            }

            var result = x - y;
            Result = result.ToString();
            OnPropertyChanged(nameof(Result));
        }

        public void Multiply()
        {
            if (!double.TryParse(XInput, out var x))
            {
                return;
            }

            if (!double.TryParse(YInput, out var y))
            {
                return;
            }

            var result = x * y;
            Result = result.ToString();
            OnPropertyChanged(nameof(Result));
        }

        public void Divide()
        {
            if (!double.TryParse(XInput, out var x))
            {
                return;
            }

            if (!double.TryParse(YInput, out var y))
            {
                return;
            }

            var result = x / y;
            Result = result.ToString();
            OnPropertyChanged(nameof(Result));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
