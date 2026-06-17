using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Infrastructure;
using Ninject;
using System;

namespace FifteenGame.Wpf.ViewModels
{
    public class UserLoginWindowViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private UserModel _userModel;

        public string UserName { get; set; }

        public MainWindowViewModel MainViewModel { get; set; }

        public UserLoginWindowViewModel()
        {
            try
            {
                _userService = NinjectKernel.Instance.Get<IUserService>();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка инициализации: {ex.Message}", "Ошибка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public bool FindUser()
        {
            if (_userService == null) return false;
            _userModel = _userService.GetUserByName(UserName);
            return _userModel != null;
        }

        public void CreateUser()
        {
            if (_userService == null) return;
            _userModel = _userService.GetOrCreateUser(UserName);
        }

        public void CommitUser()
        {
            if (_userModel == null)
            {
                System.Windows.MessageBox.Show("Пользователь не найден", "Ошибка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return;
            }

            if (MainViewModel == null)
            {
                System.Windows.MessageBox.Show("Ошибка: MainViewModel не установлен", "Ошибка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }

            MainViewModel.CommitUser(_userModel);
        }
    }
}