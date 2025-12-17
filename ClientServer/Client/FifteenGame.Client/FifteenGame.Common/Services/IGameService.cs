using FifteenGame.Common.BusinessDtos;

namespace FifteenGame.Common.Services
{
    public interface IGameService
    {
        StartGameReply StartNewGame(int userId); 
        GameReply MakeMove(MakeMoveRequest request);
    }
}