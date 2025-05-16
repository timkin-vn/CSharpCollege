using CalculatorWPF.ViewModels;
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

namespace CalculatorWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
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

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddDecimal();
        }

        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressSquare();
        }

        private void SquareRootButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressSquareRoot();
        }

        private void LogarithmButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressLogarithm();
        }
    }
}
