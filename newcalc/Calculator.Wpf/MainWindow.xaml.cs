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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SinBUttonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.PressSin();
        }

        private void ChangeButton(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            
            button.Content = button.Content.ToString() == "R" ? "С" : "R";
            ViewModel.PressChange(); 
        }

        private void SqrtButton(object sender, RoutedEventArgs e)
        {
            ViewModel.PressSqrt();
        }

        private void PercentButton(object sender, RoutedEventArgs e)
        {
            ViewModel.PressPercent();
        }

        private void BackspaceButton(object sender, RoutedEventArgs e)
        {
            ViewModel.PressBack();
        }

        private void CosButton(object sender, RoutedEventArgs e)
        {
            ViewModel.PressCos();
        }

        private void TgButton(object sender, RoutedEventArgs e)
        {
            ViewModel.PressTg();
        }

        private void LogButton(object sender, RoutedEventArgs e)
        {
            ViewModel.PressLog();
        }

        private void DegreeButton(object sender, RoutedEventArgs e)
        {
            ViewModel.PressDegree();
        }

        private void ChangeSign(object sender, RoutedEventArgs e)
        {
            ViewModel.ChangeSign();
        }
    }
}
