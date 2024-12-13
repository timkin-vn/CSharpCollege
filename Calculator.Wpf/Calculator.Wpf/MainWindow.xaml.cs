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
        private void DecimalPointButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.Content is string digit)
            {
                ViewModel.InsertDecimalPoint(digit);
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
        private void CleaEm_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearEmOperationLog();
        }
        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.BackspaceOperation();
        }
        private void RowOperationButton_click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.Content is string opCode)
            {
                ViewModel.RowOperation(opCode);
            }
        }
        private void PowOperationButton_click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.Content is string opCode)
            {
                ViewModel.PowOperation(opCode);
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
        private void LnOperationButton_click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.Content is string opCode)
            {
                ViewModel.LnOperation(opCode);
            }
        } 
        private void Fractional_DivisioButton_click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.Content is string opCode)
            {
                ViewModel.Fractional_DivisioOperation(opCode);
            }
        }
    }
}
