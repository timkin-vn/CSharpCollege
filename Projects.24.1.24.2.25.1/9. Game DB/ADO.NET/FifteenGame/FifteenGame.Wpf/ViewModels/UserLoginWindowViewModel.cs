using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Infrastructure;
using Ninject;
using System;

namespace FifteenGame.Wpf.ViewModels
{
    public class UserLoginWindowViewModel : ViewModelBase
    {
        private readonly IUserService _userService =
            NinjectKernel.Instance.Get<IUserService>();

        private UserModel _userModel;

        public string UserName { get; set; }

        internal MainWindowViewModel MainViewModel { get; set; }

        public bool FindUser()
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                _userModel = null;
                return false;
            }

            _userModel = _userService.GetUserByName(UserName.Trim());

            return _userModel != null;
        }

        public void CreateUser()
        {
            if (string.IsNullOrWhiteSpace(UserName))
                throw new ArgumentException("Введите имя пользователя.");

            _userModel = _userService.GetOrCreateUser(UserName.Trim());
        }

        public void CommitUser()
        {
            if (_userModel == null)
                throw new InvalidOperationException(
                    "Сначала выберите или создайте пользователя.");

            if (MainViewModel == null)
                throw new InvalidOperationException(
                    "Главное окно не установлено.");

            MainViewModel.CommitUser(_userModel);
        }
    }
}