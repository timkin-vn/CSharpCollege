using System.Collections.Generic;
using System.Linq;
using SeaBattle.Common.Dto;
using SeaBattle.Common.Interfaces;
using SeaBattle.Common.Models;

namespace SeaBattle.BusinessProxy
{
    public class GameServiceProxy : ApiClient, IGameService
    {
        public GameModel SaveResult(int userId, int moveCount, bool won)
        {
            var reply = Post<GameReply>("api/games/save",
                new SaveGameRequest { UserId = userId, MoveCount = moveCount, Won = won });
            return ToModel(reply);
        }

        public IList<GameModel> GetHistory(int userId)
        {
            var replies = Get<List<GameReply>>("api/games/history/" + userId);
            return replies.Select(ToModel).ToList();
        }

        private static GameModel ToModel(GameReply r)
        {
            return new GameModel { Id = r.Id, UserId = r.UserId, MoveCount = r.MoveCount, Won = r.Won };
        }
    }
}
