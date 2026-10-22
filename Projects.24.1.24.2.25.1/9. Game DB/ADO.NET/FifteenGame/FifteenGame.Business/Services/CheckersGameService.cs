using CheckersGame.Business.Models;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersGame.Business.Contracts;

namespace CheckersGame.Business.Services
{
    public class CheckersGameService : ICheckersGameService
    {
        private readonly ICheckersGameRepository _repository;
        private readonly GameService _coreService = new GameService();

        public CheckersGameService(ICheckersGameRepository repository)
        {
            _repository = repository;
        }

        public GameModel CreateNewGame(int userId)
        {
            var model = new GameModel();
            _coreService.Initialize(model);
            model.Id = 0;
            model.UserId = userId;

            var dto = new CheckersGameDto
            {
                UserId = userId,
                CurrentPlayer = (int)Player.White,
                GameStateJson = model.SerializeState()
            };
            int id = _repository.Save(dto);
            model.Id = id;
            return model;
        }

        public GameModel LoadGame(int gameId)
        {
            var dto = _repository.GetById(gameId);
            if (dto == null)
                throw new Exception($"Game {gameId} not found");

            var model = GameModel.DeserializeState(dto.GameStateJson);
            model.Id = dto.Id;
            model.UserId = dto.UserId;
            return model;
        }

        public GameModel? GetLastActiveGame(int userId)
        {
            var games = _repository.GetByUserId(userId);
            var lastActive = games
                .Where(g => !g.IsFinished)
                .OrderByDescending(g => g.StartDate)
                .FirstOrDefault();

            if (lastActive == null)
                return null;

            var model = GameModel.DeserializeState(lastActive.GameStateJson);
            model.Id = lastActive.Id;
            model.UserId = lastActive.UserId;
            return model;
        }

        public void MakeMove(int gameId, int fromRow, int fromCol, int toRow, int toCol)
        {
            var model = LoadGame(gameId);

            bool isCapture = _coreService.IsCaptureMove(fromRow, fromCol, toRow, toCol, model);
            int? capturedRow = null, capturedCol = null;

            if (isCapture)
            {
                int dr = Math.Sign(toRow - fromRow);
                int dc = Math.Sign(toCol - fromCol);
                int midR = fromRow + dr;
                int midC = fromCol + dc;
                while (midR != toRow || midC != toCol)
                {
                    if (model[midR, midC] != Piece.None)
                    {
                        capturedRow = midR;
                        capturedCol = midC;
                        break;
                    }
                    midR += dr;
                    midC += dc;
                }
            }

            var pieceBeforeMove = model[fromRow, fromCol];
            bool promoted = (pieceBeforeMove == Piece.Black && toRow == GameModel.BoardSize - 1)
                         || (pieceBeforeMove == Piece.White && toRow == 0);

            bool success = _coreService.MakeMove(model, fromRow, fromCol, toRow, toCol);
            if (!success)
                throw new InvalidOperationException("Невозможный ход");

            var dto = new CheckersGameDto
            {
                Id = gameId,
                UserId = model.UserId,
                CurrentPlayer = (int)model.CurrentPlayer,
                IsFinished = model.IsGameOver(),
                Winner = model.IsGameOver() ? (int?)(model.CurrentPlayer == Player.Black ? Player.White : Player.Black) : null,
                GameStateJson = model.SerializeState()
            };
            _repository.Save(dto);

            var moveDto = new CheckersMoveDto
            {
                GameId = gameId,
                MoveNumber = model.MoveCount,
                FromRow = fromRow,
                FromCol = fromCol,
                ToRow = toRow,
                ToCol = toCol,
                IsCapture = isCapture,
                CapturedRow = capturedRow,
                CapturedCol = capturedCol,
                PromotedToKing = promoted,
                MoveTime = DateTime.UtcNow
            };
            _repository.AddMove(moveDto);
        }

        public void FinishCapture(int gameId)
        {
            var model = LoadGame(gameId);
            _coreService.FinishCapture(model);

            var dto = new CheckersGameDto
            {
                Id = gameId,
                UserId = model.UserId,
                CurrentPlayer = (int)model.CurrentPlayer,
                IsFinished = model.IsGameOver(),
                Winner = model.IsGameOver() ? (int?)(model.CurrentPlayer == Player.Black ? Player.White : Player.Black) : null,
                GameStateJson = model.SerializeState()
            };
            _repository.Save(dto);
        }

        public bool IsGameOver(int gameId)
        {
            var model = LoadGame(gameId);
            return model.IsGameOver();
        }

        public void DeleteGame(int gameId)
        {
            _repository.Delete(gameId);
        }
    }
}