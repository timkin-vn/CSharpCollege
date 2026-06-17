using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfHello.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public string XInput { get; set; }

        public string YInput { get; set; }

        public string Result => _result.ToString();

        private double _result;

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
            _result = result;

            OnPropertyChanged(nameof(Result));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
