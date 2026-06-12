using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Common.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FifteenGame.Wpf.ViewModels
{
    public class UserLoginWindowViewModel
    {
        private IUserService _userService;

        private UserModel _user;

        public UserLoginWindowViewModel()
        {
            try
            {
                _userService = NinjectKernel.Instance.Get<IUserService>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации сервиса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public string UserName { get; set; }

        public MainWindowViewModel MainViewModel { get; set; }

        public bool FindUser()
        {
            try
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    return false;
                }

                if (_userService == null)
                {
                    MessageBox.Show("Сервис пользователей не инициализирован", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                _user = _userService.GetUserByName(UserName);
                return _user != null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public void CreateUser()
        {
            try
            {
                _user = _userService.GetOrCreateUser(UserName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SaveUser()
        {
            try
            {
                if (_user != null)
                {
                    MainViewModel.SetUser(_user);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
