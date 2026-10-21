using MemoryGame.Common.BusinessModels;
using MemoryGame.Common.Contracts.Services;
using MemoryGame.Common.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.Wpf.ViewModels
{
    public class UserLoginWindowViewModel : ViewModelBase
    {
        private IUserService _userService;

        private UserModel _userModel;

                private IUserService UserService => _userService ?? (_userService = NinjectKernel.Instance.Get<IUserService>());

        public string UserName { get; set; }

        internal MainWindowViewModel MainViewModel { get; set; }

        public bool FindUser()
        {
                    _userModel = UserService.GetUserByName(UserName);
            return _userModel != null;
        }

        public void CreateUser()
        {
                    _userModel = UserService.GetOrCreateUser(UserName);
        }

        public void CommitUser()
        {
            MainViewModel.CommitUser(_userModel);
        }
    }
}
