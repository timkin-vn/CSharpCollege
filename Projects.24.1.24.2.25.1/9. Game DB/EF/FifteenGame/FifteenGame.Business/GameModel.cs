using CheckersGame.Business.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CheckersGame.Business.Models
{
    public enum Piece
    {
        None = 0,
        White = 1,
        Black = 2,
        WhiteKing = 3,
        BlackKing = 4
    }

    public enum Player
    {
        White = 0,
        Black = 1
    }

    public class GameModel
    {
        public const int BoardSize = 8;

        private Piece[,] _board = new Piece[BoardSize, BoardSize];

        // Новые свойства для БД
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MoveCount { get; set; }

        public Piece this[int row, int col]
        {
            get => _board[row, col];
            set => _board[row, col] = value;
        }

        public Player CurrentPlayer { get; set; }
        public bool IsCaptureInProgress { get; private set; }
        public int CaptureRow { get; private set; }
        public int CaptureCol { get; private set; }
        public HashSet<(int, int)> CapturedThisTurn { get; } = new HashSet<(int, int)>();

        public event Action ModelChanged;
        public void RaiseModelChanged() => ModelChanged?.Invoke();

        public void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Player.White ? Player.Black : Player.White;
        }

        public void StartCaptureSequence(int row, int col)
        {
            IsCaptureInProgress = true;
            CaptureRow = row;
            CaptureCol = col;
        }

        public void EndCaptureSequence()
        {
            IsCaptureInProgress = false;
            CapturedThisTurn.Clear();
        }

        public bool IsGameOver()
        {
            bool whiteExists = false, blackExists = false;
            for (int r = 0; r < BoardSize; r++)
                for (int c = 0; c < BoardSize; c++)
                {
                    var p = _board[r, c];
                    if (p == Piece.White || p == Piece.WhiteKing) whiteExists = true;
                    if (p == Piece.Black || p == Piece.BlackKing) blackExists = true;
                }
            return !whiteExists || !blackExists;
        }

        // Для сохранения в БД
        public string SerializeState()
        {
            var state = new
            {
                Cells = Enumerable.Range(0, BoardSize).SelectMany(r =>
                    Enumerable.Range(0, BoardSize).Select(c => new { r, c, piece = (int)this[r, c] })),
                CurrentPlayer = (int)CurrentPlayer,
                IsCaptureInProgress = IsCaptureInProgress,
                CaptureRow = CaptureRow,
                CaptureCol = CaptureCol,
                CapturedThisTurn = CapturedThisTurn.Select(c => new { r = c.Item1, c = c.Item2 }),
                MoveCount = MoveCount
            };
            return JsonConvert.SerializeObject(state);
        }

        // Восстановление состояния из JSON
        public static GameModel DeserializeState(string json)
        {
            var model = new GameModel();
            if (string.IsNullOrEmpty(json))
            {
                var service = new GameService();
                service.Initialize(model);
                return model;
            }

            dynamic obj = JsonConvert.DeserializeObject(json);
            model.EndCaptureSequence();
            for (int r = 0; r < BoardSize; r++)
                for (int c = 0; c < BoardSize; c++)
                    model[r, c] = Piece.None;

            foreach (var cell in obj.Cells)
                model[(int)cell.r, (int)cell.c] = (Piece)(int)cell.piece;

            model.CurrentPlayer = (Player)(int)obj.CurrentPlayer;
            model.MoveCount = (int)obj.MoveCount;

            if ((bool)obj.IsCaptureInProgress)
            {
                model.StartCaptureSequence((int)obj.CaptureRow, (int)obj.CaptureCol);
                foreach (var cap in obj.CapturedThisTurn)
                    model.CapturedThisTurn.Add(((int)cap.r, (int)cap.c));
            }
            return model;
        }
    }
}