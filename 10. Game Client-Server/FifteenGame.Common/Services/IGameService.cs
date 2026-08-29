using FifteenGame.Common.Dto;

namespace FifteenGame.Common.Services
{
    public interface IGameService
    {
        GameDto CreateGame(int userId, int mode);
        GameDto GetGame(int gameId);
        GameDto MakeMove(int gameId, int row, int column);
    }
}