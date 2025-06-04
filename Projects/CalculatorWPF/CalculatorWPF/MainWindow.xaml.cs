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
        public MainWindowsViewModel ViewModel => (MainWindowsViewModel)DataContext;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowsViewModel();
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            var digit = (string)((Button)sender).Content;
            ViewModel.PressDigit(digit);
        }

        private void CleanButton_click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressClear();
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            var operationCode = (string)((Button)sender).Content;
            ViewModel.PressOperation(operationCode);
        }

        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressLog();
        }

        private void SinButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressSin();
        }

        private void CosButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressCos();
        }

        private void SqrtButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressSqrt();
        }

        private void DegreeButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressDegree();
        }

        private void UpdateSignButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressUpdSign();
        }

        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressPercent();
        }

        private void CleanHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressCleanHistory();
        }
    }
}
