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
            var digit = ((Button)sender).Content.ToString();
            ViewModel.PressDigit(digit);
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            var operationCode = ((Button)sender).Content.ToString();
            ViewModel.PressOperation(operationCode);
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressEqual();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressClear();
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressBackspace();
        }

        private void Procent_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressProcent();
        }
    }
}