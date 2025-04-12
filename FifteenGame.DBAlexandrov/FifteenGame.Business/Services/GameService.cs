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
        public int X;
        public int Y;
        public Units SelectedUnit;
        private bool isUnitSelected = false;
        public Units selectedUnit;
        private readonly IGameRepository _gameRepository;

        private List<Units> units_ = new List<Units>
        {
            new Units(" ", 0, 0, 0, 0, Units.UnitType.None),
            new Units("Д", 100, 20, 0, 1, Units.UnitType.Dragon), // Дракон
            new Units("М", 80, 10, 0, 3, Units.UnitType.Medic), // Медик
            new Units("Р", 120, 25, 0, 5, Units.UnitType.Knight), // Рыцарь
            new Units("К", 150, 15, 0, 7, Units.UnitType.King), // Король
            new Units("Б", 500, 40, 4, 2, Units.UnitType.Boss) // Босс
        };

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
                GameBegin = DateTime.Now,
                MoveCount = 0,
            };

            Shuffle(game);
            game.MoveCount = 0;
            dto = ToDto(game);
            var gameId = _gameRepository.Save(dto);

            return GetByGameId(gameId);
        }

        public void Initialize(GameModel model)
        {
            int value = 1;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = value++;
                }
            }

            model[Constants.RowCount - 1, Constants.ColumnCount - 1] = Constants.FreeCellValue;
            model.FreeCellRow = Constants.RowCount - 1;
            model.FreeCellColumn = Constants.ColumnCount - 1;
        }

        public bool IsGameOver(GameModel model)
        {
            int freeCellRow = model.FreeCellRow;
            if (freeCellRow != Constants.RowCount - 1)
            {
                return false;
            }

            int freeCellColumn = model.FreeCellColumn;
            if (freeCellColumn != Constants.ColumnCount - 1)
            {
                return false;
            }

            int value = 1;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (row == freeCellRow && column == freeCellColumn)
                    {
                        if (model[row, column] != Constants.FreeCellValue)
                        {
                            return false;
                        }
                    }
                    else if (model[row, column] != value++)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsGameOver(int gameId)
        {
            var gameDto = _gameRepository.GetByGameId(gameId);
            var result = IsGameOver(FromDto(gameDto));
            return result;
        }
        private bool IsAdjacent(int unitRow, int unitColumn, int targetRow, int targetColumn)
        {

            return (Math.Abs(unitRow - targetRow) == 1 && unitColumn == targetColumn) ||
                   (Math.Abs(unitColumn - targetColumn) == 1 && unitRow == targetRow);
        }
        public bool MakeMove1(GameModel model, int targetRow, int targetColumn, Units selectedUnit)
        {
            if (!isUnitSelected)
            {

                HighlightAdjacentCells(model);
                SelectedUnit = selectedUnit;
                isUnitSelected = true;
                

                return false;
            }
            else
            {
                if (IsAdjacent(SelectedUnit.X, SelectedUnit.Y, targetRow, targetColumn))
                {
                    if (model[targetRow, targetColumn] != null)
                    {
                        model[targetRow, targetColumn] = model[SelectedUnit.X, SelectedUnit.Y];
                    }
                    isUnitSelected = false;
                    return true;
                }
                else
                {
                    Console.WriteLine($"Клетка ({targetRow}, {targetColumn}) не соседняя с ({SelectedUnit.X}, {SelectedUnit.Y})");
                }
            }
            return false;
        }

        public GameModel MakeMove(int gameId,int X,int Y, Units model, MoveDirection direction)
        {
            var gameDto = _gameRepository.GetByGameId(gameId);
            var gameModel = FromDto(gameDto);

            // Обновляем состояние units на основе gameModel
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (gameModel[row, column] != Constants.FreeCellValue)
                    {
                        model[row, column] = units_[gameModel[row, column]];
                    }
                    else
                    {
                        model[row, column] = units_[0]; // Пустая клетка
                    }
                }
            }

            MakeMove1(gameModel, X, Y, model);
            _gameRepository.Save(ToDto(gameModel));
            return gameModel;
        }

        public void RemoveGame(int gameId)
        {
            _gameRepository.Remove(gameId);
        }

        private void HighlightAdjacentCells(GameModel model)
        {

            int row = model.FreeCellRow;
            int column = model.FreeCellColumn;


            if (row > 0) HighlightCell(row - 1, column);
            if (row < Constants.RowCount - 1) HighlightCell(row + 1, column);
            if (column > 0) HighlightCell(row, column - 1);
            if (column < Constants.ColumnCount - 1) HighlightCell(row, column + 1);
        }

        private void HighlightCell(int row, int column)
        {

        }
        public void Shuffle(GameModel model)
        {
            Initialize(model);


        }
        private GameModel FromDto(GameDto dto)
        {
            var result = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount,
                GameBegin = dto.GameBegin,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result[row, column] = dto.Cells[row, column];
                    if (result[row, column] == Constants.FreeCellValue)
                    {
                        result.FreeCellRow = row;
                        result.FreeCellColumn = column;
                    }
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
                MoveCount = game.MoveCount,
                GameBegin = game.GameBegin,
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
