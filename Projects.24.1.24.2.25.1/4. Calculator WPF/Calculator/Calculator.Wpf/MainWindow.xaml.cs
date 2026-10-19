using Calculator.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private bool _isDark;

        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            ViewModel.History.CollectionChanged += History_CollectionChanged;
        }

        private void History_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (HistoryList.Items.Count > 0)
            {
                HistoryList.ScrollIntoView(HistoryList.Items[HistoryList.Items.Count - 1]);
            }
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

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressEqual();
        }

        private void PointButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressDecimalPoint();
        }

        private void SignButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressToggleSign();
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressBackspace();
        }

        private void ClearHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearHistory();
        }

        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            _isDark = !_isDark;
            ThemeButton.Content = _isDark ? "Светлая тема" : "Тёмная тема";

            Resources["WindowBrush"] = CreateBrush(_isDark ? "#202024" : "#F0F0F0");
            Resources["PanelBrush"] = CreateBrush(_isDark ? "#2B2B30" : "#FFFFFF");
            Resources["ButtonBrush"] = CreateBrush(_isDark ? "#3A3A42" : "#E1E1E1");
            Resources["TextBrush"] = CreateBrush(_isDark ? "#DCDCDC" : "#000000");
        }

        private static Brush CreateBrush(string hex)
        {
            return (Brush)new BrushConverter().ConvertFromString(hex);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    ViewModel.PressEqual();
                    e.Handled = true;
                    break;

                case Key.Back:
                    ViewModel.PressBackspace();
                    e.Handled = true;
                    break;

                case Key.Escape:
                    ViewModel.PressClear();
                    e.Handled = true;
                    break;
            }
        }

        private void Window_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var text = e.Text;

            if (text.Length != 1)
            {
                return;
            }

            var key = text[0];

            if (key >= '0' && key <= '9')
            {
                ViewModel.PressDigit(text);
            }
            else if (key == '+' || key == '-' || key == '*' || key == '/')
            {
                ViewModel.PressOperation(text);
            }
            else if (key == '=')
            {
                ViewModel.PressEqual();
            }
            else if (key == '.' || key == ',')
            {
                ViewModel.PressDecimalPoint();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }
    }
}