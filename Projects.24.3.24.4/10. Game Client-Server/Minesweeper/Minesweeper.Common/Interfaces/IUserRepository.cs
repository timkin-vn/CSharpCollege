using Minesweeper.Common.Models;

namespace Minesweeper.Common.Interfaces
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetByName(string name);
        User Create(string name);
    }
}
