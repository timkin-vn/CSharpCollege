using FifteenGame.Common.Dto;
using FifteenGame.Common.BusinessModels; 

namespace FifteenGame.Common.Repositories
{
    public interface IUserRepository
    {
        UserDto GetUserByName(string name);
        void SaveUser(UserDto user);
        bool HasSavedGame(int userId);
        GameModel LoadSavedGame(int userId);
        void SaveCurrentGameAsSaved(int userId, GameModel gameModel);
        void ClearSavedGame(int userId); 
    }
}