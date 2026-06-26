using Ninject;
using TwentyFortyEight.Common.BusinessModels;
using TwentyFortyEight.Common.Contracts.Services;
using TwentyFortyEight.Common.Infrastructure;

namespace TwentyFortyEight.Wpf.ViewModels
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
            _userModel = _userService.GetUserByName(UserName);
            return _userModel != null;
        }

        public void CreateUser()
        {
            _userModel = _userService.GetOrCreateUser(UserName);
        }

        public void CommitUser()
        {
            MainViewModel.CommitUser(_userModel);
        }
    }
}
