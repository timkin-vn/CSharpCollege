using Game2048.Common;
using Game2048.Common.Dto;
using Game2048.Common.Interfaces;
using Game2048.Common.Models;

namespace Game2048.BusinessProxy
{

    public class GameServiceProxy : ApiClient, IGameService
    {
        public GameModel GetByUserId(int userId)
        {
            return ToModel(Get<GameReply>("api/games/get-by-user-id/" + userId));
        }

        public GameModel MakeMove(int userId, MoveDirection direction)
        {
            var request = new MakeMoveRequest { UserId = userId, Direction = (int)direction };
            return ToModel(Post<GameReply>("api/games/make-move", request));
        }

        public bool IsWon(int userId)
        {
            return Get<BooleanReply>("api/games/is-won/" + userId).Value;
        }

        public bool IsGameOver(int userId)
        {
            return Get<BooleanReply>("api/games/is-over/" + userId).Value;
        }

        public void Remove(int gameId)
        {
            Delete("api/games/remove/" + gameId);
        }

        private static GameModel ToModel(GameReply reply)
        {
            var model = new GameModel
            {
                Id = reply.Id,
                UserId = reply.UserId,
                Score = reply.Score,
                MoveCount = reply.MoveCount
            };
            int n = reply.Size > 0 ? reply.Size : Constants.Size;
            if (reply.Cells != null)
            {
                for (int i = 0; i < reply.Cells.Length && i < n * n; i++)
                    model.Field[i / n, i % n] = reply.Cells[i];
            }
            return model;
        }
    }
}
