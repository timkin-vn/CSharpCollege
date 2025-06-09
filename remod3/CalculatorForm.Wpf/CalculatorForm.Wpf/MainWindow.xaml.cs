using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CalculatorForm.Business.Services;
using CalculatorForm.Business.Models;

namespace CalculatorForm.Wpf {
    public partial class MainWindow : Window {
        private readonly CalculatorState _state = new CalculatorState();
        private readonly CalculatorService _service = new CalculatorService();

        public MainWindow() {
            InitializeComponent();
            this.KeyDown += CalculatorForm_KeyDown;
        }

        private void CalculatorForm_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                CalculatorService.Clear(_state);
                SineButton.Content = "sin";
                ShowResult();
                e.Handled = true;
            }
            else if (e.Key >= Key.D0 && e.Key <= Key.D9) {
                _service.PressDigit(_state, (e.Key - Key.D0).ToString());
                ShowResult();
                e.Handled = true;
            }
            else if (e.Key == Key.Add) {
                _service.PressOperation(_state, "+");
                ShowResult();
                e.Handled = true;
            }
            else if (e.Key == Key.Subtract) {
                _service.PressOperation(_state, "-");
                ShowResult();
                e.Handled = true;
            }
            else if (e.Key == Key.Multiply) {
                _service.PressOperation(_state, "*");
                ShowResult();
                e.Handled = true;
            }
            else if (e.Key == Key.Divide) {
                _service.PressOperation(_state, "/");
                ShowResult();
                e.Handled = true;
            }
            else if (e.Key == Key.Enter) {
                CalculatorService.PressEqual(_state);
                ShowResult();
                e.Handled = true;
            }
            else if (e.Key == Key.Decimal || e.Key == Key.OemPeriod) {
                CalculatorService.AddDecimal(_state);
                ShowResult();
                e.Handled = true;
            }
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e) {
            var digit = (sender as Button).Content.ToString();
            _service.PressDigit(_state, digit);
            ShowResult();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e) {
            CalculatorService.Clear(_state);
            SineButton.Content = "sin";
            ShowResult();
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e) {
            var operation = (sender as Button).Content.ToString();
            _service.PressOperation(_state, operation);
            ShowResult();
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e) {
            CalculatorService.PressEqual(_state);
            ShowResult();
        }

        private void FunctionButton_Click(object sender, RoutedEventArgs e) {
            var function = (sender as Button).Content.ToString();
            switch (function) {
                case "√":
                    CalculatorService.SquareRoot(_state);
                    break;
                case "sin":
                case "cos":
                case "tan":
                case "cot":
                case "arcsin":
                case "arccos":
                case "arctan":
                case "arccot":
                    CalculatorService.ComputeTrigFunction(_state);
                    break;
                case "x²":
                    CalculatorService.Square(_state);
                    break;
                case "%":
                    CalculatorService.Percent(_state);
                    break;
                case "+/-":
                    CalculatorService.ChangeSign(_state);
                    break;
                case ".":
                    CalculatorService.AddDecimal(_state);
                    break;
                case "MS":
                    CalculatorService.MemoryStore(_state);
                    break;
                case "MR":
                    CalculatorService.MemoryRecall(_state);
                    break;
            }
            ShowResult();
        }

        private void TrigMenuButton_Click(object sender, RoutedEventArgs e) {
            ContextMenu contextMenu = new ContextMenu();
            contextMenu.Background = new SolidColorBrush(Color.FromRgb(40, 40, 40));
            contextMenu.Foreground = new SolidColorBrush(Colors.White);
            foreach (var func in new[] { "sin", "cos", "tan", "cot", "arcsin", "arccos", "arctan", "arccot" }) {
                var menuItem = new MenuItem { Header = func };
                menuItem.Click += (_, _) => SetTrigFunction(func);
                contextMenu.Items.Add(menuItem);
            }
            contextMenu.IsOpen = true;
            contextMenu.PlacementTarget = sender as UIElement;
            contextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
        }

        private void SetTrigFunction(string function) {
            _state.SelectedFunction = function;
            SineButton.Content = function;
        }

        private void ShowResult() {
            DisplayTextBlock.Text = !string.IsNullOrEmpty(_state.CurrentInput)
                ? _state.CurrentInput
                : _state.RegisterX.ToString("F6").TrimEnd('0').TrimEnd('.');
            ExpressionTextBlock.Text = _state.Expression;
        }
    }
}