using LightsOutGame.Business.Models;

namespace LightsOutGame.Business.Services
{
    // Вся игровая логика на сервере (аналог GameService у преподавателя).
    // Сервис без состояния: работает над переданной моделью.
    public class GameService
    {
        private readonly Random _random = new Random();

        // Новая игра: гарантированно решаемая раскладка.
        // Берём пустое поле и делаем несколько случайных "нажатий" —
        // такое поле всегда можно вернуть в выключенное состояние.
        public void Shuffle(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
                for (int column = 0; column < GameModel.ColumnCount; column++)
                    model[row, column] = false;

            int presses = _random.Next(8, 16);
            for (int i = 0; i < presses; i++)
            {
                int row = _random.Next(GameModel.RowCount);
                int column = _random.Next(GameModel.ColumnCount);
                Press(model, row, column);
            }

            // Если случайно собралось решённое поле — нажмём ещё раз
            if (IsGameOver(model))
            {
                Press(model, _random.Next(GameModel.RowCount), _random.Next(GameModel.ColumnCount));
            }
        }

        // Ход игрока: нажатие по клетке переключает её и соседей (аналог MakeMove).
        public void MakeMove(GameModel model, int row, int column)
        {
            if (row < 0 || row >= GameModel.RowCount || column < 0 || column >= GameModel.ColumnCount)
                return;

            Press(model, row, column);
        }

        // Переключаем нажатую клетку и 4 соседние (крест).
        private void Press(GameModel model, int row, int column)
        {
            Toggle(model, row, column);
            Toggle(model, row - 1, column);
            Toggle(model, row + 1, column);
            Toggle(model, row, column - 1);
            Toggle(model, row, column + 1);
        }

        private void Toggle(GameModel model, int row, int column)
        {
            if (row >= 0 && row < GameModel.RowCount && column >= 0 && column < GameModel.ColumnCount)
            {
                model[row, column] = !model[row, column];
            }
        }

        // Игра окончена, когда все лампочки выключены (аналог IsGameOver).
        public bool IsGameOver(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
                for (int column = 0; column < GameModel.ColumnCount; column++)
                    if (model[row, column]) return false;
            return true;
        }
    }
}
