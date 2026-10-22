using CheckersGame.Business.Contracts;
using CheckersGame.Business.Models;
using CheckersGame.Business.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // Сервис, работающий через сервер (прокси)
        private readonly ICheckersGameService _gameService;
        // Правила игры (только для подсветки ходов на клиенте)
        private readonly GameService _coreService = new GameService();
        private GameModel _model;
        private CellViewModel _selectedCell;
        private bool _isFlipped;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();
        public string CurrentPlayerText => $"Ходит: {(_model.CurrentPlayer == Player.Black ? "Чёрные" : "Белые")}";

        public MainWindowViewModel(GameModel model, bool isFlipped, ICheckersGameService gameService)
        {
            _model = model;
            _isFlipped = isFlipped;
            _gameService = gameService;
            LoadCells();
        }

        private void LoadCells()
        {
            Cells.Clear();
            for (int modelRow = 0; modelRow < GameModel.BoardSize; modelRow++)
            {
                for (int modelCol = 0; modelCol < GameModel.BoardSize; modelCol++)
                {
                    int viewRow = _isFlipped ? (GameModel.BoardSize - 1 - modelRow) : modelRow;
                    int viewCol = _isFlipped ? (GameModel.BoardSize - 1 - modelCol) : modelCol;

                    var vm = new CellViewModel
                    {
                        Row = viewRow,
                        Column = viewCol,
                        ModelRow = modelRow,
                        ModelCol = modelCol,
                        Piece = _model[modelRow, modelCol],
                        IsSelected = false,
                        IsValidTarget = false
                    };
                    Cells.Add(vm);
                }
            }

            if (_model.IsCaptureInProgress)
            {
                var capturer = Cells.FirstOrDefault(c => c.ModelRow == _model.CaptureRow && c.ModelCol == _model.CaptureCol);
                if (capturer != null)
                {
                    capturer.IsSelected = true;
                    _selectedCell = capturer;
                    HighlightMoves(capturer);
                }
            }
            else
            {
                _selectedCell = null;
            }

            OnPropertyChanged(nameof(CurrentPlayerText));
        }

        public void CellClicked(CellViewModel clicked)
        {
            if (clicked == null) return;

            if (_model.IsCaptureInProgress)
            {
                if (_selectedCell != null && clicked.IsValidTarget)
                {
                    TryMakeMove(_selectedCell, clicked);
                }
                return;
            }

            if (_selectedCell != null && clicked.IsValidTarget)
            {
                TryMakeMove(_selectedCell, clicked);
                return;
            }

            // Выбор своей шашки
            bool isOwnPiece = (clicked.Piece == Piece.Black || clicked.Piece == Piece.BlackKing) && _model.CurrentPlayer == Player.Black
                           || (clicked.Piece == Piece.White || clicked.Piece == Piece.WhiteKing) && _model.CurrentPlayer == Player.White;
            if (!isOwnPiece)
            {
                ClearSelection();
                return;
            }

            bool hasAnyCapture = _coreService.HasAnyCapture(_model);
            bool clickedHasCapture = _coreService.CanCapture(_model, clicked.ModelRow, clicked.ModelCol);

            if (hasAnyCapture && !clickedHasCapture)
            {
                ClearSelection();
                return;
            }

            if (_selectedCell == clicked)
            {
                ClearSelection();
                return;
            }

            ClearSelection();
            _selectedCell = clicked;
            _selectedCell.IsSelected = true;
            HighlightMoves(clicked);
        }

        private void TryMakeMove(CellViewModel from, CellViewModel to)
        {
            try
            {
                // Ход выполняется на сервере, затем состояние загружается заново
                _gameService.MakeMove(_model.Id, from.ModelRow, from.ModelCol, to.ModelRow, to.ModelCol);
                _model = _gameService.LoadGame(_model.Id);
                _selectedCell = null;
                LoadCells();
            }
            catch (Exception)
            {
                // Ход не удался - сбрасываем выделение
                ClearSelection();
            }
        }

        public void FinishCapture()
        {
            _gameService.FinishCapture(_model.Id);
            _model = _gameService.LoadGame(_model.Id);
            LoadCells();
        }

        private void ClearSelection()
        {
            if (_selectedCell != null)
            {
                _selectedCell.IsSelected = false;
                _selectedCell = null;
            }
            foreach (var cell in Cells)
                cell.IsValidTarget = false;
        }

        private void HighlightMoves(CellViewModel from)
        {
            var moves = _coreService.GetAvailableMoves(_model, from.ModelRow, from.ModelCol);
            foreach (var move in moves)
            {
                var target = Cells.FirstOrDefault(cell => cell.ModelRow == move.row && cell.ModelCol == move.col);
                if (target != null) target.IsValidTarget = true;
            }
        }
    }
}
