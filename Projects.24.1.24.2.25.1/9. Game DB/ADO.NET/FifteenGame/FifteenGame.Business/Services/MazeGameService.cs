using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Common.BusinessModels;
using Newtonsoft.Json;
using System;

namespace FifteenGame.Business.Services
{
    public class MazeGameService : IMazeGameService
    {
        private readonly IMazeGameRepository _mazeGameRepository;

        public MazeGameService(IMazeGameRepository mazeGameRepository)
        {
            _mazeGameRepository = mazeGameRepository;
        }

        public GameManager GetMazeGameByUserId(int userId)
        {
            var mazeGameModel = _mazeGameRepository.GetByUserId(userId);
            if (mazeGameModel == null)
            {
                return null;
            }

            var gameManager = JsonConvert.DeserializeObject<GameManager>(mazeGameModel.SerializedGameManager);
            return gameManager;
        }

        public GameManager MakeMazeMove(int userId, int deltaRow, int deltaCol)
        {
            var mazeGameModel = _mazeGameRepository.GetByUserId(userId);
            if (mazeGameModel == null)
            {
                // Should not happen if game is started
                return null;
            }

            var gameManager = JsonConvert.DeserializeObject<GameManager>(mazeGameModel.SerializedGameManager);
            gameManager.TryMove(deltaRow, deltaCol);

            mazeGameModel.SerializedGameManager = JsonConvert.SerializeObject(gameManager);
            _mazeGameRepository.Save(mazeGameModel);

            return gameManager;
        }

        public void StartNewMazeGame(int userId)
        {
            var existingGame = _mazeGameRepository.GetByUserId(userId);
            if (existingGame != null)
            {
                _mazeGameRepository.Remove(existingGame.Id);
            }

            var gameManager = new GameManager();
            var mazeGameModel = new MazeGameModel
            {
                UserId = userId,
                SerializedGameManager = JsonConvert.SerializeObject(gameManager)
            };
            _mazeGameRepository.Save(mazeGameModel);
        }

        public bool IsMazeGameOver(int userId)
        {
            var mazeGameModel = _mazeGameRepository.GetByUserId(userId);
            if (mazeGameModel == null)
            {
                return true; // No game, so it's "over"
            }

            var gameManager = JsonConvert.DeserializeObject<GameManager>(mazeGameModel.SerializedGameManager);
            return gameManager.IsGameOver;
        }
    }
}
