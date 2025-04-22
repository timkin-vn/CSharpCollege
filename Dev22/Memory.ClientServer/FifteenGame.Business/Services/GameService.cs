using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using FifteenGame.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
{
   
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        int countPar=0;
       
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
            string[] colors = { "Coal", "Copper", "Lazulit", "Iron", "Gold", "Diamond", "Sapphire", "Emerald" };
            Random random = new Random();


            List<string> colorPairs = new List<string>();

            foreach (var color in colors)
            {
                colorPairs.Add(color);
                colorPairs.Add(color);
            }



            colorPairs = colorPairs.OrderBy(x => random.Next()).ToList();


            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = colorPairs[row * Constants.ColumnCount + column];

                }

            }
        }
        public bool IsGameOver(GameModel model)
        {
            if (countPar == 8)
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

        public bool CheckMatch(GameModel model, int[] OneColumnRow, int[] TwoColumnRow)
        {
            model.MoveCount++;

            if (model[OneColumnRow[0], OneColumnRow[1]] == null || model[TwoColumnRow[0], TwoColumnRow[1]] == null)
            {
                

                return false;
            }

            else if (model[OneColumnRow[0], OneColumnRow[1]] == model[TwoColumnRow[0], TwoColumnRow[1]] && (OneColumnRow[0] != TwoColumnRow[0]
                || OneColumnRow[1] != TwoColumnRow[1]))
            {
                
                countPar++;

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        if ((row == OneColumnRow[0] &&
                            column == OneColumnRow[1]) || (row == TwoColumnRow[0] &&
                            column == TwoColumnRow[1]))
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

        public GameModel CheckMatch(int gameId, int[] OnebuutonRowCol, int[] TwobuutonRowCol)
        {
            var gameDto = _gameRepository.GetByGameId(gameId);
            var gameModel = FromDto(gameDto);

            CheckMatch(gameModel, OnebuutonRowCol, TwobuutonRowCol);

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
