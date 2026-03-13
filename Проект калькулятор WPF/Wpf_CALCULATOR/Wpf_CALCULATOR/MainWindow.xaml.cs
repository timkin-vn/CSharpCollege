using Calculator.Business.Models;
using Calculator.Business.Services;
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
using Wpf_CALCULATOR.ViewModels;

namespace Wpf_CALCULATOR
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

        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressSquare();
        }

        private void CommaButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressComma();
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressEqual();
        }
    }
}
