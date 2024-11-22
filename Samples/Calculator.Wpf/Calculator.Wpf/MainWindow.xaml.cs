using Calculator.Wpf.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Calculator.Wpf
{
    public partial class MainWindow : Window
    {
        internal CalculatorViewModel ViewModel => (CalculatorViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.Content is string digit)
            {
                ViewModel.InsertDigit(digit);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Clear();
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.Content is string opCode)
            {
                ViewModel.InsertOperation(opCode);
            }
        }

        private void ClearOperationLog_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearOperationLog();
        }
    }
}