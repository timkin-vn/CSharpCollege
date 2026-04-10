using CalculatorWPF.Models;
using CalculatorWPF.Services;
using System.Windows;
using System.Windows.Controls;

namespace CalculatorWPF
{
    public partial class MainWindow : Window
    {
        private CalculatorModel _calculatorModel;
        private CalculatorService _service;

        public MainWindow()
        {
            InitializeComponent();
            _calculatorModel = new CalculatorModel();
            _service = new CalculatorService();
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            _service.PressDigit(_calculatorModel, button.Content.ToString());
            DisplayValue();
        }

        private void CommaButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_calculatorModel.IsDrob)
            {
                _calculatorModel.IsDrob = true;
                DisplayLabel.Text += ",";
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            _service.PressClear(_calculatorModel);
            DisplayValue();
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            _service.PressOperation(_calculatorModel, button.Content.ToString());
            DisplayValue();
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            _service.PressEqual(_calculatorModel);
            DisplayValue();
        }

        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {
            _service.PressSquare(_calculatorModel);
            DisplayValue();
        }

        private void DisplayValue()
        {
            string displayText = _calculatorModel.RegisterX.ToString();
            displayText = displayText.Replace('.', ',');
            DisplayLabel.Text = displayText;
        }
    }
}