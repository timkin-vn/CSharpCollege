using TicTacToe.Business.Models;

namespace TicTacToe.Business.Services
{
    public class GameService
    {
        public void Initialize(GameModel model)
        {
            model.Reset();
        }

        public bool MakeMove(GameModel model, int row, int column)
        {
            // Проверяем, что игра еще идет
            if (model.Result != GameResult.InProgress)
            {
                return false;
            }

            // Проверяем, что клетка в пределах поля
            if (row < 0 || row >= GameModel.RowCount ||
                column < 0 || column >= GameModel.ColumnCount)
            {
                return false;
            }

            // Проверяем, что клетка пустая
            if (model[row, column] != CellValue.Empty)
            {
                return false;
            }

            // Делаем ход
            model[row, column] = model.CurrentPlayer;
            model.MovesCount++;

            // Проверяем результат игры
            CheckGameResult(model);

            // Меняем игрока, если игра продолжается
            if (model.Result == GameResult.InProgress)
            {
                model.CurrentPlayer = model.CurrentPlayer == CellValue.X
                    ? CellValue.O
                    : CellValue.X;
            }

            return true;
        }

        private void CheckGameResult(GameModel model)
        {
            // Проверяем горизонтали
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                if (model[row, 0] != CellValue.Empty &&
                    model[row, 0] == model[row, 1] &&
                    model[row, 1] == model[row, 2])
                {
                    model.Result = model[row, 0] == CellValue.X
                        ? GameResult.XWins
                        : GameResult.OWins;
                    return;
                }
            }

            // Проверяем вертикали
            for (int column = 0; column < GameModel.ColumnCount; column++)
            {
                if (model[0, column] != CellValue.Empty &&
                    model[0, column] == model[1, column] &&
                    model[1, column] == model[2, column])
                {
                    model.Result = model[0, column] == CellValue.X
                        ? GameResult.XWins
                        : GameResult.OWins;
                    return;
                }
            }

            // Проверяем диагонали
            if (model[0, 0] != CellValue.Empty &&
                model[0, 0] == model[1, 1] &&
                model[1, 1] == model[2, 2])
            {
                model.Result = model[0, 0] == CellValue.X
                    ? GameResult.XWins
                    : GameResult.OWins;
                return;
            }

            if (model[0, 2] != CellValue.Empty &&
                model[0, 2] == model[1, 1] &&
                model[1, 1] == model[2, 0])
            {
                model.Result = model[0, 2] == CellValue.X
                    ? GameResult.XWins
                    : GameResult.OWins;
                return;
            }

            // Проверяем на ничью
            if (model.MovesCount >= GameModel.RowCount * GameModel.ColumnCount)
            {
                model.Result = GameResult.Draw;
            }
        }

        public bool IsGameOver(GameModel model)
        {
            return model.Result != GameResult.InProgress;
        }

        public void MakeComputerMove(GameModel model)
        {
            if (model.Result != GameResult.InProgress ||
                model.CurrentPlayer != CellValue.O)
            {
                return;
            }

            var rnd = new Random();
            var availableCells = new List<(int, int)>();

            // Находим все доступные клетки
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model[row, column] == CellValue.Empty)
                    {
                        availableCells.Add((row, column));
                    }
                }
            }

            // Делаем случайный ход
            if (availableCells.Count > 0)
            {
                var (row, column) = availableCells[rnd.Next(availableCells.Count)];
                MakeMove(model, row, column);
            }
        }
    }
}