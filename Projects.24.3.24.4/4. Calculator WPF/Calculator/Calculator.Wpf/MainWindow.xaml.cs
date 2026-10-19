using Calculator.Wpf.ViewModels;
using System;
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
            var digitString = ((Button)sender).Content as string;
            ViewModel.PressDigit(digitString);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressClear();
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            var operationCode = ((Button)sender).Content as string;
            ViewModel.PressOperatin(operationCode);
        }

        private void DotButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressOperatin(",");
        }

        private void SignButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressOperatin("+/-");
        }
    }
}
