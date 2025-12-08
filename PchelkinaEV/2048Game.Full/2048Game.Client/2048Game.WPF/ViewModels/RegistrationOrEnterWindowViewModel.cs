using _2048Game.BusinessProxy.Services;
using _2048Game.Common.BusinessModels;
using _2048Game.Common.Services;

namespace _2048Game.WPF.ViewModels
{
    public class RegistrationOrEnterWindowViewModel
    {
        private readonly IUserService _userService;
        private UserModel _userModel;
        public string UserName { get; set; }
        public string Password { get; set; }
        internal MainWindowViewModel MainViewModel { get; set; }
        public RegistrationOrEnterWindowViewModel()
        {
            _userService = new UserServiceProxy();
        }
        public bool FindUser()
        {
            _userModel = _userService.GetUserByName(UserName, Password);
            return _userModel != null;
        }

        public void CreateUser()
        {
            _userModel = _userService.GetOrCreateUser(UserName, Password);
        }

        public void SaveUser()
        {
            MainViewModel.SetUser(_userModel);
        }
    }
}
