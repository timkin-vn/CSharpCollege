using wpf_sahur_business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace wpf_sahur.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private string _username;
        private string _errorMessage;

        public LoginViewModel(AuthService authService)
        {
            _authService = authService;
            LoginCommand = new RelayCommand(OnLogin);
        }

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        public void OnLogin(object parameter)
        {
            if (parameter is PasswordBox passwordBox)
            {
                var password = passwordBox.Password;

                if (_authService.AuthenticateUser(Username, password))
                {
                    var mainWindow = new Views.MainWindow();
                    mainWindow.DataContext = new MainViewModel();
                    mainWindow.Show();

                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window is Views.LoginView)
                        {
                            window.Close();
                            break;
                        }
                    }
                }
                else
                {
                    ErrorMessage = "Неверный логин или пароль";
                }
            }
        }
    }
}
