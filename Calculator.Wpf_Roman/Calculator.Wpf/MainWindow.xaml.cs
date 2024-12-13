using Calculator.Wpf.ViewModel;
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
        internal CalculatorViewModel ViewModel => (CalculatorViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.Content is string digit)
            {
                ViewModel.InsertDigit(digit);
            }
        }
        private void RowOperationButton_click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.Content is string opCode)
            {
                ViewModel.RowOperation(opCode);
            }
        }
        private void DegetOperationButton_click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.Content is string opCode)
            {
                ViewModel.DegOperation(opCode);
            }
        }
        private void Neg_Poss_OperationButton_click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.Content is string opCode)
            {
                ViewModel.Neg_Poss_Operation(opCode);
            }
        } 
        private void LOg2Button_click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.Content is string opCode)
            {
                ViewModel.Log2Operation(opCode);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Clear();
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.Content is string opCode)
            {
                ViewModel.InsertOperation(opCode);
            }
        }

        private void ClearOperationLog_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearOperationLog();
        }
    }
}
