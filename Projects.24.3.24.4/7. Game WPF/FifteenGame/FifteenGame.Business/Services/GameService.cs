using System;
using System.Collections.Generic;
using System.Linq;
using FifteenGame.Business.Models;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        public void Initialize(GameModel model)
        {
            for (int r = 0; r < GameModel.RowCount; r++)
                for (int c = 0; c < GameModel.ColumnCount; c++)
                    model[r, c] = CellState.Empty;

            model.Result = GameResult.None;
            model.WinningCells.Clear();
        }

        public bool MakeMove(GameModel model, int row, int column, CellState player)
        {
            if (model.Result != GameResult.None) return false;
            if (model[row, column] != CellState.Empty) return false;

            model[row, column] = player;
            CheckGameOver(model);
            return true;
        }

        public void MakeComputerMove(GameModel model)
        {
            if (model.Result != GameResult.None) return;

            var winMove = FindBestMove(model, CellState.O);
            if (winMove.HasValue)
            {
                model[winMove.Value.Row, winMove.Value.Column] = CellState.O;
                CheckGameOver(model);
                return;
            }

            var blockMove = FindBestMove(model, CellState.X);
            if (blockMove.HasValue)
            {
                model[blockMove.Value.Row, blockMove.Value.Column] = CellState.O;
                CheckGameOver(model);
                return;
            }

            if (model[1, 1] == CellState.Empty)
            {
                model[1, 1] = CellState.O;
            }
            else
            {
                var emptyCells = new List<(int r, int c)>();
                for (int r = 0; r < GameModel.RowCount; r++)
                    for (int c = 0; c < GameModel.ColumnCount; c++)
                        if (model[r, c] == CellState.Empty)
                            emptyCells.Add((r, c));

                if (emptyCells.Count > 0)
                {
                    var random = new Random();
                    var move = emptyCells[random.Next(emptyCells.Count)];
                    model[move.r, move.c] = CellState.O;
                }
            }

            CheckGameOver(model);
        }

        private (int Row, int Column)? FindBestMove(GameModel model, CellState player)
        {
            for (int i = 0; i < 3; i++)
            {
                if (model[i, 0] == player && model[i, 1] == player && model[i, 2] == CellState.Empty) return (i, 2);
                if (model[i, 0] == player && model[i, 2] == player && model[i, 1] == CellState.Empty) return (i, 1);
                if (model[i, 1] == player && model[i, 2] == player && model[i, 0] == CellState.Empty) return (i, 0);

                if (model[0, i] == player && model[1, i] == player && model[2, i] == CellState.Empty) return (2, i);
                if (model[0, i] == player && model[2, i] == player && model[1, i] == CellState.Empty) return (1, i);
                if (model[1, i] == player && model[2, i] == player && model[0, i] == CellState.Empty) return (0, i);
            }

            if (model[0, 0] == player && model[1, 1] == player && model[2, 2] == CellState.Empty) return (2, 2);
            if (model[0, 0] == player && model[2, 2] == player && model[1, 1] == CellState.Empty) return (1, 1);
            if (model[1, 1] == player && model[2, 2] == player && model[0, 0] == CellState.Empty) return (0, 0);

            if (model[0, 2] == player && model[1, 1] == player && model[2, 0] == CellState.Empty) return (2, 0);
            if (model[0, 2] == player && model[2, 0] == player && model[1, 1] == CellState.Empty) return (1, 1);
            if (model[1, 1] == player && model[2, 0] == player && model[0, 2] == CellState.Empty) return (0, 2);

            return null;
        }

        private void CheckGameOver(GameModel model)
        {
            model.WinningCells.Clear();

            for (int i = 0; i < 3; i++)
            {
                if (model[i, 0] != CellState.Empty && model[i, 0] == model[i, 1] && model[i, 1] == model[i, 2])
                {
                    SetWin(model, model[i, 0], (i, 0), (i, 1), (i, 2));
                    return;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (model[0, i] != CellState.Empty && model[0, i] == model[1, i] && model[1, i] == model[2, i])
                {
                    SetWin(model, model[0, i], (0, i), (1, i), (2, i));
                    return;
                }
            }

            if (model[0, 0] != CellState.Empty && model[0, 0] == model[1, 1] && model[1, 1] == model[2, 2])
            {
                SetWin(model, model[0, 0], (0, 0), (1, 1), (2, 2));
                return;
            }

            if (model[0, 2] != CellState.Empty && model[0, 2] == model[1, 1] && model[1, 1] == model[2, 0])
            {
                SetWin(model, model[0, 2], (0, 2), (1, 1), (2, 0));
                return;
            }

            //Проверка на ничью
            bool hasEmpty = false;
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    if (model[r, c] == CellState.Empty) hasEmpty = true;

            if (!hasEmpty)
                model.Result = GameResult.Draw;
        }

        private void SetWin(GameModel model, CellState winner, params (int Row, int Column)[] coords)
        {
            model.Result = winner == CellState.X ? GameResult.PlayerWin : GameResult.ComputerWin;
            foreach (var coord in coords)
                model.WinningCells.Add(coord);
        }
    }
}