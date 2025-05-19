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
            var content = (string)((Button)sender).Content;
            if (content == "C")
                ViewModel.PressClearAll();
            else if (content == "CE")
                ViewModel.PressClearEntry();
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            var operation = (string)((Button)sender).Content;
            ViewModel.PressOperation(operation);
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressEqual();
        }

        private void DotButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressDot();
        }

        private void SignButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressSignChange();
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressBackspace();
        }
    }
}
