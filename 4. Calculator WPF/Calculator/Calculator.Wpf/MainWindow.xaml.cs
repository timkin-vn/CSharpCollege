using Calculator.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            var digit = ((Button)sender).Content.ToString();
            ViewModel.PressDigit(digit);
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            var separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            ViewModel.PressDecimal(separator);
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            var operation = ((Button)sender).Content.ToString();
            ViewModel.PressOperation(operation);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressClear();
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressEqual();
        }

        private void SqrtButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressSqrt();
        }

        private void SqrButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressSqr();
        }
    }
}
