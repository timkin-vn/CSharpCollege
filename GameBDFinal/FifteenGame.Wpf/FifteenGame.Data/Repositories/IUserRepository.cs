using FifteenGame.Data.Entities;

namespace FifteenGame.Data.Repositories
{
    public interface IUserRepository
    {
        // Найти пользователя или создать нового, если его нет
        User GetOrCreate(string username);

        // Обновить рекорд (проверка "побит ли рекорд" будет внутри)
        void UpdateBestTime(string username, double timeInSeconds);

        // Сохранить состояние игры (JSON)
        void SaveGameState(string username, string json);

        // Удалить сохранение (когда игра закончена)
        void ClearSavedGame(string username);
    }
}