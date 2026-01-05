using Nonogram.Common.BusinessModels;
using Nonogram.Common.Definitions;
using Nonogram.Common.Dtos;
using Nonogram.Common.Repositories;
using Nonogram.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nonogram.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public void InitializeGame(GameModel model)
        {
            // Генерируем подсказки и решение
            GenerateSolutionAndClues(model);

            // Сбрасываем пользовательскую сетку
            ResetUserGrid(model);

            model.MistakesCount = 0;
        }

        public GameModel MakeMove(int gameId, int row, int column)
        {
            Console.WriteLine($"GameService.MakeMove: gameId={gameId}, row={row}, column={column}");

            try
            {
                // Получаем игру из базы
                var dto = _repository.GetByGameId(gameId);
                if (dto == null)
                {
                    Console.WriteLine("Game not found in database!");
                    return null;
                }

                // Конвертируем в модель
                var model = FromDto(dto);

                Console.WriteLine($"Game loaded: Id={model.Id}, Mistakes={model.MistakesCount}");
                Console.WriteLine($"Cell [{row},{column}] before move: {model[row, column]}");

                // Выполняем ход
                var result = MakeMoveInternal(model, row, column);
                Console.WriteLine($"Move result: {result}");

                // Если ход выполнен успешно - сохраняем
                if (result != MoveResult.AlreadyFilled && result != MoveResult.Invalid)
                {
                    Console.WriteLine($"Cell [{row},{column}] after move: {model[row, column]}");

                    // Сохраняем в базу
                    var savedId = _repository.Save(ToDto(model));
                    Console.WriteLine($"Game saved with ID: {savedId}");

                    // Загружаем обратно для проверки
                    var checkDto = _repository.GetByGameId(gameId);
                    if (checkDto != null)
                    {
                        Console.WriteLine($"Cell in DB after save: {checkDto.Cells[row, column]}");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GameService.MakeMove: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        private MoveResult MakeMoveInternal(GameModel model, int row, int column)
        {
            // Проверка границ
            if (row < 0 || row >= Constants.RowCount ||
                column < 0 || column >= Constants.ColumnCount)
            {
                return MoveResult.Invalid;
            }

            // Проверка, что клетка еще не заполнена
            if (model[row, column] == Constants.FilledCell ||
                model[row, column] == Constants.CrossedCell)
            {
                return MoveResult.AlreadyFilled;
            }

            // Проверяем решение
            var solution = model.GetSolution(row, column);
            Console.WriteLine($"Solution at [{row},{column}]: {solution}");

            if (solution == Constants.FilledCell)
            {
                // Правильный ход - закрашиваем клетку
                model[row, column] = Constants.FilledCell;
                Console.WriteLine($"Cell [{row},{column}] marked as FILLED");
                return MoveResult.Correct;
            }
            else
            {
                // Неправильный ход - ставим крестик
                model[row, column] = Constants.CrossedCell;
                model.MistakesCount++;
                Console.WriteLine($"Cell [{row},{column}] marked as CROSSED. Mistakes: {model.MistakesCount}");
                return MoveResult.Wrong;
            }
        }

        private void GenerateSolutionAndClues(GameModel model)
        {
            // Очищаем подсказки
            model.RowClues.Clear();
            model.ColumnClues.Clear();

            // Задаем подсказки для столбцов
            int[][] columnClues = new int[][]
            {
                new int[] { 2 },
                new int[] { 4 },
                new int[] { 2, 2 },
                new int[] { 7 },
                new int[] { 1, 3, 2 },
                new int[] { 3, 3 },
                new int[] { 10 },
                new int[] { 12 },
                new int[] { 13 },
                new int[] { 4, 6, 3 },
                new int[] { 2, 4, 2 },
                new int[] { 1, 2, 1 },
                new int[] { 4 },
                new int[] { 8 },
                new int[] { 4, 5 }
            };

            // Задаем подсказки для строк
            int[][] rowClues = new int[][]
            {
                new int[] { 3 },
                new int[] { 3, 1 },
                new int[] { 3, 1 },
                new int[] { 5, 2 },
                new int[] { 6, 3 },
                new int[] { 2, 6, 2 },
                new int[] { 4, 8 },
                new int[] { 2, 2, 8 },
                new int[] { 5, 5, 2 },
                new int[] { 3, 5, 2 },
                new int[] { 7, 2 },
                new int[] { 5, 1 },
                new int[] { 4, 1 },
                new int[] { 4 },
                new int[] { 3 }
            };

            // Добавляем подсказки в модель
            foreach (var clue in rowClues)
            {
                model.RowClues.Add(new List<int>(clue));
            }

            foreach (var clue in columnClues)
            {
                model.ColumnClues.Add(new List<int>(clue));
            }

            // Генерируем решение
            GenerateSolution(model);
        }

        private void GenerateSolution(GameModel model)
        {
            // Очищаем решение
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int col = 0; col < Constants.ColumnCount; col++)
                {
                    model.SetSolution(row, col, Constants.EmptyCell);
                }
            }

            // Координаты закрашенных клеток
            var filledCells = new List<(int, int)>
            {
                // Основное изображение (кролик)
                (0, 9), (0, 10), (0, 11),
                (1, 8), (1, 9), (1, 10),
                (2, 7), (2, 8), (2, 9),
                (3, 5), (3, 6), (3, 7), (3, 8), (3, 9),
                (4, 3), (4, 4), (4, 5), (4, 6), (4, 7), (4, 8),
                (5, 2), (5, 3), (5, 5), (5, 6), (5, 7), (5, 8), (5, 9), (5, 10), (5, 12), (5, 13),
                (6, 1), (6, 2), (6, 3), (6, 4), (6, 6), (6, 7), (6, 8), (6, 9), (6, 10), (6, 11), (6, 12), (6, 13),
                (7, 0), (7, 1), (7, 3), (7, 4), (7, 6), (7, 7), (7, 8), (7, 9), (7, 10), (7, 11), (7, 12), (7, 13),
                (8, 0), (8, 1), (8, 2), (8, 3), (8, 4), (8, 6), (8, 7), (8, 8), (8, 9), (8, 10), (8, 13), (8, 14),
                (9, 1), (9, 2), (9, 3), (9, 5), (9, 6), (9, 7), (9, 8), (9, 9), (9, 13), (9, 14),
                (10, 3), (10, 4), (10, 5), (10, 6), (10, 7), (10, 8), (10, 9), (10, 13), (10, 14),
                (11, 4), (11, 5), (11, 6), (11, 7), (11, 8), (11, 14),
                (12, 6), (12, 7), (12, 8), (12, 9), (12, 14),
                (13, 7), (13, 8), (13, 9), (13, 10),
                (14, 9), (14, 10), (14, 11),
                
                // Дополнительные клетки
                (1, 14), (2, 14), (3, 13), (3, 14), (4, 12), (4, 13), (4, 14)
            };

            // Заполняем решение
            foreach (var (row, col) in filledCells)
            {
                if (row >= 0 && row < Constants.RowCount &&
                    col >= 0 && col < Constants.ColumnCount)
                {
                    model.SetSolution(row, col, Constants.FilledCell);
                }
            }
        }

        private void ResetUserGrid(GameModel model)
        {
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int col = 0; col < Constants.ColumnCount; col++)
                {
                    model[row, col] = Constants.EmptyCell;
                }
            }
        }

        public bool IsGameOver(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            if (dto == null) return false;

            var model = FromDto(dto);
            return model.MistakesCount >= Constants.MaxMistakes;
        }

        public bool IsGameWon(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            if (dto == null) return false;

            var model = FromDto(dto);

            // Проверяем, все ли закрашенные клетки в решении отмечены пользователем
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int col = 0; col < Constants.ColumnCount; col++)
                {
                    if (model.GetSolution(row, col) == Constants.FilledCell &&
                        model[row, col] != Constants.FilledCell)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public GameModel GetByUserId(int userId)
        {
            // Получаем все игры пользователя
            var dtos = _repository.GetByUserId(userId).ToList();

            GameModel model;

            if (dtos.Any())
            {
                Console.WriteLine($"Found {dtos.Count} games for user {userId}");
                // Берем первую активную игру
                model = FromDto(dtos.First());
            }
            else
            {
                Console.WriteLine($"No games found for user {userId}, creating new one");

                // Создаем новую игру
                model = new GameModel
                {
                    UserId = userId,
                    MistakesCount = 0
                };

                InitializeGame(model);

                // Сохраняем в базу
                var gameId = _repository.Save(ToDto(model));
                model.Id = gameId;
                Console.WriteLine($"New game created with ID: {gameId}");
            }

            return model;
        }

        public GameModel GetByGameId(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return dto != null ? FromDto(dto) : null;
        }

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        private GameModel FromDto(GameDto dto)
        {
            var model = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MistakesCount = dto.MistakesCount
            };

            // Загружаем клетки пользователя
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int col = 0; col < Constants.ColumnCount; col++)
                {
                    model[row, col] = dto.Cells[row, col];
                }
            }

            // Генерируем подсказки и решение
            GenerateSolutionAndClues(model);

            return model;
        }

        private GameDto ToDto(GameModel model)
        {
            var dto = new GameDto
            {
                Id = model.Id,
                UserId = model.UserId,
                MistakesCount = model.MistakesCount
            };

            // Сохраняем клетки пользователя
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int col = 0; col < Constants.ColumnCount; col++)
                {
                    dto.Cells[row, col] = model[row, col];
                }
            }

            return dto;
        }
    }
}