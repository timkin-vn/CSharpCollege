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
            var digitString = ((Button)sender).Content as string;
            ViewModel.PressDigit(digitString);
        }

        private void PointButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressDecimal();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressClear();
        }

        private void MoveXToYButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressMoveXToY();
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            var operationCode = ((Button)sender).Content as string;
            ViewModel.PressOperation(operationCode);
        }

        private void SinButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressSin();
        }

        private void CosButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressCos();
        }

        private void TanButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressTg();
        }

        private void CotButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressCtg();
        }
    }
}
