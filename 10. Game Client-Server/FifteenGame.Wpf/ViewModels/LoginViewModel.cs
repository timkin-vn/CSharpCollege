using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.Services;
using FifteenGame.Wpf.Infrastructure;
using Ninject;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FifteenGame.Wpf.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        public event Action<UserDto> OnLoginSuccess;

        public string Username { get; set; }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            _userService = NinjectKernel.Instance.Get<IUserService>();

            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
        }

        private void Login(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(passwordBox?.Password))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }

            try
            {
                var user = _userService.Login(Username, passwordBox.Password);
                if (user != null)
                {
                    OnLoginSuccess?.Invoke(user);
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка соединения с сервером. Запущен ли FifteenGame.Server?");
            }
        }

        private void Register(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(passwordBox?.Password)) return;

            try
            {
                var user = _userService.Register(Username, passwordBox.Password);
                if (user != null)
                {
                    MessageBox.Show("Регистрация успешна! Теперь войдите.");
                }
                else
                {
                    MessageBox.Show("Такой пользователь уже существует.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка соединения с сервером.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}