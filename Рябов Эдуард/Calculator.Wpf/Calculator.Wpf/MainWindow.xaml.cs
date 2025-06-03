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
using System.Windows.Media.Media3D;
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


        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {
            var squareCode = (string)((Button)sender).Content;
            ViewModel.Square(squareCode);
        }

        private void SquareRootButton_Click(object sender, RoutedEventArgs e)
        {

            var squareRootCode = (string)((Button)sender).Content;
            ViewModel.SquareRoot(squareRootCode);

        }

        private void InverseButton_Click(object sender, RoutedEventArgs e)
        {
            var sInverseCode = (string)((Button)sender).Content;
            ViewModel.Inverse(sInverseCode);
        }

       

    }
}
