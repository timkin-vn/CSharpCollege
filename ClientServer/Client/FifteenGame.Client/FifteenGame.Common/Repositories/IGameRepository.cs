using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;

namespace FifteenGame.Common.Repositories
{
    public interface IGameRepository
    {
       
        int SaveGame(GameModel model, int userId);

        
        GameModel LoadGame(int gameId);

        
        void RemoveGame(int gameId);
        
    }
}