using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfCalculator
{
    public partial class MainWindow : Window
    {
        private string D;
        private string N1;
        private bool n2;

        public MainWindow()
        {
            InitializeComponent();
            n2 = false;
        }

        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            if (n2)
            {
                n2 = false;
                Display.Text = "0";
            }

            Button b = (Button)sender;
            if (Display.Text == "0")
                Display.Text = b.Content.ToString();
            else
                Display.Text += b.Content.ToString();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Display.Text = "0";
        }

        private void ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            Display.Text = "0";
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            D = b.Content.ToString();
            N1 = Display.Text;
            n2 = true;
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            double dn1 = Convert.ToDouble(N1);
            double dn2 = Convert.ToDouble(Display.Text);
            double res = 0;

            switch (D)
            {
                case "+": res = dn1 + dn2; break;
                case "-": res = dn1 - dn2; break;
                case "X": res = dn1 * dn2; break;
                case "/": res = dn1 / dn2; break;
                case "%": res = dn1 * dn2 / 100; break;
            }

            Display.Text = res.ToString();
            D = "=";
            n2 = true;
        }

        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            double dn = Convert.ToDouble(Display.Text);
            Display.Text = Math.Sqrt(dn).ToString();
        }

        private void Square_Click(object sender, RoutedEventArgs e)
        {
            double dn = Convert.ToDouble(Display.Text);
            Display.Text = Math.Pow(dn, 2).ToString();
        }

        private void Inverse_Click(object sender, RoutedEventArgs e)
        {
            double dn = Convert.ToDouble(Display.Text);
            Display.Text = (1 / dn).ToString();
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text.Length > 1)
                Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
            else
                Display.Text = "0";
        }

        private void Comma_Click(object sender, RoutedEventArgs e)
        {
            if (!Display.Text.Contains(","))
                Display.Text += ",";
        }

        private void Negate_Click(object sender, RoutedEventArgs e)
        {
            double dn = Convert.ToDouble(Display.Text);
            Display.Text = (-dn).ToString();
        }
    }
}