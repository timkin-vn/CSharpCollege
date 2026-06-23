using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Business.Models;
using Ninject;
using Ninject.Parameters;

namespace Checkers.ViewModels
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IKernel _kernel;

        public ViewModelFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public GameViewModel Create(UserSession session)
        {
            return _kernel.Get<GameViewModel>(new ConstructorArgument("userSession", session));
        }
    }
}
