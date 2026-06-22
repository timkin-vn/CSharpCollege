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

        private static string GetButtonText(object sender)
        {
            return ((Button)sender).Content.ToString();
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressDigit(GetButtonText(sender));
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressClear();
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressOperation(GetButtonText(sender));
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressEqual();
        }

        private void CommaButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressComma();
        }

        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressSquare();
        }

        private void SquareRootButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressSquareRoot();
        }
    }
}
