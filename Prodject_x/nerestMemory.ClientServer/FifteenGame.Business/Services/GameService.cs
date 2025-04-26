using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using FifteenGame.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
{
   
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        int countPar=0;
        int countbuttonclick = 0;

        public GameService()
        {
            _gameRepository = new GameRepository();
        }

        public GameModel GetByGameId(int gameId)
        {
            var dto = _gameRepository.GetByGameId(gameId);
            return FromDto(dto);
        }

        public GameModel GetByUserId(int userId)
        {
            var dtos = _gameRepository.GetByUserId(userId);
            var dto = dtos.LastOrDefault();
            if (dto != null)
            {
                return FromDto(dto);
            }

            var game = new GameModel
            {
                UserId = userId,
                GameStart = DateTime.Now,
                MoveCount = 0,
            };

            Initialize(game);
            game.MoveCount = 0;
            dto = ToDto(game);
            var gameId = _gameRepository.Save(dto);

            return GetByGameId(gameId);
        }

       public void Initialize(GameModel model)
       {
          
            string[] uniqueComponents = { "Mercedes", "BMW", "Audi", "Ferrari", "Porsche", "Lamborghini", "Bugatti", "Bentley", "Rolls-Royce", "McLaren", "Jaguar", "Maserati" };

            
            List<string> componentPairs = new List<string>();

          
            foreach (var component in uniqueComponents)
            {
                componentPairs.Add(component);
                componentPairs.Add(component);
            }

           

            
            Random random = new Random();
            componentPairs = componentPairs.OrderBy(x => random.Next()).ToList();

            
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = componentPairs[row * Constants.ColumnCount + column];
                }
            }
       }
        public bool IsGameOver(GameModel model)
        {
            if (countPar == (Constants.ColumnCount * Constants.RowCount)/2)
            {
               
                countPar = 0;
                return true;
            }


            return false;
        }

        public bool IsGameOver(int gameId)
        {
            var gameDto = _gameRepository.GetByGameId(gameId);
            var result = IsGameOver(FromDto(gameDto));
            return result;
        }

        public bool Connect_components(GameModel model, int[] ParaOneebuutonRowCol, int[] ParaTwoebuutonRowCol)
        {
            model.MoveCount++;
           
            if (model[ParaOneebuutonRowCol[0], ParaOneebuutonRowCol[1]] == null || model[ParaTwoebuutonRowCol[0], ParaTwoebuutonRowCol[1]] == null)
            {
                

                return false;
            }

            else if (model[ParaOneebuutonRowCol[0], ParaOneebuutonRowCol[1]] == model[ParaTwoebuutonRowCol[0], ParaTwoebuutonRowCol[1]] && (ParaOneebuutonRowCol[0] != ParaTwoebuutonRowCol[0]
                || ParaOneebuutonRowCol[1] != ParaTwoebuutonRowCol[1]))
            {
                
                countPar++;

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        if ((row == ParaOneebuutonRowCol[0] &&
                            column == ParaOneebuutonRowCol[1]) || (row == ParaTwoebuutonRowCol[0] &&
                            column == ParaTwoebuutonRowCol[1]))
                        {

                            model[row, column] = "";


                        }
                        

                    }

                }


                return true;
            }
            else
            {
                return false;
            }
        }

        public GameModel Connect_components(int gameId, int[] ParaOneebuutonRowCol, int[] ParaTwoebuutonRowCol)
        {
            var gameDto = _gameRepository.GetByGameId(gameId);
            var gameModel = FromDto(gameDto);

            Connect_components(gameModel, ParaOneebuutonRowCol, ParaTwoebuutonRowCol);

            _gameRepository.Save(ToDto(gameModel));
            return gameModel;
        }

        public void RemoveGame(int gameId)
        {
            _gameRepository.Remove(gameId);
        }

       

        private GameModel FromDto(GameDto dto)
        {
            var result = new GameModel
            {
                GameId = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount,
                GameStart = dto.GameStart,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result[row, column] = dto.Cells[row, column];
                    if (result[row, column] == "")
                    {
                        continue;
                    }
                }
            }

            return result;
        }

        private GameDto ToDto(GameModel game)
        {
            var dto = new GameDto
            {
                Id = game.GameId,
                UserId = game.UserId,
                MoveCount = game.MoveCount,
                GameStart = game.GameStart,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    dto.Cells[row, column] = game[row, column];
                }
            }

            return dto;             
        }
    }
}
