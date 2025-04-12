using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using FifteenGame.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
{ 
    public class GameService : IGameService
    { public static Random random1 = new Random ();
        static int valuefistrow = random1.Next(0, 3);
        static int valuefirscolumn = random1.Next(0, 3);
        static int valuesecondrow = random1.Next(0, 3);
        static int valuesecondcolumn = random1.Next(0, 3);
        private readonly IGameRepository _gameRepository;
        
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
                CountPar = 0,
            };

            Initialize(game);
            game.MoveCount = 0;
            dto = ToDto(game);
            var gameId = _gameRepository.Save(dto);

            return GetByGameId(gameId);
        }

        public void Initialize(GameModel model)
        {
            string[] colors = { "Red", "Green", "Blue", "Yellow", "Orange", "Purple", "Pink", "Cyan" };
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
            if (model.CountPar == 8)
            {

                model.CountPar = 0;
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

        public bool CheckMatch(GameModel model, int[] FisrsbuutonRowCol, int[] SecondbuutonRowCol)
        {
            model.MoveCount++;

            if (model[FisrsbuutonRowCol[0], FisrsbuutonRowCol[1]] == null || model[SecondbuutonRowCol[0], SecondbuutonRowCol[1]] == null)
            {


                return false;
            }

            if (model[FisrsbuutonRowCol[0], FisrsbuutonRowCol[1]] == model[SecondbuutonRowCol[0], SecondbuutonRowCol[1]] && (FisrsbuutonRowCol[0] != SecondbuutonRowCol[0]
                || FisrsbuutonRowCol[1] != SecondbuutonRowCol[1]))
            {
                
                model.CountPar++;

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        if ((row == FisrsbuutonRowCol[0] &&
                            column == FisrsbuutonRowCol[1]) || (row ==SecondbuutonRowCol[0] &&
                            column == SecondbuutonRowCol[1]))
                        {

                            model[row, column] = "";


                        }
                        else
                        {
                            model[row, column] = model[row, column];
                        }

                    }

                }
                if ((valuefistrow != FisrsbuutonRowCol[0] &&
                            valuefirscolumn != FisrsbuutonRowCol[1]) && (valuesecondrow != SecondbuutonRowCol[0] &&
                            valuesecondcolumn != SecondbuutonRowCol[1])){
                    FisrsbuutonRowCol[0] =valuefistrow ;
                    FisrsbuutonRowCol[1] = valuefirscolumn;
                    SecondbuutonRowCol[0] =valuesecondrow ;
                    SecondbuutonRowCol[1] = valuesecondcolumn;

                }
                
             

                return true;
            }
            else
            {
                return false;
            }
        }

        public GameModel CheckMatch(int gameId, int[] FisrsbuutonRowCol, int[] SecondbuutonRowCol)
        {
            var gameDto = _gameRepository.GetByGameId(gameId);
            var gameModel = FromDto(gameDto);

            CheckMatch(gameModel, FisrsbuutonRowCol, SecondbuutonRowCol);

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
                CountPar = dto.CountPar,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result[row, column] = dto.Cells[row, column];
                    
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
                CountPar = game.CountPar,
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
