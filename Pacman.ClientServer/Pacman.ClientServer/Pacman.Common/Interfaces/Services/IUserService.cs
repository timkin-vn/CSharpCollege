using Pacman.Common.Models;

namespace Pacman.Common.Interfaces.Services
{
    public interface IUserService
    {
        UserDto GetOrCreateUser(string name);
        UserDto GetUserById(int id);
    }
}