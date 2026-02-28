using Pacman.Common.Models;

namespace Pacman.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        UserDto GetByName(string name);
        UserDto GetById(int id);
        UserDto Create(string name);
    }
}