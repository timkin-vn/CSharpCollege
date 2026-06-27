using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Infrastucture;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FifteenGame.WebApi.Controllers
{
    public class GamesController : ApiController
    {
        private IGameService _gameService = NinjectKernel.Instance.Get<IGameService>();

        // 1. Получение стрика побед пользователя
        [HttpGet]
        [Route("api/games/win-streak/{userId}")]
        public int GetWinStreak(int userId)
        {
            return _gameService.GetUserWinStreak(userId);
        }

        [HttpGet]
        [Route("api/games/get-by-user-id/{userId}")]
        public GameReply GetByUserId(int userId)
        {
            return ToDto(_gameService.GetByUserId(userId));
        }

        [HttpGet]
        [Route("api/games/get-by-game-id/{gameId}")]
        public GameReply GetByGameId(int gameId)
        {
            return ToDto(_gameService.GetByGameId(gameId));
        }

        // 2. Обработка хода (постройка ларька) по координатам
        [HttpPost]
        [Route("api/games/move")]
        public GameReply Move([FromBody] MakeMoveRequest request)
        {
            if (request == null) return null;

            // Тянем игру из базы данных, чтобы применить к ней ход
            var gameModel = _gameService.GetByGameId(request.GameId);
            if (gameModel == null) return null;

            // Делаем ход в сервисе (экономика, штрафы, зожники посчитаются внутри)
            _gameService.Move(gameModel, request.Row, request.Column);

            // Возвращаем клиенту обновлённое состояние игры
            return ToDto(gameModel);
        }

        // 3. Сброс / Перезапуск игры
        [HttpPost]
        [Route("api/games/restart/{userId}")]
        public GameReply Restart(int userId)
        {
            return ToDto(_gameService.RestartGame(userId));
        }

        [HttpGet]
        [Route("api/games/is-over/{gameId}")]
        public BooleanReply IsOver(int gameId)
        {
            var gameModel = _gameService.GetByGameId(gameId);
            return new BooleanReply
            {
                IsTrue = gameModel == null || _gameService.IsGameOver(gameModel),
            };
        }

        [HttpDelete]
        [Route("api/games/remove/{gameId}")]
        public void Remove(int gameId)
        {
            _gameService.RemoveGame(gameId);
        }

        // Маппинг из полноценной GameModel в сетевой GameReply
        private GameReply ToDto(GameModel model)
        {
            if (model == null) return null;

            var dto = new GameReply
            {
                Id = model.Id,
                UserId = model.UserId,
                Money = model.Money,
                MoveCount = model.TurnsPlayed
            };

            // Переносим матрицы состояния карты один в один
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    dto.PeopleCount[row, column] = model.GetPeopleCount(row, column);
                    dto.HasShop[row, column] = model.GetHasShop(row, column);
                    dto.IsVeggie[row, column] = model.GetIsVeggie(row, column);
                    dto.IsRevealed[row, column] = model.GetIsRevealed(row, column);
                }
            }

            return dto;
        }
    }
}