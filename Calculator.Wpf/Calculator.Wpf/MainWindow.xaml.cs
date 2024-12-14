using Calculator.Business.Entites;
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
        }

        private void DigitButoon_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.Content is string digit)
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
            var button = (Button)sender;
            if (button.Content is string opCode)
            {
                ViewModel.PressOperation(opCode);
            }
        }



        private void Degree2click(object sender, RoutedEventArgs e)
        {
                ViewModel.PressDegree2();
        }

        private void Click_1x_click(object sender, RoutedEventArgs e)
        {
            ViewModel.Press_1x();
        }

        

        private void ClearLogButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearLog();
        }


        private void FULL_Clear_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearLog();
            ViewModel.Clear();
        }

    }
}
