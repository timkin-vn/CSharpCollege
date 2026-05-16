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
using Calculator.WPF.ViewModels;

namespace Calculator.WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel  => (MainWindowViewModel)DataContext;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Digit_Button_Click(object sender, RoutedEventArgs e)
        {
            var digitString = ((Button)sender).Content as string;
            ViewModel.PressDigit(digitString);
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            ViewModel.PressClear();
        }

        private void OperationClick(object sender, RoutedEventArgs e)
        {
            var operationString = ((Button)sender).Content as string;
            ViewModel.PressOperation(operationString);
        }

        private void PresentCLick(object sender, RoutedEventArgs e)
        {
            ViewModel.PressPresent();
        }
    }
}
