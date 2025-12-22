using Pacman.Common.Dtos;

namespace Pacman.Common.Services
{
    public interface IGameService
    {
        MapDto GetLevel(int level); 
    }
}