using Game2048.Common.Models;

namespace Game2048.Common.Interfaces
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetByName(string name);
        User Create(string name);
    }
}
