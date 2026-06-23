using Minesweeper.Common.Models;

namespace Minesweeper.Common.Interfaces
{
    public interface IUserService
    {
        User Login(string name);
        User GetById(int id);
    }
}
