using _2048Game.BusinessProxy.Services;
using _2048Game.Common.BusinessModels;
using _2048Game.WPF.ViewModels;
using System.Windows;


namespace _2048Game.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationOrEnterWindow.xaml
    /// </summary>
    public partial class RegistrationOrEnterWindow : Window
    {
        public RegistrationOrEnterWindowViewModel ViewModel => (RegistrationOrEnterWindowViewModel)DataContext;
        private readonly UserServiceProxy _userService;
        public UserModel User { get; private set; }
        public RegistrationOrEnterWindow()
        {
            InitializeComponent();
            _userService = new UserServiceProxy();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordBoxControl.Password;
            string userName = LoginBoxUserName.Text;

            var user = _userService.GetUserByNameAndPassword(userName, password);
            if (user != null)
            {
                User = user;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь не найден. Зарегистрируйтесь или введите корректные данные.",
                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = LoginBoxUserName.Text;
            string password = PasswordBoxControl.Password;

            var existingUser = _userService.GetByUserNameOnly(userName);
            if (existingUser != null)
            {
                MessageBox.Show("Пользователь с таким именем уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пароль не может быть пустым.",
                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать 6 и более символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show("Имя пользователя не может быть пустым.",
                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = _userService.GetOrCreateUser(userName, password);
            if (user != null)
            {
                User = user;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка регистрации", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
