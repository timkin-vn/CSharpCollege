using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CheckersGame.Business.Models
{
    public enum Piece
    {
        None,
        Black,
        White,
        BlackKing,
        WhiteKing
    }

    public enum Player
    {
        Black,
        White
    }

    public class GameModel
    {
        public event Action ModelChanged;

        public const int BoardSize = 8;

        private Piece[,] _gameBoard = new Piece[BoardSize, BoardSize];

        public Piece this[int row, int col]
        {
            get => _gameBoard[row, col];
            internal set => _gameBoard[row, col] = value;
        }

        public Player CurrentPlayer { get; internal set; } = Player.White;

        public bool IsCaptureInProgress { get; internal set; } = false;
        public int CaptureRow { get; internal set; }
        public int CaptureCol { get; internal set; }

        /// <summary>Позиции шашек, уже побитых в текущей серии взятий (для турецкого удара).</summary>
        public HashSet<(int row, int col)> CapturedThisTurn { get; } = new HashSet<(int row, int col)>();

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

        public void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Player.Black ? Player.White : Player.Black;
            RaiseModelChanged();
        }

        public bool IsGameOver()
        {
            bool blackExists = false, whiteExists = false;
            for (int r = 0; r < BoardSize; r++)
                for (int c = 0; c < BoardSize; c++)
                {
                    var p = _gameBoard[r, c];
                    if (p == Piece.Black || p == Piece.BlackKing) blackExists = true;
                    if (p == Piece.White || p == Piece.WhiteKing) whiteExists = true;
                }
            return !blackExists || !whiteExists;
        }

        public Player? Winner
        {
            get
            {
                if (!IsGameOver()) return null;
                bool blackExists = false;
                for (int r = 0; r < BoardSize; r++)
                    for (int c = 0; c < BoardSize; c++)
                        if (_gameBoard[r, c] == Piece.Black || _gameBoard[r, c] == Piece.BlackKing)
                            blackExists = true;
                return blackExists ? Player.Black : Player.White;
            }
        }

        public void RaiseModelChanged()
        {
            ModelChanged?.Invoke();
        }
    }
}