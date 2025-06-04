using Calculator.Wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;

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
            var digit = (string)((Button)sender).Content;
            ViewModel.PressDigit(digit);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressClear();
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            var operationCode = (string)((Button)sender).Content;
            ViewModel.PressOperation(operationCode);
        }

        private void Procent_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressProcent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressFloat();
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressEqual();
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressBackspace();
        }

        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressSqrt();
        }
    }
}
