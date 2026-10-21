using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public GameModel GetByGameId(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return FromDto(dto);
        }

        public GameModel GetByUserId(int userId)
        {
            var dtos = _repository.GetByUserId(userId);

            var dto = dtos.LastOrDefault();

            if (dto != null)
            {
                return FromDto(dto);
            }

            return StartNewGame(userId, 1, 1);
        }

        public GameModel StartNewGame(int userId, int difficulty, int theme)
        {
            var game = new GameModel
            {
                UserId = userId,
                Difficulty = difficulty >= 1 && difficulty <= 3 ? difficulty : 1,
                Theme = theme >= 1 && theme <= 3 ? theme : 1,
                MoveCount = 0,
                Score = 0,
                ComboStreak = 0,
                HintsLeft = Constants.DefaultHintsCount,
                ElapsedSeconds = 0
            };

            Shuffle(game);

            var gameId = _repository.Save(ToDto(game));

            return GetByGameId(gameId);
        }

        public void Initialize(GameModel model)
        {
            Constants.GetGridSize(model.Difficulty, out int rows, out int cols);

            int totalCards = rows * cols;

            int totalPairs = totalCards / 2;

            var pairs = new List<int>();

            for (int pairId = 1; pairId <= totalPairs; pairId++)
            {
                pairs.Add(-pairId); 
                pairs.Add(-pairId);
            }

            int index = 0;

            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    if (r < rows && c < cols && index < pairs.Count)
                    {
                        model[r, c] = pairs[index++];
                    }

                    else

                    {
                        model[r, c] = Constants.FreeCellValue; 
                    }
                }
            }

            model.FreeCellRow = -1;
            model.FreeCellColumn = -1;
        }

        public void Shuffle(GameModel model)
        {
            Initialize(model);

            Constants.GetGridSize(model.Difficulty, out int rows, out int cols);

            var rnd = new Random();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int randomRow = rnd.Next(rows);

                    int randomCol = rnd.Next(cols);

                    int temp = model[r, c];

                    model[r, c] = model[randomRow, randomCol];

                    model[randomRow, randomCol] = temp;
                }
            }

            model.MoveCount = 0;

            model.Score = 0;

            model.ComboStreak = 0;

            model.HintsLeft = Constants.DefaultHintsCount;

            model.FreeCellRow = -1;

            model.FreeCellColumn = -1;
        }

        public bool IsGameOver(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);

            return dto != null && IsGameOver(FromDto(dto));
        }

        public bool IsGameOver(GameModel model)
        {
            if (model == null) return false;

            Constants.GetGridSize(model.Difficulty, out int rows, out int cols);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int val = model[r, c];

                    if (val != Constants.FreeCellValue && val < 100)
                    {
                        return false; 
                    }
                }
            }

            return true;
        }

        public GameModel OpenCard(int gameId, int row, int column, int elapsedSeconds = 0)
        {
            var dto = _repository.GetByGameId(gameId);
            var model = FromDto(dto);
            if (model == null) return null;
            if (elapsedSeconds > 0)
            {
                model.ElapsedSeconds = elapsedSeconds;
            }

            OpenCardOnModel(model, row, column);

            _repository.Save(ToDto(model));
            return model;
        }

        public bool OpenCardOnModel(GameModel model, int row, int column)
        {
            if (row < 0 || row >= Constants.RowCount || column < 0 || column >= Constants.ColumnCount)

                return false;

            var currentlyOpen = new List< Tuple<int, int> > ();

            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    if (model[r, c] > 0 && model[r, c] < 100)
                    {
                        currentlyOpen.Add(Tuple.Create(r, c));
                    }
                }
            }

            if (currentlyOpen.Count >= 2)
            {
                foreach (var pos in currentlyOpen)
                {
                    model[pos.Item1, pos.Item2] = -Math.Abs(model[pos.Item1, pos.Item2]);
                }

                currentlyOpen.Clear();

                model.FreeCellRow = -1;

                model.FreeCellColumn = -1;
            }

            int cellValue = model[row, column];

            if (cellValue >= 0)

                return false;

            int pairId = Math.Abs(cellValue);

            model[row, column] = pairId;

            if (currentlyOpen.Count == 0)
            {
                model.FreeCellRow = row;

                model.FreeCellColumn = column;

                return true;
            }

            int firstRow = currentlyOpen[0].Item1;

            int firstCol = currentlyOpen[0].Item2;

            int firstPairId = model[firstRow, firstCol];

            model.MoveCount++;

            if (firstPairId == pairId)
            {
                model[firstRow, firstCol] = 100 + pairId;

                model[row, column] = 100 + pairId;

                model.ComboStreak++;

                model.Score += Constants.BaseMatchPoints * model.ComboStreak;

                model.FreeCellRow = -1;

                model.FreeCellColumn = -1;
            }

            else

            {
                model.ComboStreak = 0;
            }

            return true;
        }

        public GameModel UseHint(int gameId, int elapsedSeconds = 0)
        {
            var dto = _repository.GetByGameId(gameId);

            var model = FromDto(dto);

            if (model == null || model.HintsLeft <= 0)

                return model;

            if (elapsedSeconds > 0)
            {
                model.ElapsedSeconds = elapsedSeconds;
            }

            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    if (model[r, c] > 0 && model[r, c] < 100)
                    {
                        model[r, c] = -model[r, c];
                    }
                }
            }

            int targetPair = -1;

            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    if (model[r, c] < 0)
                    {
                        targetPair = Math.Abs(model[r, c]);

                        break;
                    }
                }

                if (targetPair != -1) break;
            }

            if (targetPair != -1)
            {
                for (int r = 0; r < Constants.RowCount; r++)
                {
                    for (int c = 0; c < Constants.ColumnCount; c++)
                    {
                        if (Math.Abs(model[r, c]) == targetPair)
                        {
                            model[r, c] = 100 + targetPair;
                        }
                    }
                }

                model.HintsLeft--;

                model.Score = Math.Max(0, model.Score - Constants.HintPenaltyPoints);

                model.FreeCellRow = -1;

                model.FreeCellColumn = -1;

                _repository.Save(ToDto(model));
            }

            return model;
        }

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            var dto = _repository.GetByGameId(gameId);

            var model = FromDto(dto);

            if (model == null) return null;

            MakeMove(model, direction);

            _repository.Save(ToDto(model));

            return model;
        }

        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    if (model[r, c] < 0)
                    {
                        return OpenCardOnModel(model, r, c);
                    }
                }
            }

            return false;
        }

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        private GameModel FromDto(GameDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            var result = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount,
                Difficulty = dto.Difficulty > 0 ? dto.Difficulty : 1,
                Theme = dto.Theme > 0 ? dto.Theme : 1,
                Score = dto.Score,
                ComboStreak = dto.ComboStreak,
                HintsLeft = dto.HintsLeft,
                ElapsedSeconds = dto.ElapsedSeconds,
                FreeCellRow = -1,
                FreeCellColumn = -1
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result[row, column] = dto.Cells[row, column];

                    if (result[row, column] > 0 && result[row, column] < 100)
                    {
                        result.FreeCellRow = row;
                        result.FreeCellColumn = column;
                    }
                }
            }

            return result;
        }

        private GameDto ToDto(GameModel model)
        {
            var result = new GameDto
            {
                Id = model.Id,
                UserId = model.UserId,
                MoveCount = model.MoveCount,
                Difficulty = model.Difficulty,
                Theme = model.Theme,
                Score = model.Score,
                ComboStreak = model.ComboStreak,
                HintsLeft = model.HintsLeft,
                ElapsedSeconds = model.ElapsedSeconds
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result.Cells[row, column] = model[row, column];
                }
            }

            return result;
        }
    }
}
