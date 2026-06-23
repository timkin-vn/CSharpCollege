using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Business.Models;

namespace Checkers.ViewModels
{
    public interface IViewModelFactory
    {
        GameViewModel Create(UserSession session);
    }
}
