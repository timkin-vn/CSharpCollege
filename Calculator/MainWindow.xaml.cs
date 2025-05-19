using System.Windows;
using System.Windows.Controls;
using Calculator.Wpf.ViewModels;

namespace Calculator.Wpf
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void Digit_Click(object sender, RoutedEventArgs e) =>
            ViewModel.PressDigit(((Button)sender).Content.ToString());

        private void Operation_Click(object sender, RoutedEventArgs e) =>
            ViewModel.PressOperation(((Button)sender).Content.ToString());

        private void Equal_Click(object sender, RoutedEventArgs e) =>
            ViewModel.PressEqual();

        private void Clear_Click(object sender, RoutedEventArgs e) =>
            ViewModel.PressClear();

        private void ClearEntry_Click(object sender, RoutedEventArgs e) =>
            ViewModel.PressClearEntry();

        private void Backspace_Click(object sender, RoutedEventArgs e) =>
            ViewModel.PressBackspace();

        private void Sqrt_Click(object sender, RoutedEventArgs e) =>
            ViewModel.PressSqrt();

        private void Square_Click(object sender, RoutedEventArgs e) =>
            ViewModel.PressSquare();

        private void Inverse_Click(object sender, RoutedEventArgs e) =>
            ViewModel.PressInverse();

        private void Negate_Click(object sender, RoutedEventArgs e) =>
            ViewModel.PressNegate();

        private void Comma_Click(object sender, RoutedEventArgs e) =>
            ViewModel.PressComma();
    }
}