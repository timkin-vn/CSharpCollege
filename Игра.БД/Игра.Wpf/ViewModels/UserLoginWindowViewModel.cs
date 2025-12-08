using Игра.Common.BusinessModels;
using Игра.Common.Infrastructure;
using Игра.Common.Services;
using Ninject;

namespace Игра.Wpf.ViewModels
{
    public class UserLoginWindowViewModel
    {
        private readonly IUserService _userService = NinjectKernel.Instance.Get<IUserService>();

        private UserModel _userModel;

        public string UserName { get; set; }

        internal MainWindowViewModel MainViewModel { get; set; }

        public bool FindUser()
        {
            _userModel = _userService.GetUserByName(UserName);
            return _userModel != null;
        }

        public void CreateUser()
        {
            _userModel = _userService.GetOrCreateUser(UserName);
        }

        public void SaveUser()
        {
            MainViewModel?.SetUser(_userModel);
        }
    }
}