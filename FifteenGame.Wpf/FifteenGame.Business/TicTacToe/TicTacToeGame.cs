using System;

namespace StepByStepPacman.Business.TicTacToe
{
    public class TicTacToeGame
    {
        public CellState[] Board { get; } = new CellState[9];
        public CellState CurrentPlayer { get; private set; } = CellState.X;
        public GameResult Result { get; private set; } = GameResult.InProgress;

        public bool MakeMove(int index)
        {
            if (index < 0 || index > 8) return false;
            if (Result != GameResult.InProgress) return false;
            if (Board[index] != CellState.Empty) return false;

            Board[index] = CurrentPlayer;

            RecalcResult();

            if (Result == GameResult.InProgress)
                CurrentPlayer = (CurrentPlayer == CellState.X) ? CellState.O : CellState.X;

            return true;
        }

        public void Reset()
        {
            Array.Fill(Board, CellState.Empty);
            CurrentPlayer = CellState.X;
            Result = GameResult.InProgress;
        }

        private void RecalcResult()
        {
            int[][] lines =
            {
                new[] {0,1,2}, new[] {3,4,5}, new[] {6,7,8},
                new[] {0,3,6}, new[] {1,4,7}, new[] {2,5,8},
                new[] {0,4,8}, new[] {2,4,6}
            };

            foreach (var line in lines)
            {
                var a = Board[line[0]];
                if (a == CellState.Empty) continue;

                if (Board[line[1]] == a && Board[line[2]] == a)
                {
                    Result = (a == CellState.X) ? GameResult.XWins : GameResult.OWins;
                    return;
                }
            }

            // ничья, если нет пустых клеток
            for (int i = 0; i < 9; i++)
            {
                if (Board[i] == CellState.Empty)
                {
                    Result = GameResult.InProgress;
                    return;
                }
            }

            Result = GameResult.Draw;
        }
    }
}
