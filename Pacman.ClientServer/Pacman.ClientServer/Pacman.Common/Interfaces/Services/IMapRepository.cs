using Pacman.Common.Models;

namespace Pacman.Common.Interfaces.Repositories
{
    public interface IMapRepository
    {
        MapDto GetDefaultMap();
        MapDto GetMapById(int id);
    }
}
