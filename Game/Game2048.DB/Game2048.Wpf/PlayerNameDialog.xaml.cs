using System.Windows;

namespace Game2048.Wpf
{
    public partial class PlayerNameDialog : Window
    {
        public string PlayerName { get; private set; }

        public PlayerNameDialog()
        {
            InitializeComponent();
            PlayerNameTextBox.Focus();
            PlayerNameTextBox.SelectAll();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            string name = PlayerNameTextBox.Text.Trim();
            
            if (string.IsNullOrEmpty(name))
            {
                ErrorMessage.Visibility = Visibility.Visible;
                PlayerNameTextBox.Focus();
                return;
            }

            PlayerName = name;
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
