using Game2048.Common.Models;

namespace Game2048.Common.Interfaces
{
    public interface IUserService
    {
        User Login(string name);
        User GetById(int id);
    }
}
