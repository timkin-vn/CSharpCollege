using SeaBattle.Common.Models;

namespace SeaBattle.Common.Interfaces
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetByName(string name);
        User Create(string name);
    }
}
