using System.Windows;

namespace game
{
    public partial class LoginWindow : Window
    {
        public string PlayerName { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PlayerNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите имя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            PlayerName = PlayerNameTextBox.Text.Trim();
            DialogResult = true;
            Close();
        }
    }
}
