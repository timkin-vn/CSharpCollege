using Calculator.Wpf.ViewModels;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            KeyDown += MainWindow_KeyDown;
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Content is string digit)
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
            if (sender is Button button && button.Content is string opCode)
            {
                ViewModel.PressOperation(opCode);
            }
        }

        private void ClearLogButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearLog();
        }
        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CalculatePercentage();
        }
        private void SquareRootButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CalculateSquareRoot();
        }
        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CalculatePower();
        }
        private void SinButton_Click(object sender, RoutedEventArgs e) => ViewModel.CalculateSin();
        private void CosButton_Click(object sender, RoutedEventArgs e) => ViewModel.CalculateCos();
        private void TanButton_Click(object sender, RoutedEventArgs e) => ViewModel.CalculateTan();
        private void LogButton_Click(object sender, RoutedEventArgs e) => ViewModel.CalculateLog();
        private void LnButton_Click(object sender, RoutedEventArgs e) => ViewModel.CalculateLn();


        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                // Digits
                case Key.D0:
                case Key.NumPad0:
                    ViewModel.InsertDigit("0");
                    break;
                case Key.D1:
                case Key.NumPad1:
                    ViewModel.InsertDigit("1");
                    break;
                case Key.D2:
                case Key.NumPad2:
                    ViewModel.InsertDigit("2");
                    break;
                case Key.D3:
                case Key.NumPad3:
                    ViewModel.InsertDigit("3");
                    break;
                case Key.D4:
                case Key.NumPad4:
                    ViewModel.InsertDigit("4");
                    break;
                case Key.D5:
                case Key.NumPad5:
                    ViewModel.InsertDigit("5");
                    break;
                case Key.D6:
                case Key.NumPad6:
                    ViewModel.InsertDigit("6");
                    break;
                case Key.D7:
                case Key.NumPad7:
                    ViewModel.InsertDigit("7");
                    break;
                case Key.D8:
                case Key.NumPad8:
                    ViewModel.InsertDigit("8");
                    break;
                case Key.D9:
                case Key.NumPad9:
                    ViewModel.InsertDigit("9");
                    break;

                // Operations
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
                case Key.Oem2: // '/' на стандартной клавиатуре
                    ViewModel.PressOperation("/");
                    break;

                // Result
                case Key.Enter:
                    ViewModel.PressOperation("=");
                    break;

                // Clear
                case Key.Back:
                case Key.Delete:
                    ViewModel.Clear();
                    break;
            }
        }
    }
}
