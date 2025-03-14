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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(UsernameBox.Text))
            {
                PlayerName = UsernameBox.Text;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Введите корректное имя!");
            }
        }
    }
}
