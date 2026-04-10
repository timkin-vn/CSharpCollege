using Calculator.Wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            ViewModel.PressOperation(operationCode);
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressEqual();
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressBackspace();
        }

        private void SignButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressToggleSign();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                ViewModel.PressDigit(((int)(e.Key - Key.D0)).ToString());
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                ViewModel.PressDigit(((int)(e.Key - Key.NumPad0)).ToString());
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Add:
                    case Key.OemPlus:
                        ViewModel.PressOperation("+");
                        break;

                    case Key.Subtract:
                    case Key.OemMinus:
                        ViewModel.PressOperation("-");
                        break;

                    case Key.Multiply:
                        ViewModel.PressOperation("*");
                        break;

                    case Key.Divide:
                    case Key.Oem2:
                        ViewModel.PressOperation("/");
                        break;

                    case Key.Enter:
                        ViewModel.PressEqual();
                        break;

                    case Key.Back:
                        ViewModel.PressBackspace();
                        break;

                    case Key.Escape:
                        ViewModel.PressClear();
                        break;
                }
            }
        }
    }
}
