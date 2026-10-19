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
            ViewModel.PressDigit(((Button)sender).Content.ToString());
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressClear();
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressOperation(((Button)sender).Content.ToString());
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
    }
}
