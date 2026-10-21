using MemoryGame.Business.Services;
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

        public void CommitUser()
        {
            MainViewModel.CommitUser(_userModel);
        }
    }
}
