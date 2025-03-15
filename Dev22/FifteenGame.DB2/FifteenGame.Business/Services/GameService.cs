using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Services;

namespace FifteenGame.Business.Services
{
    public class GameService: IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
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
                GameBegin = DateTime.Now,
                CountRevealed = 0,
            };

            Iniziallize(game, game.MineCount); // Предполагается, что MINES_COUNT - это количество мин

            // Сохраняем новую игру в репозитории
            dto = ToDto(game);
            var gameId = _gameRepository.Save(dto);

            return GetByGameId(gameId);
          
        }
        public void Iniziallize(GameModel model, int mines)
        {


            for (int x = 0; x < GameModel.RowCount; x++)
            {
                for (int y = 0; y < GameModel.ColumnCount; y++)
                {
                    model[x,y] = new CellsModel();
                }
            }
            PlaceMines(mines, model);
            CountNeighborMines(model);
            model.RedFlags = mines;
            model.MineCount = mines;
            model.CountRevealed = 0;

        }
        public void PlaceMines(int mineCount, GameModel model)
        {
            int placedMines = 0;
            var random = new Random();
            while (placedMines < mineCount)
            {
                int x = random.Next(GameModel.RowCount);
                int y = random.Next(GameModel.ColumnCount);
                if (!model[x, y].IsMine)
                {
                    model[x, y].IsMine = true;
                    placedMines++;
                }
            }
        }
        public void RemoveGame(int gameId)
        {
            _gameRepository.Remove(gameId);
        }

        public void CountNeighborMines(GameModel model)
        {
            for (int x = 0; x < GameModel.RowCount; x++)
            {
                for (int y = 0; y < GameModel.ColumnCount; y++)
                {
                    if (model[x, y].IsMine)
                    {
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            for (int dy = -1; dy <= 1; dy++)
                            {
                                int nx = x + dx;
                                int ny = y + dy;
                                if (nx >= 0 && nx < GameModel.RowCount && ny >= 0 && ny < GameModel.ColumnCount && !model[nx, ny].IsMine)
                                {
                                    model[nx, ny].NeightborMines++;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void OpenAllMines(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model[row, column].IsMine)
                    {

                        model[row, column].IsRevealed = true;
                    }
                }
            }
        }
        public bool IsGameOver(int gameId)
        {
            var gameDto = _gameRepository.GetByGameId(gameId);
            var result = IsGameOver(FromDto(gameDto));
            return result;
        }
        public bool IsGameOver(GameModel model)
        {

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model.CountRevealed == (GameModel.RowCount * GameModel.ColumnCount) - model.MineCount)
                    {
                        return true;
                    }
                    if (model[row, column].IsMine && model[row, column].IsRevealed)
                    {
                        OpenAllMines(model);
                        return true;

                    }
                }
            }
            return false;
        }
        public void issFlaged(GameModel model, int x, int y)
        {

            if (model.RedFlags > 0 && !model[x, y].Isflag)
            {
                model.RedFlags--;
                model[x, y].Isflag = true;
            }
            else if (model[x, y].Isflag)
            {
                model[x, y].Isflag = false;
                model.RedFlags++;
            }
        }

        public GameModel RevealCell(int gameId, int x, int y)
        {
            var gameDto = _gameRepository.GetByGameId(gameId);
            var gameModel = FromDto(gameDto);

            RevealCell(x,y,gameModel);

            _gameRepository.Save(ToDto(gameModel));
            return gameModel;
        }
        public void RevealCell(int x, int y, GameModel model)
        {

            if (x < 0 || x >= GameModel.RowCount || y < 0 || y >= GameModel.ColumnCount || model[x, y].IsRevealed || model[x, y].Isflag || IsGameOver(model))
                return;

            model[x, y].IsRevealed = true;
            model.CountRevealed++;

            if (model[x, y].IsMine)
            {
                OpenAllMines(model);
                Console.WriteLine("Game Over! You hit a mine.");
                return;
            }

            if (model[x, y].NeightborMines == 0)
            {

                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        RevealCell(x + dx, y + dy, model);
                    }
                }

            }

        }

        private GameModel FromDto(GameDto dto)
        {
            var result = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                CountRevealed = dto.MoveCount,
                GameBegin = dto.GameBegin,
               
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    
                    result[row, column] = new CellsModel
                    {
                        IsMine = dto.Cells[row, column].IsMine,
                        NeightborMines = dto.Cells[row, column].NeightborMines,
                        IsRevealed = dto.Cells[row, column].IsRevealed,
                        Isflag = dto.Cells[row, column].Isflag
                    };
                }
            }

            return result;
        }

        private GameDto ToDto(GameModel game)
        {
            var dto = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                MoveCount = game.CountRevealed,
                GameBegin = game.GameBegin,
                
                Cells = new CellsModel[Constants.RowCount, Constants.ColumnCount] // Инициализация массива
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    // Создаем новую ячейку DTO и копируем значения из модели
                    dto.Cells[row, column] = new CellsModel
                    {
                        IsMine = game[row, column].IsMine,
                        NeightborMines = game[row, column].NeightborMines,
                        IsRevealed = game[row, column].IsRevealed,
                        Isflag = game[row, column].Isflag
                    };
                }
            }

            return dto;
        }
    }
}
