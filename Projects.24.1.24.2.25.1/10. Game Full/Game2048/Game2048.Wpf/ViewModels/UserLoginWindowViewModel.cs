using Game2048.Common.BusinessModels;
using Game2048.Common.Contracts.Services;
using Ninject;
using System.Windows;
using TwentyFortyEight.Wpf.ViewModels;

namespace Game2048.Wpf
{
    public class UserLoginWindowViewModel : ViewModelBase
    {
        private readonly IUserService _authService;
        private UserModel _identifiedUser;

        public string UserName { get; set; }

        internal MainWindowViewModel MainViewModel { get; set; }

        public UserLoginWindowViewModel()
        {
            var container = Application.Current.Resources["ServiceProvider"] as IKernel;
            _authService = container?.Get<IUserService>();
        }

        public bool FindUser()
        {
            _identifiedUser = _authService.GetUserByName(UserName);
            return _identifiedUser != null;
        }

        public void CreateUser()
        {
            _identifiedUser = _authService.GetOrCreateUser(UserName);
        }

        public void CommitUser()
        {
            if (_identifiedUser != null)
            {
                MainViewModel.CommitUser(_identifiedUser);
            }
        }
    }
}