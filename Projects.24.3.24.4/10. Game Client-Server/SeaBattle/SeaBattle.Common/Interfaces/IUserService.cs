using SeaBattle.Common.Models;

namespace SeaBattle.Common.Interfaces
{
    public interface IUserService
    {
        User Login(string name);
        User GetById(int id);
    }
}
