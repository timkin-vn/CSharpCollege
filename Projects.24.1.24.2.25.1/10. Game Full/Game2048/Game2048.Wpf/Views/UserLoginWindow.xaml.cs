using System.Windows;

namespace Game2048.Wpf
{
    public partial class UserLoginWindow : Window
    {
        public UserLoginWindowViewModel AuthContext => (UserLoginWindowViewModel)DataContext;

        public UserLoginWindow()
        {
            InitializeComponent();
        }

        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AuthContext.UserName))
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (AuthContext.FindUser())
            {
                AuthContext.CommitUser();
                DialogResult = true;
                Close();
            }
            else
            {
                var result = MessageBox.Show("Пользователь не найден. Создать новый профиль?", "Регистрация", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    AuthContext.CreateUser();
                    AuthContext.CommitUser();
                    DialogResult = true;
                    Close();
                }
            }
        }
    }
}