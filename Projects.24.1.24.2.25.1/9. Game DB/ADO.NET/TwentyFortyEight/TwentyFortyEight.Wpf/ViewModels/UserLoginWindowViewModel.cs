using Ninject;
using TwentyFortyEight.Common.BusinessModels;
using TwentyFortyEight.Common.Contracts.Services;
using TwentyFortyEight.Common.Infrastructure;

namespace TwentyFortyEight.Wpf.ViewModels
{
    public class UserLoginWindowViewModel : ViewModelBase
    {
        private readonly IUserService _playerService = NinjectKernel.Instance.Get<IUserService>();
        private UserModel _sessionUser;

        public string UserName { get; set; }

        internal MainWindowViewModel MainViewModel { get; set; }

        public bool FindUser()
        {
            _sessionUser = _playerService.GetUserByName(UserName);
            return _sessionUser != null;
        }

        public void CreateUser()
        {
            _sessionUser = _playerService.GetOrCreateUser(UserName);
        }

        public void CommitUser()
        {
            if (_sessionUser != null)
            {
                MainViewModel.CommitUser(_sessionUser);
            }
        }
    }
}