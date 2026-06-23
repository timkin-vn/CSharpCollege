using Minesweeper.Common;
using Minesweeper.Common.Dto;
using Minesweeper.Common.Interfaces;
using Minesweeper.Common.Models;

namespace Minesweeper.BusinessProxy
{

    public class GameServiceProxy : ApiClient, IGameService
    {
        public GameModel GetByUserId(int userId)
        {
            return ToModel(Get<GameReply>("api/games/get-by-user-id/" + userId));
        }

        public GameModel Apply(int userId, int row, int col, CellAction action)
        {
            var request = new CellActionRequest
            {
                UserId = userId,
                Row = row,
                Col = col,
                Action = (int)action
            };
            return ToModel(Post<GameReply>("api/games/apply", request));
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
                IsLost = reply.IsLost,
                MoveCount = reply.MoveCount
            };
            int n = reply.Size > 0 ? reply.Size : Constants.Size;
            if (reply.Cells != null)
            {
                for (int i = 0; i < reply.Cells.Length && i < n * n; i++)
                    model.Field[i / n, i % n] = Decode(reply.Cells[i]);
            }
            return model;
        }

        private static Cell Decode(int code)
        {
            var cell = new Cell();
            if (code == -3) return cell;
            if (code == -2) { cell.IsFlagged = true; return cell; }
            if (code == -1) { cell.IsRevealed = true; cell.IsMine = true; return cell; }
            cell.IsRevealed = true;
            cell.AdjacentMines = code;
            return cell;
        }
    }
}
