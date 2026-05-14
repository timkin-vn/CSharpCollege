using Minesweeper.Business.Core;
using Minesweeper.Common.BusinessDtos;
using Minesweeper.Common.BusinessModels;
using Minesweeper.Common.Dto;
using System;

namespace Minesweeper.Business.Mappers
{
    public static class GameMapper
    {
        public static GameResponse ToResponse(GameModel model, Field field = null)
        {
            if (model == null) return null;

            var response = new GameResponse
            {
                Id = model.Id,
                UserId = model.UserId,
                Status = model.Status,
                IsGameOver = model.IsGameOver,
                IsGameWon = model.IsGameWon,
                PlayTime = model.PlayTime,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                FieldSize = model.FieldSize > 0 ? model.FieldSize : (field?.Size ?? 10),
                MineCount = model.MineCount > 0 ? model.MineCount : (field?.MineCount ?? 15),
                FlagsPlaced = field?.FlagsPlaced ?? 0,
                CellsRevealed = field?.CellsRevealed ?? 0,
                CellsRemaining = (model.FieldSize * model.FieldSize) - (field?.CellsRevealed ?? 0)
            };

            if (field != null)
            {
                response.VisibleField = ConvertToJaggedArray(field.GetVisibleField());
            }

            return response;
        }
        private static int[][] ConvertToJaggedArray(int[,] array2D)
        {
            int rows = array2D.GetLength(0);
            int cols = array2D.GetLength(1);
            var result = new int[rows][];

            for (int i = 0; i < rows; i++)
            {
                result[i] = new int[cols];
                for (int j = 0; j < cols; j++)
                {
                    result[i][j] = array2D[i, j];
                }
            }

            return result;
        }
        public static GameModel ToModel(GameStateDto dto)
        {
            if (dto == null) return null;

            return new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                PlayTime = dto.PlayTime,
                IsGameOver = dto.IsGameOver,
                IsGameWon = dto.IsGameWon,
                Status = dto.IsGameOver ? "game_over" : dto.IsGameWon ? "won" : "playing",
                FieldData = dto.GameData
            };
        }

        public static GameStateDto ToDto(GameModel model)
        {
            if (model == null) return null;

            return new GameStateDto
            {
                Id = model.Id,
                UserId = model.UserId,
                GameData = model.FieldData,
                IsGameOver = model.IsGameOver,
                IsGameWon = model.IsGameWon,
                PlayTime = model.PlayTime,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }
    }
}