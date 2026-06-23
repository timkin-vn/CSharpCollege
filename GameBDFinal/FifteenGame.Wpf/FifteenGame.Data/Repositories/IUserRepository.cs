using FifteenGame.Data.Entities;

namespace FifteenGame.Data.Repositories
{
    public interface IUserRepository
    {
        
        User GetOrCreate(string username);

        
        void UpdateBestTime(string username, double timeInSeconds);

        
        void SaveGameState(string username, string json);

        
        void ClearSavedGame(string username);
    }
}