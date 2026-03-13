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

        private void SqrtButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressSqrt();
        }

        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressPower();
        }

        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressPercent();
        }

        private void AbsButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressAbs();
        }

        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressSquare();
        }

    }
}
