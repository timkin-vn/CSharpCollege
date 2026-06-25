using CheckersGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int ModelRow { get; set; }
        public int ModelCol { get; set; }

        private Piece _piece;
        public Piece Piece
        {
            get => _piece;
            set
            {
                if (_piece == value) return;
                _piece = value;
                OnPropertyChanged(nameof(Piece));
                OnPropertyChanged(nameof(HasPiece));
                OnPropertyChanged(nameof(HasBlackPiece));
                OnPropertyChanged(nameof(HasWhitePiece));
                OnPropertyChanged(nameof(IsKing));
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged(nameof(IsSelected)); }
        }

        private bool _isValidTarget;
        public bool IsValidTarget
        {
            get => _isValidTarget;
            set { _isValidTarget = value; OnPropertyChanged(nameof(IsValidTarget)); }
        }

        public bool IsDarkCell => (Row + Column) % 2 == 0;
        public bool HasBlackPiece => Piece == Piece.Black || Piece == Piece.BlackKing;
        public bool HasWhitePiece => Piece == Piece.White || Piece == Piece.WhiteKing;
        public bool HasPiece => Piece != Piece.None;
        public bool IsKing => Piece == Piece.BlackKing || Piece == Piece.WhiteKing;
    }
}